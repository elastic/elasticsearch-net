using System;
using Elasticsearch.Net;

namespace Nest
{
	public class ReindexObservable<T> : IDisposable, IObservable<IReindexResponse<T>> where T : class
	{
		private ReindexDescriptor<T> _reindexDescriptor;
		private readonly IConnectionSettingsValues _connectionSettings;
		internal IElasticClient CurrentClient { get; set; }
		internal ReindexDescriptor<T> ReindexDescriptor { get; set; }

		public ReindexObservable(IElasticClient client, IConnectionSettingsValues connectionSettings, ReindexDescriptor<T> reindexDescriptor)
		{
			this._connectionSettings = connectionSettings;
			this._reindexDescriptor = reindexDescriptor;
			this.CurrentClient = client;
		}

		public IDisposable Subscribe(IObserver<IReindexResponse<T>> observer)
		{
			observer.ThrowIfNull("observer");
			try
			{
				this.Reindex(observer);
			}
			catch (Exception e)
			{
				observer.OnError(e);
			}
			return this;

		}

		private void Reindex(IObserver<IReindexResponse<T>> observer)
		{
			var fromIndex = this._reindexDescriptor._FromIndexName;
			var toIndex = this._reindexDescriptor._ToIndexName;
			var scroll = this._reindexDescriptor._Scroll ?? "2m";
			var size = this._reindexDescriptor._Size ?? 100;

			fromIndex.ThrowIfNullOrEmpty("fromIndex");
			toIndex.ThrowIfNullOrEmpty("toIndex");

			var indexSettings = this.CurrentClient.GetIndexSettings(i => i.Index(this._reindexDescriptor._FromIndexName));
			Func<CreateIndexDescriptor, CreateIndexDescriptor> settings =
				this._reindexDescriptor._CreateIndexSelector ?? ((ci) => ci);

			var createIndexResponse = this.CurrentClient.CreateIndex(
				toIndex, (c) => settings(c.InitializeUsing(indexSettings.IndexSettings)));
			if (!createIndexResponse.IsValid)
				throw new ReindexException(createIndexResponse.ConnectionStatus);

			var page = 0;
			Func<SearchDescriptor<T>, SearchDescriptor<T>> searchDescriptor = s => s.Index(fromIndex);

			if (typeof(T).Name.Equals(typeof(object).Name))
			{
				searchDescriptor = s => searchDescriptor(s).AllTypes();
			}
			else
			{
				searchDescriptor = s => searchDescriptor(s).Type<T>();
			}


			var searchResult = this.CurrentClient.Search<T>(
				s => searchDescriptor(s)
					.From(0)
					.Size(size)
					.Query(this._reindexDescriptor._QuerySelector ?? (q => q.MatchAll()))
					.SearchType(SearchType.Scan)
					.Scroll(scroll));

			if (searchResult.Total <= 0)
				throw new ReindexException(searchResult.ConnectionStatus, string.Format("index {0} has no documents!", fromIndex));
			IBulkResponse indexResult = null;
			do
			{
				var result = searchResult;
				searchResult = this.CurrentClient.Scroll<T>(s => s
					.Scroll(scroll)
					.ScrollId(result.ScrollId)
				);
				if (searchResult.Documents.HasAny())
					indexResult = this.IndexSearchResults(searchResult, observer, toIndex, page);
				page++;
			} while (searchResult.IsValid && indexResult != null && indexResult.IsValid && searchResult.Documents.HasAny());


			observer.OnCompleted();
		}

		public IBulkResponse IndexSearchResults(ISearchResponse<T> searchResult, IObserver<IReindexResponse<T>> observer, string toIndex, int page)
		{
			if (!searchResult.IsValid)
				throw new ReindexException(searchResult.ConnectionStatus, "reindex failed on scroll #" + page);

			var bb = new BulkDescriptor();
			foreach (var d in searchResult.Hits)
			{
				IHit<T> d1 = d;
				bb.Index<T>(bi => bi.Document(d1.Source).Type(d1.Type).Index(toIndex).Id(d.Id));
			}

			var indexResult = this.CurrentClient.Bulk(b => bb);
			if (!indexResult.IsValid)
				throw new ReindexException(indexResult.ConnectionStatus, "reindex failed when indexing page " + page);

			observer.OnNext(new ReindexResponse<T>
			{
				BulkResponse = indexResult,
				SearchResponse = searchResult,
				Scroll = page
			});
			return indexResult;
		}


		public void Dispose()
		{

		}
	}
}