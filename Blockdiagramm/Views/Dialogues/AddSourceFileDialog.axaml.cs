using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Blockdiagramm.Extensions;
using Blockdiagramm.Logic;
using Blockdiagramm.ViewModels;
using Blockdiagramm.ViewModels.Dialogues;
using HDLAbstractSyntaxTree.Statement;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;

namespace Blockdiagramm.Views.Dialogues
{
    public partial class AddSourceFileDialog : DialogBase<AddSourceFileDialogViewModel>
    {
        public AvaloniaList<object> SourceFileTypes { get; } = new();

        public AddSourceFileDialog()
        {
            InitializeComponent();

            InitializeSourceFileTypes();

            this.WhenActivated(d =>
            {
                RegisterBaseHandlers(d);

                d(ViewModel!.BrowseFiles.RegisterHandler(BrowseFilesAsync));
                d(ViewModel!.ConfirmAddSource.RegisterHandler(ConfirmAddSource)); 
            });
        }

        private void InitializeSourceFileTypes()
        {
            if (Application.Current == null) return;
            Application app = Application.Current;
            IEnumerable<ResourceInclude> resources = app.Resources.MergedDictionaries.OfType<ResourceInclude>();
            ResourceInclude? localization = resources.FirstOrDefault(x => x.Source?.OriginalString?.Contains('/' + ViewModelBase.LocalizationDictionary) ?? false);

            // Generate the source file types list
            Array types = Enum.GetValues(typeof(SourceFileType));
            foreach (SourceFileType type in types)
            {
                string? typeName = Enum.GetName(typeof(SourceFileType), type);
                if (string.IsNullOrEmpty(typeName))
                {
                    continue;
                }

                // Find the field with corresponding to the language in dynamic resource
                if ((localization != null) && localization.Loaded.TryGetResource($"Strings.{typeName}", out object? value))
                {
                    if (value is string langString)
                    {
                        SourceFileTypes.Add(new EnumWithDisplayName<SourceFileType>(type, langString));
                        continue;
                    }
                }

                SourceFileTypes.Add(type);
            }
        }

        private async Task BrowseFilesAsync(InteractionContext<string, (string, bool, SourceFileType)> args)
        {
            // Set the default path to project path if there is no path pass from UI
            string defaultPath = string.IsNullOrWhiteSpace(args.Input) ?
                GlobalStatic.Project.Path : args.Input;

            OpenFileDialog ofd = new()
            {
                Directory = defaultPath,
                Title = "Select files to add to the project",
                AllowMultiple = true,
                Filters = new List<FileDialogFilter>()
                {
                    new () { Name = "All Design Files", Extensions = new() { "vhd", "vhdl", "sv", "svh", "v" } },
                    new () { Name = "System Verilog Files", Extensions = new() { "sv", "svh" } },
                    new () { Name = "VHDL Files", Extensions = new() { "vhd", "vhdl" } },
                    new () { Name = "Verilog Files", Extensions = new() { "v" } },
                }
            };

            var result = await ofd.ShowAsync(this);
            SourceFileType type = SourceFileType.Auto;
            if (result != null)
            {
                if (result.Length == 1)
                {
                    // Single file, we can detect the file type by the extension
                    string extension = System.IO.Path.GetExtension(result[0]).ToLower();
                    type = extension switch 
                    {
                        ".vhd" or ".vhdl" => SourceFileType.VHDLSource,
                        ".sv" => SourceFileType.SystemVerilogSource,
                        ".svh" => SourceFileType.SystemVerilogHeader,
                        ".v" => SourceFileType.VerilogSource,
                        _ => SourceFileType.Auto
                    };
                }

                args.SetOutput((string.Join(';', result), true, type));
                return;
            }

            args.SetOutput(("", false, SourceFileType.Auto));
        }

        private void ConfirmAddSource(InteractionContext<Unit, bool> args)
        {
            if (DataContext is AddSourceFileDialogViewModel model)
            {
                model.Validate();
                
                if (model.ViewModelValid)
                {
                    Submit();
                    args.SetOutput(true);
                    return;
                }
            }

            args.SetOutput(false);  
        }

        private void Submit() => Close(DataContext);
    }
}
