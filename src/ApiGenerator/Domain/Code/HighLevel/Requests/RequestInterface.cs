using System.Collections.Generic;
using ApiGenerator.Configuration;
using ApiGenerator.Domain.Specification;

namespace ApiGenerator.Domain.Code.HighLevel.Requests 
{
	public class RequestInterface
	{
		public IReadOnlyCollection<UrlPart> UrlParts { get; set; }

		/// <summary>
		/// Partial parameters are query string parameters we prefer to send over the body of a request.
		/// We declare these on the generated interfaces so that we don't forget to implement them in our request
		/// implementations
		/// </summary>
		public IReadOnlyCollection<QueryParameters> PartialParameters { get; set; }
		
		public string OfficialDocumentationLink { get; set; }
		
		public CsharpNames CsharpNames { get; set; }

		private bool GenerateOnlyGenericInterface => CodeConfiguration.GenericOnlyInterfaces.Contains(CsharpNames.RequestInterfaceName);

		public bool NeedsGenericInterface => !GenerateOnlyGenericInterface && !string.IsNullOrWhiteSpace(CsharpNames.GenericsDeclaredOnRequest);

		public string Name => CsharpNames.GenericOrNonGenericInterfacePreference;
	}
}