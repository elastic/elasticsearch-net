// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using ApiGenerator.Configuration;
using ApiGenerator.Domain.Specification;

namespace ApiGenerator.Domain.Code.HighLevel.Requests
{
	public class RequestPartialImplementation
	{
		public CsharpNames CsharpNames { get; set; }
		public string OfficialDocumentationLink { get; set; }
		public Stability Stability { get; set; }
		public IReadOnlyCollection<UrlPart> Parts { get; set; }
		public IReadOnlyCollection<UrlPath> Paths { get; set; }
		public IReadOnlyCollection<QueryParameters> Params { get; set; }
		public IReadOnlyCollection<Constructor> Constructors { get; set; }
		public IReadOnlyCollection<Constructor> GenericConstructors { get; set; }
		public bool HasBody { get; set; }

		private bool GenerateOnlyGenericInterface => CodeConfiguration.GenericOnlyInterfaces.Contains(CsharpNames.RequestInterfaceName);

		public bool NeedsGenericImplementation => !GenerateOnlyGenericInterface && !string.IsNullOrWhiteSpace(CsharpNames.GenericsDeclaredOnRequest);

		public string Name => CsharpNames.GenericOrNonGenericRequestPreference;

		public string InterfaceName => CsharpNames.GenericOrNonGenericInterfacePreference;
	}
}
