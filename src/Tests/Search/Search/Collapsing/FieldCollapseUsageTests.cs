using System;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using static Nest.Infer;

namespace Tests.Search.Search.Collapsing
{
	/**
	 */
	public class FieldCollapseUsageTests : SearchUsageTestBase
	{
		public FieldCollapseUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
		};

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Collapse(c => c
				.Field(f => f.State)
				.InnerHits(i => i
					.Name(nameof(StateOfBeing).ToLowerInvariant())
					.Size(5)
					.From(1)
				)
			);

		protected override SearchRequest<Project> Initializer => new SearchRequest<Project>
		{
			Collapse = new FieldCollapse
			{
				Field = Field<Project>(p=>p.State),
				InnerHits = new InnerHits
				{
					Name = nameof(StateOfBeing).ToLowerInvariant(),
					Size = 5,
					From = 1
				}
			}
		};
	}
}
