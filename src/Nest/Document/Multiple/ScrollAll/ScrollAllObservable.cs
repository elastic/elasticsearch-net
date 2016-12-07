using System;
using System.Linq;
using Elasticsearch.Net;
using System.Threading.Tasks;
using System.Threading;

namespace Nest
{
	public class ScrollAllObservable<T> : IDisposable, IObservable<IScrollAllResponse<T>> where T : class
	{
		private readonly IScrollAllRequest _scrollAllRequest;
		private readonly ISearchRequest _searchRequest;
		private readonly IElasticClient _client;

		private readonly CancellationToken _compositeCancelToken;
		private readonly CancellationTokenSource _compositeCancelTokenSource;

		//since we modify the passed searchrequest during the setup phase we use a simple
		//semaphore async await to make sure we do not mutate over multiple request during the initial
		//sliced scroll setup
		private readonly SemaphoreSlim _scrollInitiationLock = new SemaphoreSlim(1, 1);
		private readonly ProducerConsumerBackPressure _backPressure;

		public ScrollAllObservable(
			IElasticClient client, 
			IScrollAllRequest scrollAllRequest,
			CancellationToken cancellationToken = default(CancellationToken)
			)
		{
			this._scrollAllRequest = scrollAllRequest;
			this._searchRequest = scrollAllRequest?.Search ?? new SearchRequest<T>();
			if (this._searchRequest.Sort == null)
				this._searchRequest.Sort = SortField.ByDocumentOrder;
			this._searchRequest.RequestParameters.Scroll(this._scrollAllRequest.ScrollTime.ToTimeSpan());
			this._client = client;
			this._compositeCancelTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
			this._compositeCancelToken = this._compositeCancelTokenSource.Token;
			this._backPressure = this._scrollAllRequest.BackPressure;
		}

		public IDisposable Subscribe(IObserver<IScrollAllResponse<T>> observer)
		{
			observer.ThrowIfNull(nameof(observer));
			try
			{
				this.ScrollAll(observer);
			}
			catch (Exception e)
			{
				observer.OnError(e);
			}
			return this;
		}

		private void ScrollAll(IObserver<IScrollAllResponse<T>> observer)
		{
			var slices = this._scrollAllRequest.Slices;
			var maxSlicesAtOnce = this._scrollAllRequest.MaxDegreeOfParallelism ?? this._scrollAllRequest.Slices;

			Enumerable.Range(0, slices).ForEachAsync<int, bool>(
				(slice, l) => this.ScrollSliceAsync(observer, slice),
				(slice, r) => { },
				t => OnCompleted(t, observer),
				maxSlicesAtOnce
			);
		}

		private async Task<bool> ScrollSliceAsync(IObserver<IScrollAllResponse<T>> observer, int slice)
		{
			var searchResult = await this.InitiateSearchAsync(slice).ConfigureAwait(false);
			await this.ScrollToCompletionAsync(slice, observer, searchResult).ConfigureAwait(false);
			return true;
		}

		private static ElasticsearchClientException Throw(string message, IApiCallDetails details) =>
			new ElasticsearchClientException(PipelineFailure.BadResponse, message, details);

		private void ThrowOnBadSearchResult(ISearchResponse<T> result, int slice, int page)
		{
			if (result == null || !result.IsValid)
			{
				var path = result?.ApiCall.Uri.PathAndQuery ?? "(unknown)";
				throw Throw($"scrolling search on {path} with slice {slice} was not valid on scroll iteration {page}", result?.ApiCall);
			}
			this._compositeCancelToken.ThrowIfCancellationRequested();
		}

		private async Task ScrollToCompletionAsync(int slice, IObserver<IScrollAllResponse<T>> observer, ISearchResponse<T> searchResult)
		{
			var page = 0;
			ThrowOnBadSearchResult(searchResult, slice, page);
			var scroll = this._scrollAllRequest.ScrollTime;
			while (searchResult.IsValid && searchResult.Documents.HasAny())
			{
				if (this._backPressure != null)
					await this._backPressure.WaitAsync(_compositeCancelToken);

				observer.OnNext(new ScrollAllResponse<T>()
				{
					Slice = slice,
					SearchResponse = searchResult,
					Scroll = page
				});
				page++;
				var request = new ScrollRequest(searchResult.ScrollId, scroll);
				searchResult = await this._client.ScrollAsync<T>(request, this._compositeCancelToken).ConfigureAwait(false);
				ThrowOnBadSearchResult(searchResult, slice, page);
			}
		}

		private async Task<ISearchResponse<T>> InitiateSearchAsync(int slice)
		{
			//since we are mutating the searchRequests .Slice it can not be shared across threads for the initial searches
			//so these need to happen in a serial fashion
			await _scrollInitiationLock.WaitAsync(_compositeCancelToken).ConfigureAwait(false);
			try
			{
				this._searchRequest.Slice = new SlicedScroll
				{
					Id = slice,
					Max = this._scrollAllRequest.Slices,
					Field = this._scrollAllRequest.RoutingField
				};
				var response = await this._client.SearchAsync<T>(this._searchRequest, this._compositeCancelToken).ConfigureAwait(false);
				if (response.Total <= 0)
					throw Throw($"ScrollAll query against {response.ApiCall.Uri.PathAndQuery} doesn't contain any documents.", response.ApiCall);
				return response;
			}
			finally { _scrollInitiationLock.Release(); }
		}

		private static void OnCompleted(Task task, IObserver<IScrollAllResponse<T>> observer)
		{
			switch (task.Status)
			{
				case System.Threading.Tasks.TaskStatus.RanToCompletion:
					observer.OnCompleted();
					break;
				case System.Threading.Tasks.TaskStatus.Faulted:
					observer.OnError(task.Exception.InnerException);
					break;
				case System.Threading.Tasks.TaskStatus.Canceled:
					observer.OnError(new TaskCanceledException(task));
					break;
			}
		}

		public bool IsDisposed { get; private set; }
		public void Dispose()
		{
			this.IsDisposed = true;
			this._compositeCancelTokenSource?.Cancel();
		}
	}
}
