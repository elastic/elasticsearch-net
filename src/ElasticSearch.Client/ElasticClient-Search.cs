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
            typeName.ThrowIfNullOrEmpty("typeName");

            Type type = typeof (T);
            string path = this.CreatePath(index, typeName) + "_search";

            ConnectionStatus status = this.Connection.PostSync(path, query);
            if (status.Error != null)
            {
                return new QueryResponse<T>
                       {
                           PropertyNameResolver = this.PropertyNameResolver,
                           IsValid = false,
                           ConnectionError = status.Error
                       };
            }
            var response = JsonConvert.DeserializeObject<QueryResponse<T>>(status.Result, this.SerializationSettings);
            response.PropertyNameResolver = this.PropertyNameResolver;
            return response;
        }

        public QueryResponse<T> Search<T>(Search search) where T : class
        {
            return this.Search<T>(search, this.Settings.DefaultIndex, this.InferTypeName<T>());
        }

        public QueryResponse<T> Search<T>(Search search, string index, string typeName) where T : class
        {
            string rawQuery = this.Serialize(search);
            return this.Query<T>(rawQuery, index, typeName);
        }

        public QueryResponse<T> Search<T>(string search) where T : class
        {
            return this.Search<T>(search, this.Settings.DefaultIndex, this.InferTypeName<T>());
        }

        public QueryResponse<T> Search<T>(string search, string index, string typeName) where T : class
        {
            return this.Query<T>(search, index, typeName);
        }

        public QueryResponse<T> Search<T>(Query<T> query) where T : class
        {
            return this.Search(query, this.Settings.DefaultIndex, this.InferTypeName<T>());
        }

        public QueryResponse<T> Search<T>(Query<T> query, string index, string typeName) where T : class
        {
            QueryDescriptor q = query.Queries.First();
            MemberExpression expression = q.MemberExpression;

            IContractResolver o = this.SerializationSettings.ContractResolver;

            JsonContract contract = this.SerializationSettings.ContractResolver.ResolveContract(expression.Type);

            var search = new Search
                         {
                         };

            string rawQuery = this.Serialize(search);
            return this.Query<T>(rawQuery, index, typeName);
        }

        public QueryResponse<T> Search<T>(IQuery query) where T : class
        {
            return this.Search<T>(query, this.Settings.DefaultIndex, this.InferTypeName<T>());
        }

        public QueryResponse<T> Search<T>(IQuery query, string index, string typeName) where T : class
        {
            return this.Search<T>(new Search
                                  {
                                      Query = new Query(query)
                                  }.Skip(0).Take(10),
                                  index,
                                  typeName);
        }
    }
}