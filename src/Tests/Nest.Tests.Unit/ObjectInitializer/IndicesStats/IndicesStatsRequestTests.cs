using System.Collections.Generic;
using Elasticsearch.Net;
using FluentAssertions;
using Nest.Resolvers;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.ObjectInitializer.IndicesStats
{
	[TestFixture]
	public class IndicesStatsRequestTests : BaseJsonTests
	{
		private readonly IElasticsearchResponse _status;

		public IndicesStatsRequestTests()
		{
			var request = new IndicesStatsRequest()
			{
				CompletionFields = new List<PropertyPathMarker> {"name"},
				FielddataFields = new List<PropertyPathMarker> { Property.Path<ElasticsearchProject>(p=>p.PingIP)},
				Metrics = new [] { IndicesStatsMetric.Completion, IndicesStatsMetric.Fielddata}
			};
			var response = this._client.IndicesStats(request);
			this._status = response.ConnectionStatus;
		}

		[Test]
		public void Url()
		{
			this._status.RequestUrl.Should().EndWith("/_stats/completion%2Cfielddata?completion_fields=name&fielddata_fields=pingIP");
			this._status.RequestMethod.Should().Be("GET");
		}
	}

}
