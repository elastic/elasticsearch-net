using System;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <summary>
		/// Search using dynamic as its return type.
		/// </summary>
		public IQueryResponse<dynamic> Search(Func<SearchDescriptor<dynamic>, SearchDescriptor<dynamic>> searcher)
		{
			var search = new SearchDescriptor<dynamic>();
			var descriptor = searcher(search);
			var path = this.PathResolver.GetPathForDynamic(descriptor);
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
			var search = new SearchDescriptor<T>();
			var descriptor = searcher(search);

			var query = this.Serialize(descriptor);
			var path = this.PathResolver.GetPathForTyped(descriptor);
			ConnectionStatus status = this.Connection.PostSync(path, query);
			var r = this.ToParsedResponse<QueryResponse<T>>(status);
			return r;
		}
		/// <summary>
		/// Search using T as the return type, string based.
		/// </summary>
		[Obsolete("Deprecated but will never be removed. Found a bug in the DSL? https://github.com/Mpdreamz/NEST/issues")]
		public IQueryResponse<T> SearchRaw<T>(string query) where T : class
		{
			var descriptor = new SearchDescriptor<T>();
			var path = this.PathResolver.GetPathForTyped(descriptor);
			ConnectionStatus status = this.Connection.PostSync(path, query);
			var r = this.ToParsedResponse<QueryResponse<T>>(status);
			return r;
		}
	}
}