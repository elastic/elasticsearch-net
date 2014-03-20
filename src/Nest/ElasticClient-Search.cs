using System;
using System.Diagnostics;
using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.CSharp.RuntimeBinder;
using System.Reflection;
using System.Linq;
using Nest.Resolvers;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public partial class ElasticClient
	{

		/// <summary>
		/// Synchronously search using T as the return type
		/// </summary>
		public IQueryResponse<T> Search<T>(Func<SearchDescriptor<T>, SearchDescriptor<T>> searchSelector) where T : class
		{
			return Search<T, T>(searchSelector);
		}

		/// <summary>
		/// Synchronously search using TResult as the return type and T to construct the query
		/// </summary>
		public IQueryResponse<TResult> Search<T, TResult>(Func<SearchDescriptor<T>, SearchDescriptor<T>> searchSelector)
			where T : class
			where TResult : class
		{
			searchSelector.ThrowIfNull("searchSelector");
			var descriptor = searchSelector(new SearchDescriptor<T>());
			var pathInfo = ((IPathInfo<SearchQueryString>)descriptor).ToPathInfo(this._connectionSettings);
			var deserializationState = CreateCovariantSearchSelector<T, TResult>(descriptor);
			var status = this.RawDispatch.SearchDispatch<QueryResponse<TResult>>(pathInfo, descriptor, deserializationState);
			return status.Success ? status.Response : CreateInvalidInstance<QueryResponse<TResult>>(status);
		}


		/// <summary>
		/// Asynchronously search using T as the return type
		/// </summary>
		public Task<IQueryResponse<T>> SearchAsync<T>(Func<SearchDescriptor<T>, SearchDescriptor<T>> searchSelector) where T : class
		{
			return SearchAsync<T, T>(searchSelector);
		}

		/// <summary>
		/// Asynchronously search using TResult as the return type and T to construct the query
		/// </summary>
		public Task<IQueryResponse<TResult>> SearchAsync<T, TResult>(Func<SearchDescriptor<T>, SearchDescriptor<T>> searchSelector)
			where T : class
			where TResult : class
		{
			searchSelector.ThrowIfNull("searchSelector");
			var descriptor = searchSelector(new SearchDescriptor<T>());
			var pathInfo = ((IPathInfo<SearchQueryString>)descriptor).ToPathInfo(this._connectionSettings);
			var deserializationState = CreateCovariantSearchSelector<T, TResult>(descriptor);
			return this.RawDispatch.SearchDispatchAsync<QueryResponse<TResult>>(pathInfo, descriptor, deserializationState)
				.ContinueWith<IQueryResponse<TResult>>(t=> t.Result.Success ? 
					t.Result.Response : CreateInvalidInstance<QueryResponse<TResult>>(t.Result));
		}

		private JsonConverter CreateCovariantSearchSelector<T, TResult>(SearchDescriptor<T> originalSearchDescriptor)
			where T : class
			where TResult : class
		{
			var types = (originalSearchDescriptor._Types ?? Enumerable.Empty<TypeNameMarker>()).Where(t => t.Type != null);
			if (originalSearchDescriptor._ConcreteTypeSelector == null && types.Any(t => t.Type != typeof(TResult)))
			{
				var typeDictionary = types.ToDictionary(this.Infer.TypeName, t => t.Type); 
				originalSearchDescriptor._ConcreteTypeSelector = (o, h) =>
				{
					Type t;
					if (!typeDictionary.TryGetValue(h.Type, out t))
						return typeof(TResult);
					return t;
				};
			}
			if (originalSearchDescriptor._ConcreteTypeSelector == null)
				return null;
			return new ConcreteTypeConverter<TResult>(originalSearchDescriptor._ConcreteTypeSelector);
		}
	}
}