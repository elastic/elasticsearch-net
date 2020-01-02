using System.Collections.Generic;
using ApiGenerator.Domain.Specification;

namespace ApiGenerator.Domain.Code.HighLevel.Requests 
{
	public class RequestParameterImplementation
	{
		public CsharpNames CsharpNames { get; set; }
		public string OfficialDocumentationLink { get; set; }
		public IReadOnlyCollection<QueryParameters> Params { get; set; }
		public string HttpMethod { get; set; }
	}
}