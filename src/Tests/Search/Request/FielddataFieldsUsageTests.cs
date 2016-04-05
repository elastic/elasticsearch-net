using System;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;

namespace Tests.Search.Request
{
	/** Allows to return the field data representation of a field for each hit.
	*
	* See the Elasticsearch documentation on {ref_current}/search-request-fielddata-fields.html[Field Data Fields] for more detail.
	*/
	public class FielddataFieldsUsageTests : SearchUsageTestBase
	{
		public FielddataFieldsUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			fielddata_fields = new[] { "name", "leadDeveloper", "startedOn" }
		};

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.FielddataFields(fs => fs
				.Field(p => p.Name)
				.Field(p => p.LeadDeveloper)
				.Field(p => p.StartedOn)
			);

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>
			{
				FielddataFields = new string [] { "name", "leadDeveloper", "startedOn" }
			};
	}
}
