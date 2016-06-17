using System;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;
using static Nest.Infer;

namespace Tests.Search.FieldStats
{
	[Collection(TypeOfCluster.ReadOnly)]
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
		protected override string UrlPath => "/project/_field_stats?level=indices";

		protected override Func<FieldStatsDescriptor, IFieldStatsRequest> Fluent => d => d
			.Fields(Fields<Project>("*"))
			.Level(Level.Indices);
			// TODO: These seem to never return stats...
			//.IndexConstraints(cs => cs
			//	.IndexConstraint(Field<Project>(p => p.StartedOn), c => c
			//		.MinValue(min => min
			//			.GreaterThanOrEqualTo(Project.Projects.Min(p => p.StartedOn).ToString("yyyy-MM-dd"))
			//			.Format("date_optional_time")
			//		)
			//		.MaxValue(max => max
			//			.LessThan(Project.Projects.Max(p => p.StartedOn).ToString("yyyy-MM-dd"))
			//			.Format("date_optional_time")
			//		)
			//	)
			//);

		protected override FieldStatsRequest Initializer => new FieldStatsRequest(typeof(Project))
		{
			Fields = Fields<Project>("*"),
			Level = Level.Indices,
			// TODO: These seem to never return stats...
			//IndexConstraints = new IndexConstraints
			//{
			//	{
			//		Field<Project>(p => p.StartedOn),
			//		new IndexConstraint
			//		{
			//			MinValue = new IndexConstraintComparison
			//			{
			//				GreaterThanOrEqualTo = Project.Projects.Min(p => p.StartedOn).ToString("yyyy-MM-dd"),
			//				Format = "date_optional_time"
			//			},
			//			MaxValue = new IndexConstraintComparison
			//			{
			//				LessThan = Project.Projects.Max(p => p.StartedOn).ToString("yyyy-MM-dd"),
			//				Format = "date_optional_time"
			//			}
			//		}
			//	}
			//}
		};

		protected override void ExpectResponse(IFieldStatsResponse response)
		{
			foreach (var index in response.Indices)
			{
				var stats = index.Value;
				stats.Fields.Should().NotBeEmpty();
			}
		}
	}
}
