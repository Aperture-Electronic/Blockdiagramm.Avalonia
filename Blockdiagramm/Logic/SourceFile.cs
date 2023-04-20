using Avalonia.Media;
using Blockdiagramm.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Blockdiagramm.Logic
{
    public class SourceFile
    {
        /// <summary>
        /// The hash of source file calculated by the SHA-256 hash algorithm
        /// </summary>
        public string Hash { get; }

        /// <summary>
        /// Path to the corresponding source file
        /// </summary>
        public string FilePath { get; }

        /// <summary>
        /// Type of the source file.
        /// This will determine how to interpret the source
        /// </summary>
        public SourceFileType Type { get; }

        /// <summary>
        /// The abbreviate string of the file type, only for display
        /// </summary>
        public string TypeAbbreviate => Type.Abbreviate();

        /// <summary>
        /// The theme color of the file type, only for display
        /// </summary>
        public IBrush TypeThemeColor => Type.ThemeColor();

        /// <summary>
        /// The short name of the file (without the path)
        /// </summary>
        public string ShortName => Path.GetFileName(FilePath);

        /// <summary>
        /// Get the relative location (only path) of the file
        /// </summary>
        /// <param name="projectPath">Project path relative to</param>
        public string GetRelativeLocation(string projectPath) => Path.GetDirectoryName(GetRelativePath(projectPath)) ?? "";

        /// <summary>
        /// Get the relative path (with file name) of the file
        /// </summary>
        /// <param name="projectPath">Project path relative to</param>
        public string GetRelativePath(string projectPath) => Path.GetRelativePath(projectPath, FilePath);

        private static SourceFileType GetTypeByExtension(string filePath) => Path.GetExtension(filePath) switch
        {
            "v" => SourceFileType.VerilogSource,
            "sv" => SourceFileType.SystemVerilogSource,
            "svh" => SourceFileType.SystemVerilogHeader,
            "vhd" => SourceFileType.VHDLSource,
            _ => SourceFileType.SystemVerilogSource // Default
        };


        public SourceFile(string filePath) : this(filePath, GetTypeByExtension(filePath))
        { }

        public SourceFile(string filePath, SourceFileType type)
        {
            FilePath = filePath;
            Type = type == SourceFileType.Auto ? GetTypeByExtension(filePath) : type;

            // Calculate the path hash
            byte[] hashValue = SHA256.HashData(Encoding.UTF8.GetBytes(filePath));
            Hash = Convert.ToBase64String(hashValue);
        }

        /// <summary>
        /// Get if the file existed
        /// </summary>
        public bool Exist => !string.IsNullOrEmpty(FilePath) && File.Exists(FilePath);

        /// <summary>
        /// Content of the file (this property will not be serialize)
        /// </summary>
        [JsonIgnore, IgnoreDataMember]
        public string Content { get; private set; } = string.Empty;

        /// <summary>
        /// Read and update the file content
        /// </summary>
        /// <exception cref="FileNotFoundException">The target file is not on the disk</exception>
        public async Task ReadFileContent()
        {
            if (!Exist)
            {
                throw new FileNotFoundException("The source file is not found", FilePath);
            }

            using FileStream fileStream = File.OpenRead(FilePath);
            using StreamReader streamReader = new(fileStream);
            Content = await streamReader.ReadToEndAsync();
            streamReader.Close();
        }

        [JsonIgnore, IgnoreDataMember]
        public string ContentHash
        {
            get
            {
                // Get hash of content by SHA-256
                byte[] hash = SHA256.HashData(Encoding.UTF8.GetBytes(Content));
                return Convert.ToBase64String(hash);
            }
        }

        public override string ToString() => $"{FilePath}({Hash}) :: {Type}";
    }
}
