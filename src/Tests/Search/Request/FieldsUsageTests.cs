using System;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using static Nest.Infer;

namespace Tests.Search.Request
{
	public class FieldsUsageTests : SearchUsageTestBase
	{
		public FieldsUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			fields = new[] { "name", "startedOn" }
		};

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Fields(fs => fs
				.Field(p => p.Name)
				.Field(p => p.StartedOn)
			);

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>
			{
				Fields = Fields<Project>(p => p.Name, p => p.StartedOn)
			};
	}
}
