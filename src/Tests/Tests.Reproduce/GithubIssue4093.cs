using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using static Tests.Core.Serialization.SerializationTestHelper;

namespace Tests.Reproduce
{
	public class GithubIssue4093
	{
		[U] public void TermsIncludeCanBeObjectOtherThanString() => Expect(new
			{
				aggs = new
				{
					sizes = new
					{
						terms = new
						{
							field = "size",
							size = 20,
							include = new[] { 35, 50, 70, 75, 100 }
						}
					}
				}
			})
			.DeserializesTo<SearchRequest>((message, request) =>
			{
				request.Should().NotBeNull(message);
				request.Aggregations.Should().NotBeNull(message).And.HaveCount(1, message);
				var termsAggregation = request.Aggregations["sizes"].Terms;
				termsAggregation.Should().NotBeNull(message);
				termsAggregation.Include.Should().NotBeNull(message);
				termsAggregation.Include.Values.Should().NotBeNull(message).And.HaveCount(5, message);
			});

		[U] public void TermsExcludeCanBeObjectOtherThanString() => Expect(new {
			aggs = new {
				sizes = new {
					terms = new {
						field = "size",
						size = 20,
						exclude = new[] { 35, 50, 70, 75, 100 }
					}
				}
			}
		})
			.DeserializesTo<SearchRequest>((message, request) => {
				request.Should().NotBeNull(message);
				request.Aggregations.Should().NotBeNull(message).And.HaveCount(1, message);
				var termsAggregation = request.Aggregations["sizes"].Terms;
				termsAggregation.Should().NotBeNull(message);
				termsAggregation.Exclude.Should().NotBeNull(message);
				termsAggregation.Exclude.Values.Should().NotBeNull(message).And.HaveCount(5, message);
			});
	}
}
