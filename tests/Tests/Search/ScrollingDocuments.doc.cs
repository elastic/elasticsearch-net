// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Threading;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.DocumentationTests;

namespace Tests.Search
{
	/**=== Scrolling documents
	 *
	 * The scroll API can be used to return a large collection of documents from Elasticsearch.
	 *
	 * NEST exposes the scroll API and an observable scroll implementation that can be used
	 * to write concurrent scroll requests.
	 */
	public class ScrollDocuments : IntegrationDocumentationTestBase, IClusterFixture<ReadOnlyCluster>
	{
		public ScrollDocuments(ReadOnlyCluster cluster) : base(cluster) { }

		// hide
		private void ProcessResponse(ISearchResponse<Project> response) { }

		/**==== Simple use
		 *
		 * The simplest use of the scroll API is to perform a search request with a
		 * scroll timeout, then pass the scroll id returned in each response to
		 * the next request to the scroll API, until no more documents are returned
		 */
		[I]
		public void SimpleUse()
		{
			var searchResponse = Client.Search<Project>(s => s
				.Query(q => q
					.Term(f => f.State, StateOfBeing.Stable)
				)
				.Scroll("10s") // <1> Specify a scroll time for how long Elasticsearch should keep this scroll open on the server side. The time specified should be sufficient to process the response on the client side.
			);

			while (searchResponse.Documents.Any()) // <2> make subsequent requests to the scroll API to keep fetching documents, whilst documents are returned
			{
				ProcessResponse(searchResponse); // <3> do something with the response
				searchResponse = Client.Scroll<Project>("10s", searchResponse.ScrollId);
			}
		}

		/**[[scrollall-observable]]
		 * ==== ScrollAllObservable
		 *
		 * Similar to <<bulkall-observable, `BulkAllObservable`>> for bulk indexing a large number of documents,
		 * NEST exposes an observable scroll implementation, `ScrollAllObservable`, that can be used
		 * to write concurrent scroll requests. `ScrollAllObservable` uses sliced scrolls to split the scroll into
		 * multiple slices that can be consumed concurrently.
		 *
		 * The simplest use of `ScrollAllObservable` is
		 */
		[I]
		public void SimpleScrollAllObservable()
		{
			int numberOfSlices = Environment.ProcessorCount; // <1> See https://www.elastic.co/guide/en/elasticsearch/reference/current/paginate-search-results.html[sliced scroll] documentation for choosing an appropriate number of slices.

			var scrollAllObservable = Client.ScrollAll<Project>("10s", numberOfSlices, sc => sc
				.MaxDegreeOfParallelism(numberOfSlices) // <2> Number of concurrent sliced scroll requests. Usually want to set this to the same value as the number of slices
				.Search(s => s
					.Query(q => q
						.Term(f => f.State, StateOfBeing.Stable)
					)
				)
			);

			scrollAllObservable.Wait(TimeSpan.FromMinutes(10), response => // <3> Total overall time for scrolling **all** documents. Ensure this is a sufficient value to scroll all documents
			{
				ProcessResponse(response.SearchResponse); // <4> do something with the response
			});
		}

		/**
		 * More control over how the observable is consumed can be achieved by writing
		 * your own observer and subscribing to the observable, which will initiate scrolling
		 */
		[I]
		public void ComplexScrollAllObservable()
		{
			int numberOfSlices = Environment.ProcessorCount;

			var scrollAllObservable = Client.ScrollAll<Project>("10s", numberOfSlices, sc => sc
				.MaxDegreeOfParallelism(numberOfSlices)
				.Search(s => s
					.Query(q => q
						.Term(f => f.State, StateOfBeing.Stable)
					)
				)
			);

			var waitHandle = new ManualResetEvent(false);
			ExceptionDispatchInfo info = null;

			var scrollAllObserver = new ScrollAllObserver<Project>(
				onNext: response => ProcessResponse(response.SearchResponse), // <1> do something with the response
				onError: e =>
				{
					info = ExceptionDispatchInfo.Capture(e); // <2> if an exception is thrown, capture it to throw outside of the observer
					waitHandle.Set();
				},
				onCompleted: () => waitHandle.Set()
			);

			scrollAllObservable.Subscribe(scrollAllObserver); // <3> initiate scrolling

			waitHandle.WaitOne(); // <4> block the current thread until the wait handle is set
			info?.Throw(); // <5> if an exception was captured whilst scrolling, throw it
		}
	}
}
