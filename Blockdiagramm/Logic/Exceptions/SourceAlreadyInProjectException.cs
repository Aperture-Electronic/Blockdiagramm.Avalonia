using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockdiagramm.Logic.Exceptions
{
    public class SourceAlreadyInProjectException : Exception
    {
        public SourceFile SourceFile { get; set; }
        public string Hash => SourceFile.Hash;

        public SourceAlreadyInProjectException(SourceFile sourceFile) : base("The source file is already in the list")
        {
            SourceFile = sourceFile;
        }
    }
}
