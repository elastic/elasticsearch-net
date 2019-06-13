using System;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Newtonsoft.Json;
using Tests.Core.ManagedElasticsearch.Clusters;

namespace Tests.Reproduce
{
	public class GithubIssue3778 : IClusterFixture<WritableCluster>
	{
		private readonly WritableCluster _cluster;

		public GithubIssue3778(WritableCluster cluster) => _cluster = cluster;

		[I] public void ShouldSerializeAndDeserializeDateRange()
		{
			DateTimeOffset? startDate1 = Convert.ToDateTime("2019/01/01").Date;
			DateTimeOffset? endDate1 = Convert.ToDateTime("2019/04/01").Date;

			var dt = new DateRange { GreaterThanOrEqualTo = startDate1, LessThanOrEqualTo = endDate1 };

			var settings = new JsonSerializerSettings();
			settings.Converters.Add(new Nest.JsonNetSerializer.Converters.HandleNestTypesOnSourceJsonConverter(_cluster.Client.RequestResponseSerializer));

			var serialized = JsonConvert.SerializeObject(dt, Formatting.Indented, settings);
			serialized.Equals(@"{
                             ""gt"": null,
                             ""gte"": ""2019-01-01T00:00:00+11:00"",
                             ""lt"": null,
                             ""lte"": ""2019-04-01T00:00:00+11:00""
                           }");

			var raw = @"{""gt"": null,""gte"": ""2019-01-01T00:00:00-06:00"",""lt"": null,""lte"": ""2019-04-01T00:00:00-05:00""}";
			var deserialized = JsonConvert.DeserializeObject<DateRange>(raw, settings);

			deserialized.GreaterThan.Should().BeNull();
			deserialized.GreaterThanOrEqualTo.Should().Be(Convert.ToDateTime("2019-01-01T00:00:00-06:00"));
			deserialized.LessThan.Should().BeNull();
			deserialized.LessThanOrEqualTo.Should().Be(Convert.ToDateTime("2019-04-01T00:00:00-05:00"));
		}
	}
}
