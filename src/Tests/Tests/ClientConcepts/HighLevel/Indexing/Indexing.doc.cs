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
		* ==== Multiple Documents
		*
		* Multiple documents can be indexed using the bulk methods, again either synchronously or asynchronously
		*/
		public async Task BulkDocuments()
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

			var bulkIndexResponse = client.IndexMany(people); //<1> synchronous method that returns an `IBulkResponse`

			//<2> the response can be inspected for its success.
			var hasErrors = bulkIndexResponse.Errors;
			if (hasErrors)
			{
				//<3> If there are errors, they can be enumerated and inspected.
				foreach (var itemWithError in bulkIndexResponse.ItemsWithErrors)
				{
					Console.WriteLine("Failed to index document {0}: {1}", itemWithError.Id, itemWithError.Error);
				}
			}

			// Alternatively, documents can be indexed asynchronously
			var asyncBulkIndexResponse = await client.IndexManyAsync(people); //<4> asynchronous method that returns a `Task<IBulkResponse>` that can be awaited
		}

		/**
		* ==== Multiple Documents with BulkAll helper.
		*
		* Multiple documents can be indexed using the BulkAll helper. This helper exposes retry and backoff functionality
		* to automatically retry in the event of a failure. This allows you to focus on the overall objective of indexing,
		* without having to concern yourself with retry mechanics. Of course, if there is an eventual failure you have
		* the ability to inspect any exceptions and indexing progress.
		*/
		public async Task BulkDocumentsWithHelper()
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

			// <4> register an observer to be notified of bulk events
			bulkAllObservable.Subscribe(new BulkAllObserver(
				onNext: b =>
				{
					//<5> do something e.g. write number of pages to console
				},
				onError: e =>
				{
					//<6> capture the exception into the local variable; do not throw as it will be swallowed
					exception = e;
					waitHandle.Set();
				},
				onCompleted: () => waitHandle.Set()));

			// <7> wait for indexing
			waitHandle.WaitOne();

			// <8> if there was an exception, throw it now
			if (exception != null)
				throw exception;
		}
	}
}
