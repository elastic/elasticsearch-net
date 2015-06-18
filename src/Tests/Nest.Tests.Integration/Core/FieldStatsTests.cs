using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using FluentAssertions;

namespace Nest.Tests.Integration.Core
{
	[TestFixture]
	public class FieldStatsTests : IntegrationTests
	{
		private void Assert(FieldStatsField fields)
		{
			fields.DocCount.Should().BeGreaterThan(0);
			fields.MaxDoc.Should().BeGreaterThan(0);
			fields.Density.Should().BeGreaterThan(0);
			fields.SumDocumentFrequency.Should().BeGreaterThan(0);
			fields.MinValue.Should().NotBeNullOrEmpty();
			fields.MaxValue.Should().NotBeNullOrEmpty();
		}

		[Test]
		[SkipVersion("0 - 1.5.9", "Field stats added in ES 1.6")]
		public void FieldStatsClusterLevel()
		{
			var r = this.Client.FieldStats(fs => fs.Fields("name", "country"));
			r.IsValid.Should().BeTrue();
			r.Shards.Should().NotBeNull();
			r.Shards.Successful.Should().BeGreaterThan(0);
			r.Indices.Should().NotBeEmpty();
			r.Indices.Should().ContainKey("_all");
			var all = r.Indices["_all"];
			all.Fields.Should().NotBeNull();
			all.Fields.Should().ContainKey("name");
			var nameFieldStats = all.Fields["name"];
			Assert(nameFieldStats);
			all.Fields.Should().ContainKey("country");
			var countryFieldStats = all.Fields["country"];
			Assert(countryFieldStats);
		}

		[Test]
		[SkipVersion("0 - 1.5.9", "Field stats added in ES 1.6")]
		public void FieldStatsIndicesLevel()
		{
			var r = this.Client.FieldStats(fs => fs
				.Index(ElasticsearchConfiguration.DefaultIndex)
				.Fields("name")
				.Level(Elasticsearch.Net.Level.Indices)
			);
			r.IsValid.Should().BeTrue();
			r.Shards.Should().NotBeNull();
			r.Shards.Successful.Should().BeGreaterThan(0);
			r.Indices.Should().NotBeEmpty();
			r.Indices.Should().ContainKey(ElasticsearchConfiguration.DefaultIndex);
			var defaultIndex = r.Indices[ElasticsearchConfiguration.DefaultIndex];
			defaultIndex.Fields.Should().NotBeNull();
			defaultIndex.Fields.Should().ContainKey("name");
			Assert(defaultIndex.Fields["name"]);
		}
	}
}
