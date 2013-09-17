using System;
using Nest.Dsl.Factory.Extensions;
using Nest.Dsl.Factory;
using System.Diagnostics;
using System.Threading.Tasks;
using Nest.Resolvers;

namespace Nest
{
	public static class ElasticClientExtensions
	{
		public static IQueryResponse<T> SearchRaw<T>(this IElasticClient client,
			string path, string query) where T  : class
		{
			var connectionStatus = client.Connection.PostSync(path, query);
			return client.ToParsedResponse<QueryResponse<T>>(connectionStatus);
		}

		public static Task<IQueryResponse<T>> SearchRawAsync<T>(this IElasticClient client,
			string path, string query) where T : class
		{
			return client.Connection.Post(path, query)
				.ContinueWith(t=> client.ToParsedResponse<QueryResponse<T>>(t.Result) as IQueryResponse<T>);
		}
		/// <summary>
		/// Synchronously search using dynamic as its return type.
		/// </summary>
		public static IQueryResponse<dynamic> Search(this IElasticClient client,
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

			var path = new PathResolver(client.Settings).GetSearchPathForDynamic(search);
			var query = searchBuilder.ToString();

			return client.SearchRaw<dynamic>(query, path);
		}

		/// <summary>
		/// Synchronously search using T as the return type
		/// </summary>
		public static IQueryResponse<T> Search<T>(this IElasticClient client, 
			SearchBuilder searchBuilder,
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
			var path = new PathResolver(client.Settings).GetSearchPathForTyped(search);
			return client.SearchRaw<T>(query, path);
		}

		/// <summary>
		/// Asynchronously search using dynamic as its return type.
		/// </summary>
		public static Task<IQueryResponse<dynamic>> SearchAsync(this IElasticClient client,
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

			var path = new PathResolver(client.Settings).GetSearchPathForDynamic(search);
			var query = searchBuilder.ToString();

			return client.SearchRawAsync<dynamic>(query, path);

		}

		/// <summary>
		/// Asynchronously search using T as the return type
		/// </summary>
		public static Task<IQueryResponse<T>> SearchAsync<T>(this IElasticClient client,
			SearchBuilder searchBuilder,
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
			var path = new PathResolver(client.Settings).GetSearchPathForTyped(search);

			return client.SearchRawAsync<T>(query, path);
		}

	}
}