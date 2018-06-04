using System.IO;
using System.Text;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Framework;
using static Tests.Framework.RoundTripper;

namespace Tests.Reproduce
{
	public class GithubIssue2503
	{
		[U] public void DeserializeTermsIncludeExcludeValues()
		{
			Expect(new
			{
				aggs = new
				{
					sizes = new
					{
						terms =  new
						{
							field= "size",
							size= 20,
							include= new [] { "35", "50", "70", "75", "100" }
						}
					}
				}
			}).DeserializesTo<SearchRequest>((message, request) =>
			{
				request.Should().NotBeNull(message);
				request.Aggregations.Should().NotBeNull(message).And.HaveCount(1, message);
				var termsAggregation = request.Aggregations["sizes"].Terms;
				termsAggregation.Should().NotBeNull(message);
				termsAggregation.Include.Should().NotBeNull(message);
				termsAggregation.Include.Values.Should().NotBeNull(message).And.HaveCount(5, message);
			});
		}

		[U] public void DeserializeTermsIncludeExcludePattern()
		{
			Expect(@"{
			  ""aggs"": {
				""sizes"": {
				  ""terms"": {
					""field"": ""size"",
					""size"": 20,
					""include"": ""\\d+""
				  }
				}
			  }
			}").DeserializesTo<SearchRequest>((message, request) =>
			{
				request.Should().NotBeNull(message);
				request.Aggregations.Should().NotBeNull(message).And.HaveCount(1, message);
				var termsAggregation = request.Aggregations["sizes"].Terms;
				termsAggregation.Should().NotBeNull(message);
				termsAggregation.Include.Should().NotBeNull(message);
				termsAggregation.Include.Pattern.Should().NotBeNull(message);
			});
		}
	}
}
