// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;

namespace Tests.MetaHeader
{
	public class MetaHeaderHelperTests
	{
		[U] public void BulkAllHelperRequestsIncludeExpectedHelperMetaData()
		{
			var pool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));

			// We can avoid specifying response bodies and this still exercises all requests.
			var responses = new List<(int, string)>
			{
				(200, "{}"),
				(200, "{}"),
				(200, "{}")
			};

			var connection = new TestableInMemoryConnection(a =>
				a.RequestMetaData.Single(x => x.Key == "helper").Value.Should().Be("b"), responses);
			var settings = new ConnectionSettings(pool, connection);
			var client = new ElasticClient(settings);

			var documents = CreateLazyStreamOfDocuments(20);

			var observableBulk = client.BulkAll(documents, f => f
				.MaxDegreeOfParallelism(8)
				.BackOffTime(TimeSpan.FromSeconds(10))
				.BackOffRetries(2)
				.Size(10)
				.RefreshOnCompleted()
				.Index("an-index")
			);
			
			var bulkObserver = observableBulk.Wait(TimeSpan.FromMinutes(5), b =>
			{
				foreach (var item in b.Items)
				{
					item.IsValid.Should().BeTrue();
					item.Id.Should().NotBeNullOrEmpty();
				}
			});

			connection.AssertExpectedCallCount();
		}

		[U] public void ScrollAllHelperRequestsIncludeExpectedHelperMetaData()
		{
			var pool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
			
			var responses = new List<(int, string)>
			{
				(200, "{\"_scroll_id\":\"SCROLLID\",\"took\":0,\"timed_out\":false,\"_shards\":{\"total\":1,\"successful\":1,\"skipped\":0,\"failed\":0},\"hits\":{\"total\":{\"value\":0,\"relation\":\"eq\"},\"max_score\":null,\"hits\":[]}}"),
				(200, "{\"_scroll_id\":\"SCROLLID\",\"took\":0,\"timed_out\":false,\"_shards\":{\"total\":1,\"successful\":1,\"skipped\":0,\"failed\":0},\"hits\":{\"total\":{\"value\":1,\"relation\":\"eq\"},\"max_score\":null,\"hits\":[{\"_index\":\"index-a\",\"_type\":\"_doc\",\"_id\":\"ISXw0HYBAJbnbq7-Utq6\",\"_score\":null,\"_source\":{\"name\": \"name-a\"},\"sort\":[0]}]}}"),
				(200, "{\"_scroll_id\":\"SCROLLID\",\"took\":1,\"timed_out\":false,\"terminated_early\":false,\"_shards\":{\"total\":1,\"successful\":1,\"skipped\":0,\"failed\":0},\"hits\":{\"total\":{\"value\":1,\"relation\":\"eq\"},\"max_score\":null,\"hits\":[]}}")
			};

			var connection = new TestableInMemoryConnection(a =>
				a.RequestMetaData.Single(x => x.Key == "helper").Value.Should().Be("s"), responses);
			var settings = new ConnectionSettings(pool, connection);
			var client = new ElasticClient(settings);

			var documents = CreateLazyStreamOfDocuments(20);

			var observableScroll = client.ScrollAll<SmallObject>("5s", 2, s => s.Search(ss => ss.Size(2).Index("index-a")));
			var bulkObserver = observableScroll.Wait(TimeSpan.FromMinutes(5), _ => { });

			connection.AssertExpectedCallCount();
		}

		[U] public void ReindexHelperRequestsIncludeExpectedHelperMetaData()
		{
			var pool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));

			var responses = new List<(int, string)>
			{
				(404, "{}"),
				(200, "{\"index-a\":{\"aliases\":{},\"mappings\":{\"properties\":{\"name\":{\"type\":\"keyword\"}}},\"settings\":{\"index\":{\"routing\":{\"allocation\":{\"include\":{\"_tier_preference\":\"data_content\"}}},\"number_of_shards\":\"1\",\"provided_name\":\"index-a\",\"creation_date\":\"1609823178261\",\"number_of_replicas\":\"1\",\"uuid\":\"2R4H1VfTR5imfmIPkNIIxw\",\"version\":{\"created\":\"7100099\"}}}}}"),
				(200, "{\"acknowledged\":true,\"shards_acknowledged\":true,\"index\":\"index-b\"}"),
				(200, "{\"_scroll_id\":\"SCROLLID\",\"took\":0,\"timed_out\":false,\"_shards\":{\"total\":1,\"successful\":1,\"skipped\":0,\"failed\":0},\"hits\":{\"total\":{\"value\":0,\"relation\":\"eq\"},\"max_score\":null,\"hits\":[]}}"),
				(200, "{\"_scroll_id\":\"SCROLLID\",\"took\":0,\"timed_out\":false,\"_shards\":{\"total\":1,\"successful\":1,\"skipped\":0,\"failed\":0},\"hits\":{\"total\":{\"value\":1,\"relation\":\"eq\"},\"max_score\":null,\"hits\":[{\"_index\":\"index-a\",\"_type\":\"_doc\",\"_id\":\"ISXw0HYBAJbnbq7-Utq6\",\"_score\":null,\"_source\":{\"name\": \"name-a\"},\"sort\":[0]}]}}"),
				(200, "{\"_scroll_id\":\"SCROLLID\",\"took\":1,\"timed_out\":false,\"terminated_early\":false,\"_shards\":{\"total\":1,\"successful\":1,\"skipped\":0,\"failed\":0},\"hits\":{\"total\":{\"value\":1,\"relation\":\"eq\"},\"max_score\":null,\"hits\":[]}}"),
				(200, "{\"took\":4,\"errors\":false,\"items\":[{\"index\":{\"_index\":\"index-b\",\"_type\":\"_doc\",\"_id\":\"ISXw0HYBAJbnbq7-Utq6\",\"_version\":1,\"result\":\"created\",\"_shards\":{\"total\":2,\"successful\":1,\"failed\":0},\"_seq_no\":0,\"_primary_term\":1,\"status\":201}}]}")
			};

			var connection = new TestableInMemoryConnection(a =>
				a.RequestMetaData.Single(x => x.Key == "helper").Value.Should().Be("r"), responses);
			var settings = new ConnectionSettings(pool, connection);
			var client = new ElasticClient(settings);

			var reindexObserver = client.Reindex<SmallObject>(r => r
					.ScrollAll("5s", 2, s => s.Search(ss => ss.Size(2).Index("index-a")))
					.BulkAll(b => b.Size(1).Index("index-b")))
				.Wait(TimeSpan.FromMinutes(1), _ => { });

			connection.AssertExpectedCallCount();
		}

		[U] public void SnapshotHelperRequestsIncludeExpectedHelperMetaData()
		{
			var pool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));

			// We can avoid specifying response bodies and this still exercises all requests.
			var responses = new List<(int, string)>
			{
				(200, "{}"),
				(200, "{}")
			};

			var connection = new TestableInMemoryConnection(a =>
				a.RequestMetaData.Single(x => x.Key == "helper").Value.Should().Be("sn"), responses);
			var settings = new ConnectionSettings(pool, connection);
			var client = new ElasticClient(settings);
			
			var observableSnapshot = new SnapshotObservable(client, new SnapshotRequest("repository-a", "snapshot-a"));
			var observer = new SnapshotObserver(connection);
			using var subscription = observableSnapshot.Subscribe(observer);
		}

		private class SnapshotObserver : IObserver<SnapshotStatusResponse>
		{
			private readonly TestableInMemoryConnection _connection;

			public SnapshotObserver(TestableInMemoryConnection connection) => _connection = connection;

			public void OnCompleted() => _connection.AssertExpectedCallCount();
			public void OnError(Exception error) => throw new NotImplementedException();
			public void OnNext(SnapshotStatusResponse value) { }
		}

		[U] public void RestoreHelperRequestsIncludeExpectedHelperMetaData()
		{
			var pool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));

			// We can avoid specifying response bodies and this still exercises all requests.
			var responses = new List<(int, string)>
			{
				(200, "{}"),
				(200, "{}")
			};

			var connection = new TestableInMemoryConnection(a =>
				a.RequestMetaData.Single(x => x.Key == "helper").Value.Should().Be("sr"), responses);
			var settings = new ConnectionSettings(pool, connection);
			var client = new ElasticClient(settings);

			var observableRestore = new RestoreObservable(client, new RestoreRequest("repository-a", "snapshot-a"));
			var observer = new RestoreObserver(connection);
			using var subscription = observableRestore.Subscribe(observer);
		}

		private class RestoreObserver : IObserver<RecoveryStatusResponse>
		{
			private readonly TestableInMemoryConnection _connection;

			public RestoreObserver(TestableInMemoryConnection connection) => _connection = connection;

			public void OnCompleted() => _connection.AssertExpectedCallCount();
			public void OnError(Exception error) => throw new NotImplementedException();
			public void OnNext(RecoveryStatusResponse value) { }
		}

		protected static IEnumerable<SmallObject> CreateLazyStreamOfDocuments(int count)
		{
			for (var i = 0; i < count; i++)
				yield return new SmallObject { Name = i.ToString() };
		}

		protected class SmallObject
		{
			public string Name { get; set; }
		}

		protected class TestableInMemoryConnection : IConnection
		{
			internal static readonly byte[] EmptyBody = Encoding.UTF8.GetBytes("");

			private readonly Action<RequestData> _perRequestAssertion;
			private readonly List<(int, string)> _responses;
			private int _requestCounter = -1;
			
			public TestableInMemoryConnection(Action<RequestData> assertion, List<(int, string)> responses)
			{
				_perRequestAssertion = assertion;
				_responses = responses;
			}

			public void AssertExpectedCallCount() => _requestCounter.Should().Be(_responses.Count - 1);
			
			async Task<TResponse> IConnection.RequestAsync<TResponse>(RequestData requestData, CancellationToken cancellationToken)
			{
				Interlocked.Increment(ref _requestCounter);
				
				_perRequestAssertion(requestData);

				await Task.Yield(); // avoids test deadlocks

				int statusCode;
				string response;
				
				if (_responses.Count > _requestCounter)
					(statusCode, response) = _responses[_requestCounter];
				else
					(statusCode, response) = (500, (string)null);

				var stream = !string.IsNullOrEmpty(response) ? requestData.MemoryStreamFactory.Create(Encoding.UTF8.GetBytes(response)) : requestData.MemoryStreamFactory.Create(EmptyBody);

				return await ResponseBuilder
					.ToResponseAsync<TResponse>(requestData, null, statusCode, null, stream, RequestData.MimeType, cancellationToken)
					.ConfigureAwait(false);
			}

			TResponse IConnection.Request<TResponse>(RequestData requestData)
			{
				Interlocked.Increment(ref _requestCounter);

				_perRequestAssertion(requestData);

				int statusCode;
				string response;

				if (_responses.Count > _requestCounter)
					(statusCode, response) = _responses[_requestCounter];
				else
					(statusCode, response) = (200, (string)null);

				var stream = !string.IsNullOrEmpty(response) ? requestData.MemoryStreamFactory.Create(Encoding.UTF8.GetBytes(response)) : requestData.MemoryStreamFactory.Create(EmptyBody);

				return ResponseBuilder.ToResponse<TResponse>(requestData, null, statusCode, null, stream, RequestData.MimeType);
			}

			public void Dispose() { }
		}
	}
}
