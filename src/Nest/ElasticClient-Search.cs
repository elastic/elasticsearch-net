using System;
using System.Diagnostics;
using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.CSharp.RuntimeBinder;
using System.Reflection;
using System.Linq;
using Nest.Resolvers;

namespace Nest
{
	public partial class ElasticClient
	{

		/// <summary>
		/// Synchronously search using T as the return type
		/// </summary>
		public IQueryResponse<T> Search<T>(Func<SearchDescriptor<T>, SearchDescriptor<T>> searcher) where T : class
		{
			return Search<T, T>(searcher);
		}

		/// <summary>
		/// Synchronously search using TResult as the return type and T to construct the query
		/// </summary>
		public IQueryResponse<TResult> Search<T, TResult>(Func<SearchDescriptor<T>, SearchDescriptor<T>> searcher)
			where T : class
			where TResult : class
		{
			var search = new SearchDescriptor<T>();
			var descriptor = searcher(search);

			var query = this.Serialize(descriptor);
			var path = this.PathResolver.GetSearchPathForTyped(descriptor);
			var status = this.Connection.PostSync(path, query);
			return this.GetParsedResponse<T, TResult>(status, descriptor);

		}


		/// <summary>
		/// Asynchronously search using T as the return type
		/// </summary>
		public Task<IQueryResponse<T>> SearchAsync<T>(Func<SearchDescriptor<T>, SearchDescriptor<T>> searcher) where T : class
		{
			return SearchAsync<T, T>(searcher);
		}

		/// <summary>
		/// Asynchronously search using TResult as the return type and T to construct the query
		/// </summary>
		public Task<IQueryResponse<TResult>> SearchAsync<T, TResult>(Func<SearchDescriptor<T>, SearchDescriptor<T>> searcher)
			where T : class
			where TResult : class
		{
			var search = new SearchDescriptor<T>();
			var descriptor = searcher(search);
			var query = this.Serialize(descriptor);
			var path = this.PathResolver.GetSearchPathForTyped(descriptor);

			var task = this.Connection.Post(path, query);
			return task.ContinueWith<IQueryResponse<TResult>>(t => this.GetParsedResponse<T, TResult>(task.Result, descriptor));
		}

		private IQueryResponse<TResult> GetParsedResponse<T, TResult>(ConnectionStatus status, SearchDescriptor<T> descriptor)
			where T : class
			where TResult : class
		{
			var types = (descriptor._Types ?? Enumerable.Empty<TypeNameMarker>())
				.Where(t => t.Type != null);
			var partialFields = descriptor._PartialFields.EmptyIfNull().Select(x => x.Key);
			if (descriptor._ConcreteTypeSelector == null && (
				types.Any(t => t.Type != typeof(TResult)))
				|| partialFields.Any())
			{
				var typeDictionary = types
					.ToDictionary(t => t.Resolve(this._connectionSettings), t => t.Type);

				descriptor._ConcreteTypeSelector = (o, h) =>
				{
					Type t;
					if (!typeDictionary.TryGetValue(h.Type, out t))
						return typeof(TResult);
					return t;
				};
			}

			if (descriptor._ConcreteTypeSelector == null)
				return this.Deserialize<QueryResponse<TResult>>(status);

			return this.Serializer.DeserializeInternal<QueryResponse<TResult>>(
				status,
				piggyBackJsonConverter: new ConcreteTypeConverter<TResult>(descriptor._ConcreteTypeSelector, partialFields)
			);
		}
	}
}