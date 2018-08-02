using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Tests.Framework;

namespace Tests.Reproduce
{
	public class GithubIssue2690
	{
		[U] public void EmptyPolicyCausesNullReferenceException()
		{
			var client = TestClient.DefaultInMemoryClient;
			var response = client.CreateIndex("foo", c => c
				.Settings(s => s
					.Merge(m => m
						.Scheduler(sch => sch.MaxThreadCount(1))
					)
				)
			);
			response.ShouldBeValid();
		}
	}
}
