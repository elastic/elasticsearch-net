using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.RoundTripper;

namespace Tests.Reproduce
{
	public class GithubIssue2875
	{

		[U] public void ReusingQueryDescriptorOutSideOfSelector()
		{
			Expect(new
			{
				query = new {
					@bool = new
					{
						must = new []
						{
							new  { term = new { field = new { value = "value" } }}
						}

					}
				}
			}).FromRequest(ReuseQueryDescriptorUnexpected);
		}

		private static ISearchResponse<Project> ReuseQueryDescriptorUnexpected(IElasticClient client) => client.Search<Project>(s => s
			.Query(q => q
				.Bool(b => b
					.Must(q.Term("field", "value"))
				)
			)
		);
	}
}
