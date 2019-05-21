using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ApiGenerator.Domain
{
	public class HighLevelClientMethod
	{
		public CsharpNames CsharpNames { get; set; }
		public bool HasBody { get; set; }

		public string MethodName => CsharpNames.MethodName;
		public string ResponseName => CsharpNames.ResponseName;
		public string RequestGenerics => CsharpNames.GenericsDeclaredOnRequest;

	}
}
