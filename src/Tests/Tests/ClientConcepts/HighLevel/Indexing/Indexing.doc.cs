using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;

namespace Tests.ClientConcepts.HighLevel.Caching
{
	/**[[indexing]]
	*=== Indexing
	*
	* NEST has a number of ways in which documents can be indexed.
	*/
	public class Indexing : DocumentationTestBase
	{
		private readonly IElasticClient client = new ElasticClient(new ConnectionSettings(new SingleNodeConnectionPool(new Uri("http://localhost:9200")), new InMemoryConnection()));

		/// hide
		public class Person
		{
			public int Id { get; set; }
			public string FirstName { get; set; }
			public string LastName { get; set; }
		}

		/**
		* ==== Single Documents
		* A single document can be indexed at a time, either synchronously or asynchronously
		*/
		public async Task SingleDocument()
		{
			var person = new Person
			{
				Id = 1,
				FirstName = "Martijn",
				LastName = "Laarman"
			};

			var indexResponse = client.IndexDocument(person); //<1> synchronous method that returns an `IIndexResponse`

			var asyncIndexResponse = await client.IndexDocumentAsync(person); //<2> asynchronous method that returns a `Task<IIndexResponse>` that can be awaited
		}

		/**
		* ==== Multiple Documents with IndexMany
		*
		* Multiple documents can be indexed using the `IndexMany` and `IndexManyAsync` methods, again either synchronously or asynchronously.
		* These methods are specific to the NEST client and wrap calls to the `_bulk` endpoint, providing a convenient shortcut to indexing
		* multiple documents.
		*/
		public async Task IndexManyDocuments()
		{
			var people = new []
			{
				new Person
				{
					Id = 1,
					FirstName = "Martijn",
					LastName = "Laarman"
				},
				new Person
				{
					Id = 2,
					FirstName = "Stuart",
					LastName = "Cam"
				},
				new Person
				{
					Id = 3,
					FirstName = "Russell",
					LastName = "Cam"
				}
			};

			var indexManyResponse = client.IndexMany(people); //<1> synchronous method that returns an `IBulkResponse`

			if (indexManyResponse.Errors) //<2> the response can be inspected for its success
			{
				//<3> If there are errors, they can be enumerated and inspected
				foreach (var itemWithError in indexManyResponse.ItemsWithErrors)
				{
					Console.WriteLine("Failed to index document {0}: {1}", itemWithError.Id, itemWithError.Error);
				}
			}

			// Alternatively, documents can be indexed asynchronously
			var indexManyAsyncResponse = await client.IndexManyAsync(people); //<4> asynchronous method that returns a `Task<IBulkResponse>` that can be awaited
		}

		/**
		* ==== Multiple Documents with Bulk
		*
		* If you require finer grained control over bulk indexing you can use the `Bulk` and `BulkAsync` methods and use the descriptors to
		* customise the bulk calls.
		*/
		public async Task BulkIndexDocuments()
		{
			//hide
			var people = new [] { new Person { Id = 1, FirstName = "Martijn", LastName = "Laarman" } };

			var bulkIndexResponse = client.Bulk(b => b
				.Index("people")
				.IndexMany(people)); //<1> synchronous method that returns an `IBulkResponse`, the same as `IndexMany` and can be inspected in the same way for errors

			// Alternatively, documents can be indexed asynchronously similar to `IndexManyAsync`
			var asyncBulkIndexResponse = await client.BulkAsync(b => b
				.Index("people")
				.IndexMany(people)); //<4> asynchronous method that returns a `Task<IBulkResponse>` that can be awaited
		}

		/**
		* ==== Multiple Documents with BulkAllObservable helper
		*
		* Multiple documents can be indexed using the `BulkAllObservable` helper. This helper exposes retry and backoff functionality
		* to automatically retry in the event of a failure. This allows you to focus on the overall objective of indexing,
		* without having to concern yourself with retry mechanics. Of course, if there is an eventual failure you have
		* the ability to inspect any exceptions and indexing progress.
		*/
		public async Task BulkDocumentsWithObservableHelper()
		{
			var people = new []
			{
				new Person
				{
					Id = 1,
					FirstName = "Martijn",
					LastName = "Laarman"
				},
				new Person
				{
					Id = 2,
					FirstName = "Stuart",
					LastName = "Cam"
				},
				new Person
				{
					Id = 3,
					FirstName = "Russell",
					LastName = "Cam"
				}
			};

			var bulkAllObservable = client.BulkAll(people, b => b
				.Index("people")
				.BackOffTime("30s") //<1> how long to wait between retries
				.BackOffRetries(2) //<2> how many reties should this bulk index attempt is unsuccessful
				.RefreshOnCompleted()
				.MaxDegreeOfParallelism(Environment.ProcessorCount)
				.Size(1000) // <3> items per bulk request
			);

			Exception exception = null;
			var waitHandle = new ManualResetEvent(false);

			bulkAllObservable.Subscribe(new BulkAllObserver( //<4> register an observer to be notified of bulk events
				onNext: b =>
				{
					// Do something e.g. write number of pages to console
				},
				onError: e =>
				{
					exception = e; //<5> capture the exception into the local variable; do not throw as it will be swallowed
					waitHandle.Set();
				},
				onCompleted: () => waitHandle.Set()));

			waitHandle.WaitOne(); //<6> wait for indexing

			if (exception != null) //<7> if there was an exception, throw it now
				throw exception;
		}
	}
}
