using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest;
using HackerNews.Indexer.Domain;
using Nest.TestData;
using Nest.TestData.Domain;
using System.Threading;
using NUnit.Framework;
using System.Diagnostics;
using System.Net;

namespace Nest.Tests.Integration
{
	[TestFixture]
	public class AsyncTests : BaseElasticSearchTests
	{

		[Test]
		public void TestIndex()
		{
			var newProject = new ElasticSearchProject 
			{
				Name = "COBOLES", //COBOL ES client ?
			};
			var t = this.ConnectedClient.IndexAsync<ElasticSearchProject>(newProject);
			t.Wait();
			Assert.True(t.Result.Success);
			Assert.True(t.IsCompleted, "task did not complete");
			Assert.True(t.IsCompleted, "task did not complete");
		}
		[Test]
		public void TestIndexTimeout()
		{
			var timeout = 1;
			var s = new ConnectionSettings(Test.Default.Host, Test.Default.Port, timeout)
						.SetDefaultIndex(Test.Default.DefaultIndex)
						.SetMaximumAsyncConnections(Test.Default.MaximumAsyncConnections)
						.UsePrettyResponses();

			var client = new ElasticClient(s);

			var newProject = new ElasticSearchProject
			{
				Name = "COBOLES", //COBOL ES client ?
			};
			var t = client.IndexAsync<ElasticSearchProject>(newProject);
			t.Wait(1000);
			var cs = t.Result;
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
