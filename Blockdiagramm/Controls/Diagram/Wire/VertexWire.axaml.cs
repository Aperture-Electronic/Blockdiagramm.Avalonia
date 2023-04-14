using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Blockdiagramm.Extensions;
using Blockdiagramm.Renderer.Wiring.Router;
using Blockdiagramm.ViewModels.Diagram.Wire;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Threading.Tasks;

namespace Blockdiagramm.Controls.Diagram.Wire
{
    public partial class VertexWire : UserControl, ILineObstacle
    {
        public static readonly DirectProperty<VertexWire, AvaloniaList<Point>> VerticesProperty =
            AvaloniaProperty.RegisterDirect<VertexWire, AvaloniaList<Point>>(nameof(Vertices), o => o.Vertices);

        public AvaloniaList<Point> Vertices => dplWire.Points;

        public VertexWire()
        {
            InitializeComponent();
        }
    
        public void ReduceVertices()
        {
            int verticesCount = Vertices.Count;

            for (int i = 2; i < verticesCount;)
            {
                // Delete if 3 vertices are on the same line
                if (Vertices[i - 1].IsOnLine(Vertices[i - 2], Vertices[i]))
                {
                    Vertices.RemoveAt(i - 1);
                    verticesCount--;
                }
                else
                {
                    i++;
                }
            }
        }

        public Point StartVertex => Vertices.First();
        public Point EndVertex => Vertices.Last();

        public WireModel Model
        {
            get
            {
                if (DataContext is not WireModel model)
                {
                    throw new Exception("The wire has not a valid data context of wire model");
                }

                return model;
            }
        }

        public bool IsObstacleValid => Model.WireStatus != WireStatus.Temporary;

        #region Line obstacle methods that already gived up
        //public bool IsOnLine(Point point, double threshold = double.Epsilon)
        //{
        //    int count = Vertices.Count;

        //    for (int i = 1; i < count; i++)
        //    {
        //        Point startPoint = Vertices[i - 1];
        //        Point endPoint = Vertices[i];

        //        if (point.IsOnOrthogonalSegment(startPoint, endPoint))
        //        {
        //            return true;
        //        }
        //    }

        //    //var result = Parallel.For(1, count, delegate (int i, ParallelLoopState state)
        //    //{
        //    //    Point startPoint = Vertices[i - 1];
        //    //    Point endPoint = Vertices[i];

        //    //     if (point.IsOnSegment(startPoint, endPoint, threshold))
        //    //    if (point.IsOnOrthogonalSegment(startPoint, endPoint))
        //    //    {
        //    //        state.Break();
        //    //    }
        //    //});

        //    //if (!result.IsCompleted)
        //    //{
        //    //    return true;
        //    //}

        //    return false;
        //}

        //public bool IsOverSegment(Point point, Point relative, double threshold = double.Epsilon)
        //{
        //    int count = Vertices.Count;

        //    for (int i = 1; i < count; i++)
        //    {
        //        Point startPoint = Vertices[i - 1];
        //        Point endPoint = Vertices[i];

        //        // if (point.IsSegmentOverSegment(relative, startPoint, endPoint, threshold))
        //        if (point.IsOrthogonalSegmentOverOrthogonalSegment(relative, startPoint, endPoint))
        //        {
        //            return true;
        //        }
        //    }
        //    //var result = Parallel.For(1, count, delegate (int i, ParallelLoopState state)
        //    //{
        //    //    Point startPoint = Vertices[i - 1];
        //    //    Point endPoint = Vertices[i];

        //    //    if (point.IsSegmentOverSegment(relative, startPoint, endPoint, threshold))
        //    //    {
        //    //        state.Break();
        //    //    }
        //    //});

        //    //if (!result.IsCompleted)
        //    //{
        //    //    return true;
        //    //}

        //    return false;
        //}

        //public bool IsCrossSegment(Point point, Point relative, double threshold = double.Epsilon)
        //{
        //    int count = Vertices.Count;

        //    for (int i = 1; i < count; i++)
        //    {
        //        Point startPoint = Vertices[i - 1];
        //        Point endPoint = Vertices[i];

        //        if (point.IsCrossSegment(relative, startPoint, endPoint, threshold))
        //        {
        //            return true;
        //        }
        //    }

        //    return false;
        //}
        #endregion

        public IEnumerable<OrthogonalSegment> Segments
        {
            get
            {
                int count = Vertices.Count;

                for (int i = 1; i < count; i++)
                {
                    yield return new OrthogonalSegment(Vertices[i - 1], Vertices[i]);
                }
            }
        }

        public void Translate(Point delta)
        {
            int count = Vertices.Count;

            for (int i = 0; i < count; i++)
            {
                Vertices[i] += delta;
            }
        }
    }
}
