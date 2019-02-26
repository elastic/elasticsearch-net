using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utf8Json.UniversalCodeGenerator;

namespace Utf8Json.CodeGenerator.Generator
{
    public partial class FormatterTemplate
    {
        public string Namespace;
        public ObjectSerializationInfo[] objectSerializationInfos;
    }

    public partial class ResolverTemplate
    {
        public string Namespace;
        public string FormatterNamespace { get; set; }
        public string ResolverName = "GeneratedResolver";
        public IResolverRegisterInfo[] registerInfos;
    }
}
