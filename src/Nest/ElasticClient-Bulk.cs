using System;
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
	public partial class ElasticClient
	{
		private Regex _bulkReplace = new Regex(@",\n|^\[", RegexOptions.Compiled | RegexOptions.Multiline);

		public IBulkResponse Bulk(Func<BulkDescriptor, BulkDescriptor> bulkSelector)
		{
			bulkSelector.ThrowIfNull("bulkSelector");
			var bulkDescriptor = bulkSelector(new BulkDescriptor());
			return this.Bulk(bulkDescriptor);
		}
		public IBulkResponse Bulk(BulkDescriptor bulkDescriptor)
		{
			bulkDescriptor.ThrowIfNull("bulkDescriptor");
			var sb = new StringBuilder();
			
			foreach (var operation in bulkDescriptor._Operations)
			{
				var command = operation._Operation;
				var index = operation._Index ??
				            bulkDescriptor._FixedIndex ?? 
							new IndexNameResolver(this.Settings).GetIndexForType(operation._ClrType);
				var typeName = operation._Type
				               ?? bulkDescriptor._FixedType
				               ?? this.GetTypeNameFor(operation._ClrType);

				var id = operation.GetIdForObject(this.IdResolver);
				operation._Index = index;
				operation._Type = typeName;
				operation._Id = id;

				var opJson = JsonConvert.SerializeObject(operation, Formatting.None, IndexSerializationSettings);

				var action = "{{ \"{0}\" :  {1} }}\n".F(command, opJson);
				sb.Append(action);

				if (command == "index" || command == "create")
				{
					string jsonCommand = JsonConvert.SerializeObject(operation._Object, Formatting.None, IndexSerializationSettings);
					sb.Append(jsonCommand + "\n");
				}
			}
			var json = sb.ToString();
			var path = "_bulk";
			if (!bulkDescriptor._FixedIndex.IsNullOrEmpty())
			{
				if (!bulkDescriptor._FixedType.IsNullOrEmpty())
					path = bulkDescriptor._FixedType + "/" + path;
				path = bulkDescriptor._FixedIndex + "/" + path;
			}
			var status = this.Connection.PostSync(path, json);
			return this.ToParsedResponse<BulkResponse>(status);
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
			objects.ThrowIfNull("objects");

			var index = this.IndexNameResolver.GetIndexForType<T>();
			if (string.IsNullOrEmpty(index))
				throw new NullReferenceException("Cannot infer default index for current connection.");

			return this.GenerateBulkCommand<T>(objects, index, command);
		}
		private string GenerateBulkCommand<T>(IEnumerable<BulkParameters<T>> objects, string command) where T : class
		{
			objects.ThrowIfNull("objects");

			var index = this.IndexNameResolver.GetIndexForType<T>();
			if (string.IsNullOrEmpty(index))
				throw new NullReferenceException("Cannot infer default index for current connection.");

			return this.GenerateBulkCommand<T>(objects, index, command);
		}
		private string GenerateBulkCommand<T>(IEnumerable<T> objects, string index, string command) where T : class
		{
			objects.ThrowIfNull("objects");
			index.ThrowIfNullOrEmpty("index");

			var type = typeof(T);
			var typeName = this.GetTypeNameFor<T>();

			return this.GenerateBulkCommand<T>(objects, index, typeName, command);
		}
		private string GenerateBulkCommand<T>(IEnumerable<BulkParameters<T>> objects, string index, string command) where T : class
		{
			objects.ThrowIfNull("objects");
			index.ThrowIfNullOrEmpty("index");

			var type = typeof(T);
			var typeName = this.GetTypeNameFor<T>();

			return this.GenerateBulkCommand<T>(objects, index, typeName, command);
		}


		private string GenerateBulkCommand<T>(IEnumerable<T> @objects, string index, string typeName, string command) where T : class
		{
			if (!@objects.Any())
				return null;

			var sb = new StringBuilder();
			var action = "{{ \"{0}\" : {{ \"_index\" : \"{1}\", \"_type\" : \"{2}\"".F(command, index, typeName);

			foreach (var @object in objects)
			{
				var objectAction = action;
				
					var id = this.IdResolver.GetIdFor(@object);
					if (!id.IsNullOrEmpty())
						objectAction += ", \"_id\" : \"{0}\" ".F(id);

				objectAction += "} }\n";

				sb.Append(objectAction);
				if (command == "index")
				{
					string jsonCommand = JsonConvert.SerializeObject(@object, Formatting.None, IndexSerializationSettings);
					sb.Append(jsonCommand + "\n");
				}
			}
			var json = sb.ToString();
			return json;



		}
		private string GenerateBulkCommand<T>(IEnumerable<BulkParameters<T>> @objects, string index, string typeName, string command) where T : class
		{
			if (!@objects.Any())
				return null;

			var sb = new StringBuilder();
			var action = "{{ \"{0}\" : {{ \"_index\" : \"{1}\", \"_type\" : \"{2}\"".F(command, index, typeName);

			foreach (var @object in objects)
			{
				if (@object.Document == null)
					continue;

				var objectAction = action;

				objectAction += ", \"_id\" : \"{0}\" ".F(this.IdResolver.GetIdFor(@object.Document));

				if (!@object.Version.IsNullOrEmpty())
					objectAction += ", \"version\" : \"{0}\" ".F(@object.Version);
				if (!@object.Parent.IsNullOrEmpty())
					objectAction += ", \"parent\" : \"{0}\" ".F(@object.Parent);
				if (@object.VersionType != VersionType.Internal)
					objectAction += ", \"version_type\" : \"{0}\" ".F(@object.VersionType.ToString().ToLower());
				if (!@object.Routing.IsNullOrEmpty())
					objectAction += ", \"routing\" : \"{0}\" ".F(@object.Routing);
				objectAction += "} }\n";

				sb.Append(objectAction);
				if (command == "index")
				{
					string jsonCommand = JsonConvert.SerializeObject(@object.Document, Formatting.None, IndexSerializationSettings);
					sb.Append(jsonCommand + "\n");
				}
			}
			var json = sb.ToString();
			return json;
		}

	}
}
