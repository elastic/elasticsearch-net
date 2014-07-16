using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Core
{
	[TestFixture]
	public class AsyncTests : IntegrationTests
	{

		[Test]
		public void TestIndex()
		{
			var newProject = new ElasticsearchProject
			{
				Name = "COBOLES", //COBOL ES client ?
			};
			var t = this.Client.IndexAsync<ElasticsearchProject>(newProject);
			t.Wait();
			Assert.True(t.Result.IsValid);
			Assert.True(t.IsCompleted, "task did not complete");
			Assert.True(t.IsCompleted, "task did not complete");
		}
		
	}
}
