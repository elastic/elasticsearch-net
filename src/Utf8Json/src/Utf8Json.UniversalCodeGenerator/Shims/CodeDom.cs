using System;
using System.Collections.Generic;
using System.Text;

namespace System.CodeDom.Compiler
{
    public class CompilerError
    {
        public string ErrorText { get; set; }
        public bool IsWarning { get; set; }
    }

    public class CompilerErrorCollection
    {
        public void Add(CompilerError error)
        {
        }
    }
}
