using System.Collections.Generic;
using ApiGenerator.Domain.Specification;

namespace ApiGenerator.Domain.Code.HighLevel.Requests 
{
	public class RequestPartialImplementation
	{
		public CsharpNames CsharpNames { get; set; }
		public string OfficialDocumentationLink { get; set; }
		public IReadOnlyCollection<UrlPart> Parts { get; set; }
		public IReadOnlyCollection<UrlPath> Paths { get; set; }
		public IReadOnlyCollection<QueryParameters> Params { get; set; }
		public IReadOnlyCollection<Constructor> Constructors { get; set; }
		public IReadOnlyCollection<Constructor> GenericConstructors { get; set; }
		public bool HasBody { get; set; }
	}
}