using System.Collections.Generic;
using System.Linq;

namespace ApiGenerator.Domain
{
	public class CsharpMethod
	{
		public string Arguments { get; set; }
		public string CallTypeGeneric { get; set; }
		public string DescriptorType { get; set; }
		public string DescriptorTypeGeneric { get; set; }
		public string DocumentationUrl { get; set; }
		public string FullName { get; set; }
		public string HttpMethod { get; set; }

		public string InterfaceType => $"I{RequestType}";

		public string InterfaceTypeGeneric =>
			string.IsNullOrEmpty(RequestTypeGeneric) ? null : $"I{RequestType}{RequestTypeGeneric}";

		public string ObsoleteMethodVersion { get; set; }
		public IEnumerable<UrlPart> Parts { get; set; }
		public string Path { get; set; }
		public string QueryStringParamName { get; set; }
		public string RequestType { get; set; }

		public string RequestTypeGeneric { get; set; }
		public bool RequestTypeUnmapped { get; set; }
		public string ReturnDescription { get; set; }
		public string ReturnType { get; set; }
		public string ReturnTypeGeneric { get; set; }

		public bool Unmapped { get; set; }
		public UrlInformation Url { get; set; }
		public bool HasBody { get; set; }


	}
}
