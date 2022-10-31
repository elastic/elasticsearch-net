// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch.Aggregations;
using Tests.Serialization;
using VerifyXunit;

namespace Tests.Aggregations;

[UsesVerify]
public class TermsIncludeSerializationTests : SerializerTestBase
{
	[U]
	public async Task RoundTripSerialize_TermsIncludeWithRegexPattern()
	{
		const string pattern = "water_.*";

		var target = new TestClass
		{
			Include = new TermsInclude(pattern)
		};

		var result = await RoundTripAndVerifyJsonAsync(target);

		result.Include.RegexPattern.Should().Be(pattern);
		result.Include.Values.Should().BeNull();
		result.Include.Partition.Should().BeNull();
		result.Include.NumberOfPartitions.Should().BeNull();
	}

	[U]
	public async Task RoundTripSerialize_TermsIncludeWithValues()
	{
		var values = new[] { "term_a", "term_b" };

		var target = new TestClass
		{
			Include = new TermsInclude(values)
		};

		var result = await RoundTripAndVerifyJsonAsync(target);

		result.Include.RegexPattern.Should().BeNull();
		result.Include.Values.Should().Contain(values);
		result.Include.Partition.Should().BeNull();
		result.Include.NumberOfPartitions.Should().BeNull();
	}

	[U]
	public async Task RoundTripSerialize_TermsIncludeWithPartition()
	{
		var target = new TestClass
		{
			Include = new TermsInclude(10, 100)
		};

		var result = await RoundTripAndVerifyJsonAsync(target);

		result.Include.RegexPattern.Should().BeNull();
		result.Include.Values.Should().BeNull();
		result.Include.Partition.Should().Be(10);
		result.Include.NumberOfPartitions.Should().Be(100);
	}

	private class TestClass
	{
		public TermsInclude Include { get; set; }
	}
}
