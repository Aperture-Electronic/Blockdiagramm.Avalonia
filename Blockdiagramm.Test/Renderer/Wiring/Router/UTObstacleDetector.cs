using Avalonia;
using Avalonia.Controls;
using Blockdiagramm.Controls.Diagram.Wire;
using Blockdiagramm.Extensions;
using Blockdiagramm.Renderer.Wiring.Router;

namespace Blockdiagramm.Test.Renderer.Wiring.Router
{
    [TestClass]
    public class UTObstacleDetector
    {
        [TestMethod]
        [DataRow(null)]
        public void TestSegmentCrossDetector(int? caseId)
        {
            int casesCount = ObstacleDetectorTestCases.Cases.Count;
            int from = (caseId == null) ? 0 : (int)caseId;
            int to = caseId == null ? casesCount : from + 1;

            Parallel.For(from, to, delegate (int id)
            {
                ObstacleDetectorTestCase testCase = ObstacleDetectorTestCases.Cases[id];
                Type targetRouterType = typeof(AStarRouter);
                MethodInfo? targetMethod = targetRouterType.GetMethod("CheckCrossPoint", BindingFlags.Instance | BindingFlags.NonPublic);
                MethodInfo? addSegmentsMethod = targetRouterType.GetMethod("AddSegments", BindingFlags.Instance | BindingFlags.NonPublic);
                FieldInfo? lineObstaclesField = targetRouterType.GetField("lineObstacles", BindingFlags.Instance | BindingFlags.NonPublic);

                Assert.IsNotNull(targetMethod);
                Assert.IsNotNull(addSegmentsMethod);
                Assert.IsNotNull(lineObstaclesField);

                Canvas canvas = new();
                RouterConfiguration configuration = new();

                AStarRouter router = new(canvas, configuration);

                // Add the segments to the router
                addSegmentsMethod.Invoke(router, new object[1] { testCase.Segments });

                // Test point
                Point point = testCase.Point;
                Point relative = testCase.Relative;

                // Test the crossing count
                object? methodResult = targetMethod.Invoke(router, new object[2] { point, relative });

                Assert.IsNotNull(methodResult);
                Assert.IsInstanceOfType(methodResult, typeof(int));

                int result = (int)methodResult;
                Assert.AreEqual(result, testCase.CorrectResult);

                Console.WriteLine($"T {Environment.CurrentManagedThreadId}, R {result}, C {testCase.CorrectResult}");
            });
        }
    }
}