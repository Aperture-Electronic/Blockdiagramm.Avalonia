using Avalonia;
using Avalonia.Collections;
using Blockdiagramm.Extensions;
using Blockdiagramm.Logic.Exceptions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockdiagramm.Logic
{
    public partial class Project
    {
        #region Public readonly properties
        public AvaloniaList<SourceFile> SourceFiles { get; } = new();
        #endregion

        public void AddSources(string[] paths, SourceFileType type = SourceFileType.Auto)
        {
            // Create new source file object for the paths
            // and add them to the project
            int alreadyExistCount = 0;
            foreach (string path in paths)
            {
                try
                {
                    var sourceFile = new SourceFile(path, type);
                    AddSource(sourceFile);
                }
                catch (SourceAlreadyInProjectException)
                {
                    alreadyExistCount++;
                }
                catch
                {
                    throw;
                }
            }

            // All files existed
            if (alreadyExistCount == paths.Length)
            {
                throw new Exception("All source files are already exist in project");
            }
        }

        public void AddSource(SourceFile sourceFile)
        {
            // Check the project valid or not
            if (!IsValid)
            {
                throw new Exception("The project is not opened");
            }

            // Check the source valid or not
            if (!File.Exists(sourceFile.FilePath))
            {
                throw new FileNotFoundException("The source file is not exist", sourceFile.FilePath);
            }

            // Check the hash value in the file list
            var query = from file in SourceFiles
                        where file.Hash.Equals(sourceFile.Hash)
                        select file;

            if (query.Any())
            {
                throw new SourceAlreadyInProjectException(sourceFile);
            }

            // Add the file to the list
            SourceFiles.Add(sourceFile);
        }
    }
}

