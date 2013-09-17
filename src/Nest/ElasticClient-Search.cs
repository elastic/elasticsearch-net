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
        /// Synchronously search using dynamic as its return type.
        /// </summary>
        public IQueryResponse<dynamic> Search(Func<SearchDescriptor<dynamic>, SearchDescriptor<dynamic>> searcher)
        {
            var search = new SearchDescriptor<dynamic>();
            var descriptor = searcher(search);
            var path = this.PathResolver.GetSearchPathForDynamic(descriptor);
            var query = this.Serialize(descriptor);

            ConnectionStatus status = this.Connection.PostSync(path, query);
            var r = this.GetParsedResponse<dynamic>(status, descriptor); ;
            return r;
        }

        /// <summary>
        /// Synchronously search using T as the return type
        /// </summary>
        public IQueryResponse<T> Search<T>(Func<SearchDescriptor<T>, SearchDescriptor<T>> searcher) where T : class {
            return Search<T, T>(searcher);
        }

        /// <summary>
        /// Synchronously search using TResult as the return type
        /// </summary>
        public IQueryResponse<T> Search<T>(SearchDescriptor<T> descriptor) where T : class {
            return Search<T, T>(descriptor);
        }

        /// <summary>
        /// Synchronously search using TResult as the return type
        /// </summary>
        public IQueryResponse<TResult> Search<T, TResult>(Func<SearchDescriptor<T>, SearchDescriptor<T>> searcher) where T : class where TResult : class
        {
            var search = new SearchDescriptor<T>();
            var descriptor = searcher(search);
            return Search<T, TResult>(descriptor);
        }

        /// <summary>
        /// Synchronously search using T as the return type
        /// </summary>
        public IQueryResponse<TResult> Search<T, TResult>(SearchDescriptor<T> descriptor) where T : class where TResult : class
        {
            var query = this.Serialize(descriptor);
            var path = this.PathResolver.GetSearchPathForTyped(descriptor);
            var status = this.Connection.PostSync(path, query);
            return this.GetParsedResponse<T, TResult>(status, descriptor);
        }

        /// <summary>
        /// Synchronously search using TResult as the return type, string based.
        /// </summary>
        /// <param name="path">EXPERT OPTION: Pass the path and querystring used to perform the search on, when null it will be infered from the type</param>
        public IQueryResponse<TResult> SearchRaw<T, TResult>(string query, string path = null) where T : class where TResult : class
        {
            var descriptor = new SearchDescriptor<T>();

            if (string.IsNullOrEmpty(path))
                path = this.PathResolver.GetSearchPathForTyped(descriptor);

            ConnectionStatus status = this.Connection.PostSync(path, query);
            var r = this.GetParsedResponse<T, TResult>(status, descriptor);
            return r;
        }

        /// <summary>
        /// Asynchronously search using TResult as the return type, string based.
        /// </summary>
        /// <param name="path">EXPERT OPTION: Pass the path and querystring used to perform the search on, when null it will be infered from the type</param>
        public Task<IQueryResponse<TResult>> SearchRawAsync<T, TResult>(string query, string path = null) where T : class where TResult : class
        {
            var descriptor = new SearchDescriptor<T>();

            if (string.IsNullOrEmpty(path))
                path = this.PathResolver.GetSearchPathForTyped(descriptor);

            var task = this.Connection.Post(path, query);
            return task.ContinueWith<IQueryResponse<TResult>>(t => this.GetParsedResponse<T, TResult>(task.Result, descriptor));
        }

        /// <summary>
        /// Asynchronously search using dynamic as its return type.
        /// </summary>
        public Task<IQueryResponse<dynamic>> SearchAsync(Func<SearchDescriptor<dynamic>, SearchDescriptor<dynamic>> searcher)
        {
            var search = new SearchDescriptor<dynamic>();
            var descriptor = searcher(search);
            var path = this.PathResolver.GetSearchPathForDynamic(descriptor);
            var query = this.Serialize(descriptor);

            var task = this.Connection.Post(path, query);
            return task.ContinueWith<IQueryResponse<dynamic>>(t => this.GetParsedResponse<dynamic>(task.Result, descriptor));
        }

        /// <summary>
        /// Asynchronously search using T as the return type
        /// </summary>
        public Task<IQueryResponse<T>> SearchAsync<T>(Func<SearchDescriptor<T>, SearchDescriptor<T>> searcher) where T : class
        {
            return SearchAsync<T, T>(searcher);
        }

        /// <summary>
        /// Asynchronously search using T as the return type
        /// </summary>
        public Task<IQueryResponse<T>> SearchAsync<T>(SearchDescriptor<T> descriptor) where T : class
        {
            return SearchAsync<T, T>(descriptor);
        }

        /// <summary>
        /// Asynchronously search using TResult as the return type
        /// </summary>
        public Task<IQueryResponse<TResult>> SearchAsync<T, TResult>(Func<SearchDescriptor<T>, SearchDescriptor<T>> searcher) where T : class where TResult : class
        {
            var search = new SearchDescriptor<T>();
            var descriptor = searcher(search);
            return SearchAsync<T, TResult>(descriptor);
        }

        /// <summary>
        /// Asynchronously search using TResult as the return type
        /// </summary>
        public Task<IQueryResponse<TResult>> SearchAsync<T, TResult>(SearchDescriptor<T> descriptor) where T : class where TResult : class
        {
            var query = this.Serialize(descriptor);
            var path = this.PathResolver.GetSearchPathForTyped(descriptor);

            var task = this.Connection.Post(path, query);
            return task.ContinueWith<IQueryResponse<TResult>>(t => this.GetParsedResponse<T, TResult>(task.Result, descriptor));
        }

        private IQueryResponse<T> GetParsedResponse<T>(ConnectionStatus status, SearchDescriptor<T> descriptor) where T : class
        {
            return GetParsedResponse<T, T>(status, descriptor);
        }

        private IQueryResponse<TResult> GetParsedResponse<T, TResult>(ConnectionStatus status, SearchDescriptor<T> descriptor) where T : class where TResult : class
        {
	        var types = (descriptor._Types ?? Enumerable.Empty<TypeNameMarker>())
		        .Where(t => t.Type != null);
			var partialFields = descriptor._PartialFields.EmptyIfNull().Select(x => x.Key);
            if (descriptor._ConcreteTypeSelector == null && (
				types.Any(t=>t.Type != typeof(TResult))) || partialFields.Any()
				)
            {
				var typeDictionary = types
					.ToDictionary(t => t.Resolve(this.Settings), t => t.Type);

				descriptor._ConcreteTypeSelector = (o, h) =>
				{
					Type t;
					if (!typeDictionary.TryGetValue(h.Type, out t))
						return typeof(TResult);
					return t;
				};
            }

            
	        if (descriptor._ConcreteTypeSelector == null)
		        return this.ToParsedResponse<QueryResponse<TResult>>(status);
            return this.ToParsedResponse<QueryResponse<TResult>>(
                status,

				extraConverters: new[]
				{
					new ConcreteTypeConverter(descriptor._ClrType, descriptor._ConcreteTypeSelector, partialFields)
				}
            );
        }
    }
}