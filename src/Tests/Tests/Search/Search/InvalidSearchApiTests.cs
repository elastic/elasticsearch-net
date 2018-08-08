using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Xunit;

namespace Tests.Search.Search
{
	public class InvalidSearchApiTests : SearchApiTests
	{
		public InvalidSearchApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => false;

		protected override int ExpectStatusCode => 400;

		protected override object ExpectJson => new
		{
			from = 10,
			size = 20,
			aggs = new
			{
				startDates = new
				{
					date_range = new
					{
						ranges = new[]
						{
							new { from = "-1m" }
						}
					}
				}
			}
		};

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.From(10)
			.Size(20)
			.Aggregations(a => a
				.DateRange("startDates", t => t
					.Ranges(ranges => ranges.From("-1m"))
				)
			);

		protected override SearchRequest<Project> Initializer => new SearchRequest<Project>()
		{
			From = 10,
			Size = 20,
			Aggregations = new DateRangeAggregation("startDates")
			{
				Ranges = new List<DateRangeExpression>
				{
					new DateRangeExpression { From = "-1m" }
				}
			}
		};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldNotBeValid();
			var serverError = response.ServerError;
			serverError.Should().NotBeNull();
			serverError.Status.Should().Be(400);
			serverError.Error.Reason.Should().Be("all shards failed");
			serverError.Error.RootCause.First().Reason.Should().Contain("[-1m]");

		}
	}
}
