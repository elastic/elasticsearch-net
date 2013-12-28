using System;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Nest.Resolvers;
using System.Reflection;
using System.Collections.Concurrent;

namespace Nest
{
    using System.Threading.Tasks;

    public partial class ElasticClient
	{
		public IBulkResponse Bulk(Func<BulkDescriptor, BulkDescriptor> bulkSelector)
		{
			bulkSelector.ThrowIfNull("bulkSelector");
			var bulkDescriptor = bulkSelector(new BulkDescriptor());
			return this.Bulk(bulkDescriptor);
		}

        private void GenerateBulkPathAndJson(BulkDescriptor bulkDescriptor, out string json, out string path)
        {
            bulkDescriptor.ThrowIfNull("bulkDescriptor");
            bulkDescriptor._Operations.ThrowIfEmpty("Bulk descriptor does not define any operations");
            var sb = new StringBuilder();

            foreach (var operation in bulkDescriptor._Operations)
            {
                var command = operation._Operation;
                var index = operation._Index ??
                            bulkDescriptor._FixedIndex ??
                            new IndexNameResolver(this._connectionSettings).GetIndexForType(operation._ClrType);
                var typeName = operation._Type
                               ?? bulkDescriptor._FixedType
                               ?? this.Infer.TypeName(operation._ClrType);

                var id = operation.GetIdForObject(this.Infer);
                operation._Index = index;
                operation._Type = typeName;
                operation._Id = id;

                var opJson = this.Serializer.Serialize(operation, Formatting.None);

                var action = "{{ \"{0}\" :  {1} }}\n".F(command, opJson);
                sb.Append(action);

                if (command == "index" || command == "create")
                {
                    string jsonCommand = this.Serializer.Serialize(operation._Object, Formatting.None);
                    sb.Append(jsonCommand + "\n");
                }
                else if (command == "update")
                {
                    string jsonCommand = this.Serializer.Serialize(operation.GetBody(), Formatting.None);
                    sb.Append(jsonCommand + "\n");
                }
            }
            json = sb.ToString();
            path = "_bulk";
            if (!bulkDescriptor._FixedIndex.IsNullOrEmpty())
            {
                if (!bulkDescriptor._FixedType.IsNullOrEmpty())
                    path = bulkDescriptor._FixedType + "/" + path;
                path = bulkDescriptor._FixedIndex + "/" + path;
            }
	        var queryString = new NameValueCollection();
	        if (bulkDescriptor._Refresh.HasValue)
		        queryString.Add("refresh", bulkDescriptor._Refresh.ToString().ToLowerInvariant());
	        switch (bulkDescriptor._Consistency)
	        {
		        case Consistency.All:
			        queryString.Add("consistency", "all");
					break;
				case Consistency.Quorum:
					queryString.Add("consistency", "quorem");
					break;
				case Consistency.One:
					queryString.Add("consistency", "one");
					break;
	        }
	        if (queryString.HasKeys())
		        path += queryString.ToQueryString();

        }
	
		public IBulkResponse Bulk(BulkDescriptor bulkDescriptor)
		{
		    string json, path;
		    this.GenerateBulkPathAndJson(bulkDescriptor, out json, out path);
			var status = this.Connection.PostSync(path, json);
			return this.Deserialize<BulkResponse>(status);
		}
        public Task<IBulkResponse> BulkAsync(BulkDescriptor bulkDescriptor)
        {
            string json, path;
            this.GenerateBulkPathAndJson(bulkDescriptor, out json, out path);
            var task = this.Connection.Post(path, json);
            return task.ContinueWith(t => (IBulkResponse)this.Deserialize<BulkResponse>(t.Result));
        }

		internal string GenerateBulkIndexCommand<T>(IEnumerable<T> objects) where T : class
		{
			return this.GenerateBulkCommand<T>(@objects, "index");
		}
		private string GenerateBulkIndexCommand<T>(IEnumerable<BulkParameters<T>> objects) where T : class
		{
			return this.GenerateBulkCommand<T>(@objects, "index");
		}
		private string GenerateBulkIndexCommand<T>(IEnumerable<T> objects, string index) where T : class
		{
			return this.GenerateBulkCommand<T>(@objects, index, "index");
		}
		private string GenerateBulkIndexCommand<T>(IEnumerable<BulkParameters<T>> objects, string index) where T : class
		{
			return this.GenerateBulkCommand<T>(@objects, index, "index");
		}
		private string GenerateBulkIndexCommand<T>(IEnumerable<T> @objects, string index, string typeName) where T : class
		{
			return this.GenerateBulkCommand<T>(@objects, index, typeName, "index");
		}
		private string GenerateBulkIndexCommand<T>(IEnumerable<BulkParameters<T>> @objects, string index, string typeName) where T : class
		{
			return this.GenerateBulkCommand<T>(@objects, index, typeName, "index");
		}

		private string GenerateBulkDeleteCommand<T>(IEnumerable<T> objects) where T : class
		{
			return this.GenerateBulkCommand<T>(@objects, "delete");
		}
		private string GenerateBulkDeleteCommand<T>(IEnumerable<BulkParameters<T>> objects) where T : class
		{
			return this.GenerateBulkCommand<T>(@objects, "delete");
		}
		private string GenerateBulkDeleteCommand<T>(IEnumerable<T> objects, string index) where T : class
		{
			return this.GenerateBulkCommand<T>(@objects, index, "delete");
		}
		private string GenerateBulkDeleteCommand<T>(IEnumerable<BulkParameters<T>> objects, string index) where T : class
		{
			return this.GenerateBulkCommand<T>(@objects, index, "delete");
		}
		private string GenerateBulkDeleteCommand<T>(IEnumerable<T> @objects, string index, string typeName) where T : class
		{
			return this.GenerateBulkCommand<T>(@objects, index, typeName, "delete");
		}
		private string GenerateBulkDeleteCommand<T>(IEnumerable<BulkParameters<T>> @objects, string index, string typeName) where T : class
		{
			return this.GenerateBulkCommand<T>(@objects, index, typeName, "delete");
		}

		private string GenerateBulkCommand<T>(IEnumerable<T> objects, string command) where T : class
		{
			objects.ThrowIfEmpty("objects");

			var index = this.Infer.IndexName<T>();
			if (string.IsNullOrEmpty(index))
				throw new NullReferenceException("Cannot infer default index for current connection.");

			return this.GenerateBulkCommand<T>(objects, index, command);
		}
		private string GenerateBulkCommand<T>(IEnumerable<BulkParameters<T>> objects, string command) where T : class
		{
			objects.ThrowIfEmpty("objects");

			var index = this.Infer.IndexName<T>();
			if (string.IsNullOrEmpty(index))
				throw new NullReferenceException("Cannot infer default index for current connection.");

			return this.GenerateBulkCommand<T>(objects, index, command);
		}
		private string GenerateBulkCommand<T>(IEnumerable<T> objects, string index, string command) where T : class
		{
			objects.ThrowIfEmpty("objects");
			index.ThrowIfNullOrEmpty("index");

			var type = typeof(T);
			var typeName = this.Infer.TypeName<T>();

			return this.GenerateBulkCommand<T>(objects, index, typeName, command);
		}
		private string GenerateBulkCommand<T>(IEnumerable<BulkParameters<T>> objects, string index, string command) where T : class
		{
			objects.ThrowIfEmpty("objects");
			index.ThrowIfNullOrEmpty("index");

			var type = typeof(T);
			var typeName = this.Infer.TypeName<T>();

			return this.GenerateBulkCommand<T>(objects, index, typeName, command);
		}



		//used by IndexMany and DeleteMany
		private string GenerateBulkCommand<T>(IEnumerable<T> @objects, string index, string typeName, string command) where T : class
		{
			objects.ThrowIfEmpty("objects");

			var b = new BulkDescriptor();
			b.FixedPath(index, typeName);
			foreach (var @object in @objects)
			{
				var o = @object;
				if (command == "index")
					b.Index<T>(bb => bb.Object(o));
				else if (command == "delete")
					b.Delete<T>(bb => bb.Object(o));
			}
			
			string json, path;
			this.GenerateBulkPathAndJson(b, out json, out path);
			return json;
		}


		//used by IndexMany and DeleteMany
		private string GenerateBulkCommand<T>(IEnumerable<BulkParameters<T>> @objects, string index, string typeName, string command) where T : class
		{
			objects.ThrowIfEmpty("objects");


			var b = new BulkDescriptor();
			b.FixedPath(index, typeName);
			foreach (var @object in @objects)
			{
				var o = @object;
				if (command == "index")
					b.Index<T>(bb => bb
						.Object(o.Document)
						.Id(o.Id)
						.Parent(o.Parent)
						.Percolate(o.Percolate)
						.Routing(o.Routing)
						.Timestamp(o.Timestamp)
						.Ttl(o.Ttl)
						.Version(o.Version)
						.VersionType(o.VersionType));
				else if (command == "delete")
					b.Delete<T>(bb => bb
						.Object(o.Document)
						.Parent(o.Parent)
						.Routing(o.Routing)
						.Timestamp(o.Timestamp)
						.Ttl(o.Ttl)
						.Version(o.Version)
						.VersionType(o.VersionType));
			}

			string json, path;
			this.GenerateBulkPathAndJson(b, out json, out path);
			return json;
		}

	}
}
