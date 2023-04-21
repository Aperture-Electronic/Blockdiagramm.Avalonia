using Blockdiagramm.Models;
using DynamicData;
using HDLAbstractSyntaxTree.HDLElement;
using HDLElaborateRoslyn.Elaborator;
using HDLParserSharp;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockdiagramm.Logic
{
    public partial class Project
    {
        private bool elaborating = false;

        public SourceList<ComponentPartModel> Components = new();

        /// <summary>
        /// Elaborate the source file in the project file list
        /// </summary>
        /// <param name="file">Source file instance</param>
        public async Task Elaborate(SourceFile file)
        {
            if (!file.Exist)
            {
                throw new FileNotFoundException($"The file ({file.ShortName}) to elaborate is not exist", file.FilePath);
            }

            // Get the old hash of content
            string oldHash = file.ContentHash;

            // Read the file content and get ready to elaborate
            await file.ReadFileContentAsync();

            // Get the new content hash of the file
            string newHash = file.ContentHash;

            // The hash is same, we don't need to elaborate more
            bool sameHash = oldHash == newHash;

            bool hasElaborated = Components.Items.Any((c) => c.SourceFile == file);

            // Do not elaborate if the file has beed elaborated and added to the list
            if (hasElaborated && sameHash)
            {
                return;
            }

            // Use the language parser to get Abstract Syntax Tree (AST)
            var ast = ParserHDLFileByType(file.Content, file.Type, new());
            HDLElaborator elaborator = new(ast);

            elaborator.ElaborateModules();
            elaborator.GenerateModuleGenericsList();
            elaborator.ElaborateModuleGenerics();
            elaborator.ElaborateModulePort();

            // Make the component and put them into the components list
            // Add the new component to the list
            foreach (var module in elaborator.Modules)
            {
                var moduleQuery = Components.Items.Where((c) => c.SourceFile == file)
                    .Where((c) => c.Name == module.Name);

                if (moduleQuery.Any())
                {
                    var targetComponent = moduleQuery.First();
                    targetComponent.UpdateModule(module);
                }
                else
                {
                    Components.Add(new ComponentPartModel(file, module));
                }
            }
        }

        /// <summary>
        /// Elaborate all source files in the project file list
        /// </summary>
        /// <returns></returns>
        public async Task ElaborateAll()
        {
            foreach (SourceFile file in SourceFiles.Items)
            {
                await Elaborate(file);
            }
        }

        private static List<HDLObject> ParserHDLFileByType(string content, SourceFileType type, HDLEvaluator evaluator)
        {
            List<HDLObject> ast = new();

            switch (type)
            {
                case SourceFileType.SystemVerilogSource:
                    SystemVerilogParserContainer svParser = new(ast, HDLLanguage.SystemVerilog, evaluator.EvalToBool);
                    svParser.ParseString(content);
                    break;
                case SourceFileType.VerilogSource:
                    // TODO
                    break;
                case SourceFileType.VHDLSource:
                    // TODO
                    break;
                case SourceFileType.SystemVerilogHeader:
                    // TODO
                    break;
                default:
                    break;
            }

            return ast;
        }
    }
}
