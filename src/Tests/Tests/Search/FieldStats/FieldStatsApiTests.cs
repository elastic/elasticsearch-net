using System;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.Integration;
using static Nest.Infer;

#pragma warning disable 618

namespace Tests.Search.FieldStats
{
	public class FieldStatsApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, IFieldStatsResponse, IFieldStatsRequest, FieldStatsDescriptor, FieldStatsRequest>
	{
		public FieldStatsApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override int ExpectStatusCode => 200;

		protected override Func<FieldStatsDescriptor, IFieldStatsRequest> Fluent => d => d
			.Fields("*")
			.Level(Level.Indices);

		protected override HttpMethod HttpMethod => HttpMethod.POST;
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
			Fields = Fields("*"),
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

		protected override string UrlPath => "/project/_field_stats?level=indices";

		protected override LazyResponses ClientUsage() => Calls(
			(c, f) => c.FieldStats(typeof(Project), f),
			(c, f) => c.FieldStatsAsync(typeof(Project), f),
			(c, r) => c.FieldStats(r),
			(c, r) => c.FieldStatsAsync(r)
		);

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
