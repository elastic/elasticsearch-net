using System;
using System.Linq;
using System.Linq.Expressions;
using ElasticSearch.Client.DSL;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ElasticSearch.Client
{
    public partial class ElasticClient
    {
        private QueryResponse<T> Query<T>(string query, string index, string typeName) where T : class
        {
            index.ThrowIfNullOrEmpty("index");

            string path = string.Concat(!string.IsNullOrEmpty(typeName) ? 
                            this.CreatePath(index, typeName) : 
                            this.CreatePath(index), "_search");

            ConnectionStatus status = this.Connection.PostSync(path, query);
            var r = this.ToParsedResponse<QueryResponse<T>>(status);
            return r;
        }

        public QueryResponse<T> Search<T>(string search) where T : class
        {
            return this.Search<T>(search, this.Settings.DefaultIndex, this.InferTypeName<T>());
        }

        public QueryResponse<T> Search<T>(string search, string index, string typeName) where T : class
        {
            return this.Query<T>(search, index, typeName);
        }

        public QueryResponse<T> Search<T>(Func<SearchDescriptor<T>, SearchDescriptor<T>> searcher) where T : class
        {
            var search = new SearchDescriptor<T>();
            var resolved = searcher(search);
            var query = ElasticClient.Serialize(resolved);
            var index = this.Settings.DefaultIndex;
            var typeName = this.InferTypeName<T>();
            return this.Query<T>(query, index, typeName);
        }

    }
}