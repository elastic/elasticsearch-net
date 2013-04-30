using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.CSharp.RuntimeBinder;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        /// Synchronously search using T as the return type, string based.
        /// </summary>
        /// <param name="path">EXPERT OPTION: Pass the path and querystring used to perform the search on, when null it will be infered from the type</param>
        public IQueryResponse<T> SearchRaw<T>(string query, string path = null) where T : class
        {
            return SearchRaw<T, T>(query, path);
        }

        /// <summary>
        /// Asynchronously search using T as the return type, string based.
        /// </summary>
        /// <param name="path">EXPERT OPTION: Pass the path and querystring used to perform the search on, when null it will be infered from the type</param>
        public Task<IQueryResponse<T>> SearchRawAsync<T>(string query, string path = null) where T : class
        {
            return SearchRawAsync<T, T>(query, path);
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
			
            if (descriptor._ConcreteTypeSelector == null)
            {
				var typeDictionary = (descriptor._Types ?? Enumerable.Empty<TypeNameMarker>())
					.Where(t => t.Type != null)
					.ToDictionary(t => t.Resolve(this.Settings), t => t.Type);

				descriptor._ConcreteTypeSelector = (o, h) =>
				{
					Type t;
					if (!typeDictionary.TryGetValue(h.Type, out t))
						return typeof(TResult);
					return t;
				};
            }

            var partialFields = descriptor._PartialFields.EmptyIfNull().Select(x => x.Key);

            return this.ToParsedResponse<QueryResponse<TResult>>(
                status,

				extraConverters: new[]
				{
					new ConcreteTypeConverter(descriptor._ClrType, descriptor._ConcreteTypeSelector, partialFields)
				}
            );
        }
    }

    internal class ConcreteTypeConverter : JsonConverter
    {
        private readonly Type _baseType;
        private readonly Func<dynamic, Hit<dynamic>, Type> _concreteTypeSelector;
        private readonly IEnumerable<string> _partialFields;

        public override bool CanWrite { get { return false; } }
        public override bool CanRead { get { return true; } }

        public ConcreteTypeConverter(Type baseType, Func<dynamic, Hit<dynamic>, Type> concreteTypeSelector)
            : this(baseType, concreteTypeSelector, new string[0]) { }

        public ConcreteTypeConverter(Type baseType, Func<dynamic, Hit<dynamic>, Type> concreteTypeSelector, IEnumerable<string> partialFields)
        {
            concreteTypeSelector.ThrowIfNull("concreteTypeSelector");

            _baseType = baseType;
            _concreteTypeSelector = concreteTypeSelector;
            _partialFields = partialFields;
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(IHit<object>).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
			
            Hit<dynamic> hitDynamic = new Hit<dynamic>();
            // Load JObject from stream
            JObject jObject = JObject.Load(reader);
            dynamic d = jObject;

            //favor manual mapping over doing Populate twice.
            hitDynamic.Fields = d.fields;
            hitDynamic.Source = d._source;
            hitDynamic.Index = d._index;
            hitDynamic.Score = (d._score is double) ? d._score : default(double);
            hitDynamic.Type = d._type;
            hitDynamic.Version = d._version;
            hitDynamic.Id = d._id;
            hitDynamic.Sorts = d.sort;
            hitDynamic.Highlight = d.highlight is Dictionary<string, List<string>> ? d.highlight : null;
            hitDynamic.Explanation = d._explanation is Explanation ? d._explanation : null;

            var concreteType = this._concreteTypeSelector(hitDynamic.Source, hitDynamic);
            var hitType = typeof(Hit<>).MakeGenericType(concreteType);
            var hit = Activator.CreateInstance(hitType);

            serializer.Populate(jObject.CreateReader(), hit);

            // deserialize partial fields
            if (_partialFields.Any())
            {
                var itemType = typeof(CovariantItem<>).MakeGenericType(concreteType);
                var listType = typeof(List<>).MakeGenericType(itemType);
                var dictType = typeof(CovariantDictionary<>).MakeGenericType(concreteType);

                var items = Activator.CreateInstance(listType);
                foreach (var pf in this._partialFields)
                {
                    var partial = Activator.CreateInstance(concreteType);

                    serializer.Populate(d.fields[pf].CreateReader(), partial);

                    var dictItem = Activator.CreateInstance(itemType);
                    dictItem.Key = pf;
                    dictItem.Value = partial;
                    items.Add(dictItem);
                }

                var dict = Activator.CreateInstance(dictType);
                dict.Items = items;
                hit.PartialFields = dict;
            }

            return hit;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }    }
}