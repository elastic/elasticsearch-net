using System;
using System.Collections.Generic;
using System.Text;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using static Tests.Core.Serialization.SerializationTestHelper;

namespace Tests.Reproduce
{
	public class GithubIssue4044
	{
		[U]
		[UseCulture("sv-SE")]
		public void PercentilesAggregateSerializesWithInvariantCulture()
		{
			const string percentilesString = @"{
				""test_percentiles#pp"": {
					""values"": {
						""42.42"": 42.42
					}
				}
			}";

			Expect(percentilesString).NoRoundTrip().DeserializesTo<AggregateDictionary>((s, val) =>
			{
				val.Count.Should().Be(1);
			});
		}
	}
}
