using System;
using System.Linq;
using System.Linq.Expressions;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <summary>
		/// Search using dynamic as its return type.
		/// </summary>
		public QueryResponse<dynamic> Search(Func<SearchDescriptor<dynamic>, SearchDescriptor<dynamic>> searcher)
		{
			var search = new SearchDescriptor<dynamic>();
			var descriptor = searcher(search);
			var path = this.GetPathForDynamic(descriptor);
			var query = ElasticClient.Serialize(descriptor);

			ConnectionStatus status = this.Connection.PostSync(path, query);
			var r = this.ToParsedResponse<QueryResponse<dynamic>>(status);
			return r;
		}
		/// <summary>
		/// Search using T as the return type
		/// </summary>
		public QueryResponse<T> Search<T>(Func<SearchDescriptor<T>, SearchDescriptor<T>> searcher) where T : class
		{
			var search = new SearchDescriptor<T>();
			var descriptor = searcher(search);

			var query = ElasticClient.Serialize(descriptor);
			var path = this.GetPathForTyped(descriptor);
			ConnectionStatus status = this.Connection.PostSync(path, query);
			var r = this.ToParsedResponse<QueryResponse<T>>(status);
			return r;
		}
		/// <summary>
		/// Search using T as the return type, string based.
		/// </summary>
		[Obsolete("Passing a query by string? Found a bug in the DSL? https://github.com/Mpdreamz/NEST/issues")]
		public QueryResponse<T> Search<T>(string query) where T : class
		{
			var descriptor = new SearchDescriptor<T>();
			var path = this.GetPathForTyped(descriptor);
			ConnectionStatus status = this.Connection.PostSync(path, query);
			var r = this.ToParsedResponse<QueryResponse<T>>(status);
			return r;
		}
	}
}