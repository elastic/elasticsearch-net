// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch.Types.Aggregations;
using Tests.Serialization;
using VerifyXunit;

namespace Tests.Types;

[UsesVerify]
public class TermsExcludeSerializationTests : SerializerTestBase
{
	[U]
	public async Task RoundTripSerialize_TermsExlucdeWithRegexPattern()
	{
		const string pattern = "water_.*";

		var target = new TestClass
		{
			Exclude = new TermsExclude(pattern)
		};

		var result = await RoundTripAndVerifyJsonAsync(target);

		result.Exclude.RegexPattern.Should().Be(pattern);
		result.Exclude.Values.Should().BeNull();
	}

	[U]
	public async Task RoundTripSerialize_TermsExlucdeWithValues()
	{
		var values = new[] { "term_a", "term_b" };

		var target = new TestClass
		{
			Exclude = new TermsExclude(values)
		};

		var result = await RoundTripAndVerifyJsonAsync(target);

		result.Exclude.RegexPattern.Should().BeNull();
		result.Exclude.Values.Should().Contain(values);
	}

	private class TestClass
	{
		public TermsExclude Exclude { get; set; }
	}
}
