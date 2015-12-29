using System;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;
using static Nest.Infer;

namespace Tests.Search.FieldStats
{
	[Collection(IntegrationContext.ReadOnly)]
	public class FieldStatsApiTests
		: ApiIntegrationTestBase<IFieldStatsResponse, IFieldStatsRequest, FieldStatsDescriptor, FieldStatsRequest>
	{
		public FieldStatsApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (c, f) => c.FieldStats(typeof(Project), f),
			fluentAsync: (c, f) => c.FieldStatsAsync(typeof(Project), f),
			request: (c, r) => c.FieldStats(r),
			requestAsync: (c, r) => c.FieldStatsAsync(r)
		);

		protected override int ExpectStatusCode => 200;
		protected override bool ExpectIsValid => true;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => "/project/_field_stats";

		protected override Func<FieldStatsDescriptor, IFieldStatsRequest> Fluent => d => d
			.Fields(Field<Project>(p => p.Name));

			// Causes a NPE on ES 2.0.0
			//.IndexConstraints(cs => cs
			//	.IndexConstraint(Field<Project>(p => p.StartedOn), c => c
			//		.MinValue(min => min
			//			.GreaterThanOrEqualTo("2014-01-01")
			//			.Format("date_optional_time")
			//		)
			//		.MaxValue(max => max
			//			.LessThan("2015-12-29")
			//			.Format("date_optional_time")
			//		)
			//	)
			//);

		protected override FieldStatsRequest Initializer => new FieldStatsRequest(typeof(Project))
		{
			Fields = Field<Project>(p => p.Name)

			// Causes a NPE on ES 2.0.0
			//IndexConstraints = new IndexConstraints
			//{
			//	{
			//		Field<Project>(p => p.StartedOn),
			//		new IndexConstraint
			//		{
			//			MinValue = new IndexConstraintComparison
			//			{
			//				GreaterThanOrEqualTo = "2014-01-01",
			//				Format = "date_optional_time"
			//			},
			//			MaxValue = new IndexConstraintComparison
			//			{
			//				LessThan = "2015-12-29",
			//				Format = "date_optional_time"
			//			}
			//		}
			//	}
			//}
		};
	}
}
