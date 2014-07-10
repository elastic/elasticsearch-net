using Nest.Tests.MockData.Domain;
using NUnit.Framework;
using System.Diagnostics;
using System.Net;

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
			var t = this._client.IndexAsync<ElasticsearchProject>(newProject);
			t.Wait();
			Assert.True(t.Result.IsValid);
			Assert.True(t.IsCompleted, "task did not complete");
			Assert.True(t.IsCompleted, "task did not complete");
		}
		
	}
}
