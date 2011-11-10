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
		public CountResponse Count()
		{
			return this.Count(@"match_all : { }");
		}
		public CountResponse Count<T>() where T : class
		{
			return this.Count<T>(@"match_all : { }");
		}

		public CountResponse Count(string query)
		{
			query.ThrowIfNull("query");
			var index = this.Settings.DefaultIndex;
			index.ThrowIfNullOrEmpty("Cannot infer default index for current connection.");

			return this.Count(query, index);
		}

		public CountResponse Count(string query, string index)
		{
			query.ThrowIfNull("query");
			index.ThrowIfNull("index");
			return this.Count(query, index, null);
		}

		public CountResponse Count(string query, string index, string typeName)
		{
			query.ThrowIfNull("query");
			index.ThrowIfNull("index");
			string path = null;
			if (typeName.IsNullOrEmpty())
				path = this.CreatePath(index) + "_count";
			else
				path = this.CreatePath(index, typeName) + "_count";
			if (!query.StartsWith("{"))
				query = " { " + query + " }";
			return _Count(path, query);
		}
		public CountResponse Count<T>(string query) where T : class
		{
			var index = this.Settings.DefaultIndex;
			index.ThrowIfNullOrEmpty("Cannot infer default index for current connection.");

			var type = typeof(T);
			var typeName = this.InferTypeName<T>();
			string path = this.CreatePath(index, typeName) + "_count";
			if (!query.StartsWith("{"))
				query = " { " + query+  " }";
			return _Count(path, query);
		}
		private CountResponse _Count(string path, string query)
		{
			var status = this.Connection.PostSync(path, query);
			if (status.Error != null)
			{
				return new CountResponse()
				{
					IsValid = false,
					ConnectionError = status.Error
				};
			}

			var response = JsonConvert.DeserializeObject<CountResponse>(status.Result, this.SerializationSettings);
			return response;
		}

	}
}
