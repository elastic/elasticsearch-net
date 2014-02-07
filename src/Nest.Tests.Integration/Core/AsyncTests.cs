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
		[Test, Ignore] //figure out a better way to test this.
		public void TestIndexTimeout()
		{
			var timeout = 1;
			var s = new ConnectionSettings(Test.Default.Uri, ElasticsearchConfiguration.DefaultIndex)
						.SetMaximumAsyncConnections(Test.Default.MaximumAsyncConnections)
						.UsePrettyResponses();

			var client = new ElasticClient(s);

			var newProject = new ElasticsearchProject
			{
				Name = "COBOLES", //COBOL ES client ?
			};
			var t = client.IndexAsync<ElasticsearchProject>(newProject);
			t.Wait(1000);
			var r = t.Result;
			Assert.True(r.IsValid);
			Assert.IsNotNullOrEmpty(r.Id);

			var cs = r.ConnectionStatus;
			Assert.False(cs.Success);
			Assert.NotNull(cs.Error);
			Assert.NotNull(cs.Error.OriginalException);
			Trace.WriteLine(cs.Error.OriginalException);
			Assert.IsNotNullOrEmpty(cs.Error.ExceptionMessage);
			Assert.IsTrue(cs.Error.OriginalException is WebException);
			var we = cs.Error.OriginalException as WebException;
			Assert.IsTrue(cs.Error.ExceptionMessage.Contains("The request was canceled"));
			Assert.IsTrue(we.Status == WebExceptionStatus.RequestCanceled);
			Assert.True(t.IsCompleted, "task did not complete");
			Assert.False(t.IsFaulted, "task was faulted, wich means the exception did not cleanly pass to ConnectionStatus");
		}
	}
}
