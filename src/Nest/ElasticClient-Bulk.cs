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
			var typeName = this.TypeNameResolver.GetTypeNameFor<T>();

			return this.GenerateBulkCommand<T>(objects, index, typeName, command);
		}
		private string GenerateBulkCommand<T>(IEnumerable<BulkParameters<T>> objects, string index, string command) where T : class
		{
			objects.ThrowIfNull("objects");
			index.ThrowIfNullOrEmpty("index");

			var type = typeof(T);
			var typeName = this.TypeNameResolver.GetTypeNameFor<T>();

			return this.GenerateBulkCommand<T>(objects, index, typeName, command);
		}


		private string GenerateBulkCommand<T>(IEnumerable<T> @objects, string index, string typeName, string command) where T : class
		{
			if (!@objects.Any())
				return null;

	  var idSelector = this.IdResolver.CreateIdSelector<T>();

			var sb = new StringBuilder();
			var action = "{{ \"{0}\" : {{ \"_index\" : \"{1}\", \"_type\" : \"{2}\"".F(command, index, typeName);

			foreach (var @object in objects)
			{
				var objectAction = action;
				if (idSelector != null)
					objectAction += ", \"_id\" : \"{0}\" ".F(idSelector(@object));

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

	  var idSelector = this.IdResolver.CreateIdSelector<T>();

			var sb = new StringBuilder();
			var action = "{{ \"{0}\" : {{ \"_index\" : \"{1}\", \"_type\" : \"{2}\"".F(command, index, typeName);

			foreach (var @object in objects)
			{
				if (@object.Document == null)
					continue;

				var objectAction = action;
				if (idSelector != null)
					objectAction += ", \"_id\" : \"{0}\" ".F(idSelector(@object.Document));

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
					string jsonCommand = JsonConvert.SerializeObject(@object.Document, Formatting.None, SerializationSettings);
					sb.Append(jsonCommand + "\n");
				}
			}
			var json = sb.ToString();
			return json;
		}

	}
}
