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
using System.Globalization;

namespace ElasticSearch.Client
{

	public partial class ElasticClient
	{
		public ConnectionStatus Index<T>(T @object) where T : class
		{
			var path = this.CreatePathFor<T>(@object);
			return this.Index<T>(@object, path);
		}
		public ConnectionStatus Index<T>(T @object, string index) where T : class
		{
			var path = this.CreatePathFor<T>(@object, index);
			return this._indexToPath<T>(@object, path);
		}
		public ConnectionStatus Index<T>(T @object, string index, string type) where T : class
		{
			var path = this.CreatePathFor<T>(@object, index, type);
			return this.Index<T>(@object, path);
		}
		public ConnectionStatus Index<T>(T @object, string index, string type, string id) where T : class
		{
			var path = this.CreatePathFor<T>(@object, index, type, id);
			return this.Index<T>(@object, path);
		}
		public ConnectionStatus Index<T>(T @object, string index, string type, int id) where T : class
		{
			var path = this.CreatePathFor<T>(@object, index, type, id.ToString());
			return this.Index<T>(@object, path);
		}
		
		private ConnectionStatus _indexToPath<T>(T @object, string path) where T : class
		{
			path.ThrowIfNull("path");
			
			string json = JsonConvert.SerializeObject(@object, Formatting.Indented, this.SerializationSettings);

			return this.Connection.PostSync(path, json);
		}

		public void IndexAsync<T>(T @object) where T : class
		{
			var path = this.CreatePathFor<T>(@object);
			this._indexAsyncToPath(@object, path, (s)=>{});
		}
		public void IndexAsync<T>(T @object, Action<ConnectionStatus> continuation) where T : class
		{
			var path = this.CreatePathFor<T>(@object);
			this._indexAsyncToPath(@object, path, continuation);
		}
		public void IndexAsync<T>(T @object, string index, Action<ConnectionStatus> continuation) where T : class
		{
			var path = this.CreatePathFor<T>(@object, index);
			this._indexAsyncToPath(@object, path, continuation);
		}
		public void IndexAsync<T>(T @object, string index, string type, Action<ConnectionStatus> continuation) where T : class
		{
			var path = this.CreatePathFor<T>(@object, index, type);
			this._indexAsyncToPath(@object, path, continuation);
		}
		public void IndexAsync<T>(T @object, string index, string type, string id, Action<ConnectionStatus> continuation) where T : class
		{
			var path = this.CreatePathFor<T>(@object, index, type, id);
			this._indexAsyncToPath(@object, path, continuation);
		}
		public void IndexAsync<T>(T @object, string index, string type, int id, Action<ConnectionStatus> continuation) where T : class
		{
			var path = this.CreatePathFor<T>(@object, index, type, id.ToString());
			this._indexAsyncToPath(@object, path, continuation);
		}
		
		private void _indexAsyncToPath<T>(T @object, string path, Action<ConnectionStatus> continuation) where T : class
		{
			string json = JsonConvert.SerializeObject(@object, Formatting.None, this.SerializationSettings);
			this.Connection.Post(path, json, continuation);
		}

		public ConnectionStatus Index<T>(IEnumerable<T> objects) where T : class
		{
			var json = this.GenerateBulkCommand(@objects);
			return this.Connection.PostSync("_bulk", json);
		}
		public ConnectionStatus Index<T>(IEnumerable<T> objects, string index) where T : class
		{
			var json = this.GenerateBulkCommand(@objects, index);
			return this.Connection.PostSync("_bulk", json);
		}
		public ConnectionStatus Index<T>(IEnumerable<T> objects, string index, string type) where T : class
		{
			var json = this.GenerateBulkCommand(@objects, index, type);
			return this.Connection.PostSync("_bulk", json);
		}


		public void IndexAsync<T>(IEnumerable<T> objects) where T : class
		{
			var json = this.GenerateBulkCommand(@objects);
			this.Connection.Post("_bulk", json, null);
		}
		public void IndexAsync<T>(IEnumerable<T> objects, Action<ConnectionStatus> continuation) where T : class
		{
			var json = this.GenerateBulkCommand(@objects);
			this.Connection.Post("_bulk", json, continuation);
		}
		public void IndexAsync<T>(IEnumerable<T> objects, string index, Action<ConnectionStatus> continuation) where T : class
		{
			var json = this.GenerateBulkCommand(@objects, index);
			this.Connection.Post("_bulk", json, continuation);
		}
		public void IndexAsync<T>(IEnumerable<T> objects, string index, string type, Action<ConnectionStatus> continuation) where T : class
		{
			var json = this.GenerateBulkCommand(@objects, index, type);
			this.Connection.Post("_bulk", json, continuation);
		}
		
		private string GenerateBulkCommand<T>(IEnumerable<T> objects) where T : class
		{
			objects.ThrowIfNull("objects");

			var index = this.Settings.DefaultIndex;
			if (string.IsNullOrEmpty(index))
				throw new NullReferenceException("Cannot infer default index for current connection.");

			return this.GenerateBulkCommand<T>(objects, index);
		}
		private string GenerateBulkCommand<T>(IEnumerable<T> objects, string index) where T : class
		{
			objects.ThrowIfNull("objects");
			index.ThrowIfNullOrEmpty("index");

			var type = typeof(T);
			var typeName = this.InferTypeName<T>();

			return this.GenerateBulkCommand<T>(objects, index, typeName);
		}
		private string GenerateBulkCommand<T>(IEnumerable<T> @objects, string index, string typeName) where T : class 
		{
			if (@objects.Count() == 0)
				return null;
			
			var idSelector = this.CreateIdSelector<T>();
			

			var sb = new StringBuilder();
			var command = "{{ \"index\" : {{ \"_index\" : \"{0}\", \"_type\" : \"{1}\", \"_id\" : \"{2}\" }} }}\n";

			//if we can't reflect id let ES create one.
			if (idSelector == null)
				command = "{{ \"index\" : {{ \"_index\" : \"{0}\", \"_type\" : \"{1}\" }} }}\n".F(index, typeName);

			foreach (var @object in objects)
			{
				string jsonCommand = JsonConvert.SerializeObject(@object, Formatting.None, this.SerializationSettings);
				if (idSelector == null)
					sb.Append(command);
				else
					sb.Append(command.F(index, typeName, idSelector(@object)));
				sb.Append(jsonCommand + "\n");
			}
			var json = sb.ToString();
			return json;
		}

	}
}
