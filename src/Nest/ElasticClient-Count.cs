using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.Thrift;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Fasterflect;
using Newtonsoft.Json.Converters;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Reflection;

namespace Nest
{
	public partial class ElasticClient
	{
		public CountResponse CountAll(string query) {
			return this._Count("_count", query);
		}
		public CountResponse CountAll(Action<QueryDescriptor> querySelector)
		{
			querySelector.ThrowIfNull("querySelector");
			var descriptor = new QueryDescriptor();
			querySelector(descriptor);
			var query = ElasticClient.Serialize(descriptor);
			return this._Count("_count", query);
		}
		public CountResponse CountAll<T>(Action<QueryDescriptor<T>> querySelector) where T : class
		{
			querySelector.ThrowIfNull("querySelector");
			var descriptor = new QueryDescriptor<T>();
			querySelector(descriptor);
			var query = ElasticClient.Serialize(descriptor);
			return this._Count("_count", query);
		}


		public CountResponse Count(Action<QueryDescriptor> querySelector)
		{
			var index = this.Settings.DefaultIndex;
			index.ThrowIfNullOrEmpty("Cannot infer default index for current connection.");

			string path = this.CreatePath(index) + "_count";
			var descriptor = new QueryDescriptor();
			querySelector(descriptor);
			var query = ElasticClient.Serialize(descriptor);
			return _Count(path, query);
		}
		public CountResponse Count(IEnumerable<string> indices, Action<QueryDescriptor> querySelector)
		{
			indices.ThrowIfEmpty("indices");
			string path = string.Join(",", indices) + "/_count";
			var descriptor = new QueryDescriptor();
			querySelector(descriptor);
			var query = ElasticClient.Serialize(descriptor);
			return _Count(path, query);
		}
		public CountResponse Count(IEnumerable<string> indices, IEnumerable<string> types, Action<QueryDescriptor> querySelector)
		{
			indices.ThrowIfEmpty("indices");
			indices.ThrowIfEmpty("types");
			string path = string.Join(",", indices) + "/" + string.Join(",", types) + "/_count";
			var descriptor = new QueryDescriptor();
			querySelector(descriptor);
			var query = ElasticClient.Serialize(descriptor);
			return _Count(path, query);
		}


		public CountResponse Count<T>(Action<QueryDescriptor<T>> querySelector) where T : class
		{
			var index = this.Settings.DefaultIndex;
			index.ThrowIfNullOrEmpty("Cannot infer default index for current connection.");

			var type = typeof(T);
			var typeName = this.InferTypeName<T>();
			string path = this.CreatePath(index, typeName) + "_count";
			var descriptor = new QueryDescriptor<T>();
			querySelector(descriptor);
			var query = ElasticClient.Serialize(descriptor);
			return _Count(path, query);
		}
		public CountResponse Count<T>(IEnumerable<string> indices, Action<QueryDescriptor<T>> querySelector) where T : class
		{
			indices.ThrowIfEmpty("indices");
			string path = string.Join(",", indices) + "/_count";
			var descriptor = new QueryDescriptor<T>();
			querySelector(descriptor);
			var query = ElasticClient.Serialize(descriptor);
			return _Count(path, query);
		}
		public CountResponse Count<T>(IEnumerable<string> indices, IEnumerable<string> types, Action<QueryDescriptor<T>> querySelector) where T : class
		{
			indices.ThrowIfEmpty("indices");
			indices.ThrowIfEmpty("types");
			string path = string.Join(",", indices) + "/" + string.Join(",", types) + "/_count";
			var descriptor = new QueryDescriptor<T>();
			querySelector(descriptor);
			var query = ElasticClient.Serialize(descriptor);
			return _Count(path, query);
		}

		private CountResponse _Count(string path, string query)
		{
			var status = this.Connection.PostSync(path, query);
			var r = this.ToParsedResponse<CountResponse>(status);
			return r;
		}

	}
}
