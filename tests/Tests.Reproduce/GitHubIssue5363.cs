// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using Elasticsearch.Net.Diagnostics;
using Xunit;

namespace Tests.Reproduce
{
	public class GitHubIssue5363
	{
		internal class TestDiagnosticListener : IObserver<DiagnosticListener>, IDisposable
		{
			private ConcurrentBag<IDisposable> Disposables { get; } = new();

			public Action<IApiCallDetails> OnEnded { get; }

			public TestDiagnosticListener(Action<IApiCallDetails> onEnded) => OnEnded = onEnded;

			public void OnError(Exception error) { }
			public void OnCompleted() { }

			public void OnNext(DiagnosticListener value) =>
				TrySubscribe(DiagnosticSources.RequestPipeline.SourceName,
					() => new RequestPipelineDiagnosticObserver(null, v => OnEnded(v.Value)), value);

			private void TrySubscribe(string sourceName, Func<IObserver<KeyValuePair<string, object>>> listener, DiagnosticListener value)
			{
				if (value.Name != sourceName)
					return;
				var d = value.Subscribe(listener());

				Disposables.Add(d);
			}

			public void Dispose()
			{
				foreach (var d in Disposables)
				{
					d.Dispose();
				}
			}
		}

		[U]
		public async Task DiagnosticListener_AuditTrailIsValid()
		{
			using var listener = new TestDiagnosticListener(data =>
			{
				var auditTrailEvent = data.AuditTrail[0];
				
				Assert.True(auditTrailEvent.Ended != default);
			});
			
			using var foo = DiagnosticListener.AllListeners.Subscribe(listener);

			var connectionPool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
			var settings = new ConnectionConfiguration(connectionPool, new InMemoryConnection());

			var client = new ElasticLowLevelClient(settings);
			var person = new { Id = "1" };

			await client.IndexAsync<BytesResponse>("test-index", PostData.Serializable(person));
		}
	}
}
