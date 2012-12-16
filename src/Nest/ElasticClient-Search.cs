using System;
using Nest.FactoryDsl;
using System.Diagnostics;

namespace Nest
{
	public partial class ElasticClient
	{

		/// <summary>
		/// Search using dynamic as its return type.
		/// </summary>
		public IQueryResponse<dynamic> Search(
			SearchBuilder searchBuilder, 
			string index = null,
			string type = null,
			string routing = null,
			SearchType? searchType = null)
		{
			var search = new SearchDescriptor<dynamic>();
			if (!index.IsNullOrEmpty())
				search.Index(index);
			if (!type.IsNullOrEmpty())
				search.Type(type);
			if (!routing.IsNullOrEmpty())
				search.Routing(routing);
			if (searchType.HasValue)
				search.SearchType(searchType.Value);

			var path = this.PathResolver.GetSearchPathForDynamic(search);
			var query = searchBuilder.ToString();

			ConnectionStatus status = this.Connection.PostSync(path, query);
			var r = this.ToParsedResponse<QueryResponse<dynamic>>(status);
			return r;
		}
		/// <summary>
		/// Search using T as the return type
		/// </summary>
		public IQueryResponse<T> Search<T>(SearchBuilder searchBuilder,
			string index = null,
			string type = null,
			string routing = null,
			SearchType? searchType = null) where T : class
		{
			var search = new SearchDescriptor<T>();
			if (!index.IsNullOrEmpty())
				search.Index(index);
			if (!type.IsNullOrEmpty())
				search.Type(type);
			if (!routing.IsNullOrEmpty())
				search.Routing(routing);
			if (searchType.HasValue)
				search.SearchType(searchType.Value);

			var query = searchBuilder.ToString();
			var path = this.PathResolver.GetSearchPathForTyped(search);
			ConnectionStatus status = this.Connection.PostSync(path, query);
			var r = this.ToParsedResponse<QueryResponse<T>>(status);
			return r;
		}


		/// <summary>
		/// Search using dynamic as its return type.
		/// </summary>
		public IQueryResponse<dynamic> Search(Func<SearchDescriptor<dynamic>, SearchDescriptor<dynamic>> searcher)
		{
			var search = new SearchDescriptor<dynamic>();
			var descriptor = searcher(search);
			var path = this.PathResolver.GetSearchPathForDynamic(descriptor);
			var query = this.Serialize(descriptor);

			ConnectionStatus status = this.Connection.PostSync(path, query);
			var r = this.ToParsedResponse<QueryResponse<dynamic>>(status);
			return r;
		}
		/// <summary>
		/// Search using T as the return type
		/// </summary>
		public IQueryResponse<T> Search<T>(Func<SearchDescriptor<T>, SearchDescriptor<T>> searcher) where T : class
		{
      var sw2 = new Stopwatch(); sw2.Start();
      var sw = new Stopwatch();
      sw.Start();
			var search = new SearchDescriptor<T>();
			var descriptor = searcher(search);
      var xx = sw.Elapsed;
      sw.Restart();
			var query = this.Serialize(descriptor);
      var xY = sw.Elapsed;
      sw.Restart();
			var path = this.PathResolver.GetSearchPathForTyped(descriptor);
      var xxx = sw.Elapsed;
      sw.Restart();
			ConnectionStatus status = this.Connection.PostSync(path, query);
      var xxxx = sw.Elapsed;
      sw.Restart();
			var r = this.ToParsedResponse<QueryResponse<T>>(status);
      var xxxxx = sw.Elapsed;
      sw.Restart();
      sw2.Stop();
      var total = sw2.Elapsed;
			return r;
		}
		/// <summary>
		/// Search using T as the return type, string based.
		/// </summary>
		public IQueryResponse<T> SearchRaw<T>(string query) where T : class
		{
			var descriptor = new SearchDescriptor<T>();
			var path = this.PathResolver.GetSearchPathForTyped(descriptor);
			ConnectionStatus status = this.Connection.PostSync(path, query);
			var r = this.ToParsedResponse<QueryResponse<T>>(status);
			return r;
		}
	}
}