using System;
using System.Linq;
using System.Linq.Expressions;
using Nest.DSL;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Nest
{
    public partial class ElasticClient
    {
        private QueryResponse<T> Query<T>(string query, string index, string typeName, string routing) where T : class
        {
            index.ThrowIfNullOrEmpty("index");

            string path = string.Concat(!string.IsNullOrEmpty(typeName) ? 
                            this.CreatePath(index, typeName) : 
                            this.CreatePath(index), "_search");
            if (!String.IsNullOrEmpty(routing))
            {
                path = "{0}?routing={1}".F(path, routing);
            }

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
            return this.Search<T>(search, index, typeName, null);
        }

        public QueryResponse<T> Search<T>(string search, string index, string typeName, string routing) where T : class
        {
            return this.Query<T>(search, index, typeName, routing);
        }

        public QueryResponse<T> Search<T>(Func<SearchDescriptor<T>, SearchDescriptor<T>> searcher) where T : class
        {
            var search = new SearchDescriptor<T>();
            var resolved = searcher(search);
            var query = ElasticClient.Serialize(resolved);
            var index = this.Settings.DefaultIndex;
            var typeName = this.InferTypeName<T>();
            return this.Search<T>(query, index, typeName);
        }

    }
}