using System.Collections.Generic;

namespace ApiGenerator.Domain
{
	public class CsharpMethod
	{
		public CsharpNames CsharpNames { get; set; }
		
		public string Arguments { get; set; }
		public string OfficialDocumentationLink { get; set; }
		public string PerPathMethodName { get; set; }
		public string HttpMethod { get; set; }

		public string ObsoleteMethodVersion { get; set; }
		public UrlInformation Url { get; set; }
		public bool HasBody { get; set; }
		public IEnumerable<UrlPart> Parts { get; set; }
		public string Path { get; set; }

		public string ReturnType { get; set; }

	}
}
