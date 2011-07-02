using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ElasticSearch.Client.Thrift;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Fasterflect;
using ElasticSearch;
using Newtonsoft.Json.Converters;
using ElasticSearch.Client.DSL;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Reflection;

namespace ElasticSearch.Client
{

	public partial class ElasticClient
	{
		public QueryResponse<T> Query<T>(string query) where T : class
		{
			var index = this.Settings.DefaultIndex;
			index.ThrowIfNullOrEmpty("Cannot infer default index for current connection.");

			var type = typeof(T);
			var typeName = this.InferTypeName<T>();
			string path = this.CreatePath(index, typeName) + "_search";

			var status = this.Connection.PostSync(path, query);
			if (status.Error != null)
			{
				return new QueryResponse<T>()
				{
					IsValid = false,
					ConnectionError = status.Error
				};
			}
			var response = JsonConvert.DeserializeObject<QueryResponse<T>>(status.Result, this.SerializationSettings);

			return response;
		}

		public QueryResponse<T> Search<T>(Search search) where T : class
		{
			var rawQuery = this.Serialize(search);
			return this.Query<T>(rawQuery);
		}
		public QueryResponse<T> Search<T>(string search) where T : class
		{
			return this.Query<T>(search);
		}

		public QueryResponse<T> Search<T>(Query<T> query) where T : class
		{
			
			var q = query.Queries.First();
			var expression = q.MemberExpression;
			this.PropertyNameResolver.Resolve(expression);

			var o = this.SerializationSettings.ContractResolver;


			var contract = this.SerializationSettings.ContractResolver.ResolveContract(expression.Type);


			var search = new Search()
			{

			};

			var rawQuery = this.Serialize(search);
			return this.Query<T>(rawQuery);
		}



		public QueryResponse<T> Search<T>(IQuery query) where T : class
		{
			return this.Search<T>(new Search()
			{
				Query = new Query(query)

			}.Skip(0).Take(10)
			);
		}
		
	}
}
