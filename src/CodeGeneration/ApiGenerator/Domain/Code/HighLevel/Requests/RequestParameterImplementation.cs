using System.Collections.Generic;

namespace ApiGenerator.Domain 
{
	public class RequestParameterImplementation
	{
		public CsharpNames CsharpNames { get; set; }
		public string OfficialDocumentationLink { get; set; }
		public IReadOnlyCollection<QueryParameters> Params { get; set; }
		public string HttpMethod { get; set; }
	}
}