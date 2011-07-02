using System;
using Fasterflect;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace ElasticSearch.Client
{

	public partial class ElasticClient
	{
		private Regex _bulkReplace = new Regex(@",\n|^\[", RegexOptions.Compiled | RegexOptions.Multiline);

		private Func<T, string> CreateIdSelector<T>() where T : class
		{
			Func<T, string> idSelector = null;
			var type = typeof(T);
			var idProperty = type.GetProperty("Id");
			if (idProperty != null)
			{
				if (idProperty.PropertyType == typeof(int))
					idSelector = (@object) => ((int)@object.TryGetPropertyValue("Id")).ToString();
				else if (idProperty.PropertyType == typeof(int?))
					idSelector = (@object) =>
					{
						int? val = (int?)@object.TryGetPropertyValue("Id");
						return (val.HasValue) ? val.Value.ToString() : string.Empty;
					};
				else if (idProperty.PropertyType == typeof(string))
					idSelector = (@object) => (string)@object.TryGetPropertyValue("Id");
				else
					idSelector = (@object) => (string)Convert.ChangeType(@object.TryGetPropertyValue("Id"), typeof(string), CultureInfo.InvariantCulture);
			}
			return idSelector;
		}

		private string CreatePathFor<T>(T @object) where T : class
		{
			var index = this.Settings.DefaultIndex;
			if (string.IsNullOrEmpty(index))
				throw new NullReferenceException("Cannot infer default index for current connection.");
			return this.CreatePathFor<T>(@object, index);

		}
		private string CreatePathFor<T>(T @object, string index) where T : class
		{
			//var type = typeof(T);
			var typeName = this.InferTypeName<T>();
			return this.CreatePathFor<T>(@object, index, typeName);
			
		}
		private string CreatePathFor<T>(T @object, string index, string type) where T : class
		{
			@object.ThrowIfNull("object");
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");

			var path = this.CreatePath(index, type);

			var id = this.GetIdFor<T>(@object);
			if (!string.IsNullOrEmpty(id))
				path = this.CreatePath(index, type, id);

			return path;

		}
		private string CreatePathFor<T>(T @object, string index, string type, string id) where T : class
		{
			@object.ThrowIfNull("object");
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");

			return this.CreatePath(index, type, id);
		}

		private string GetIdFor<T>(T @object)
		{
			var type = typeof(T);
			var idProperty = type.GetProperty("Id");
			int? id = null;
			string idString = string.Empty;
			if (idProperty != null)
			{
				if (idProperty.PropertyType == typeof(int)
					|| idProperty.PropertyType == typeof(int?))
					id = (int?)@object.TryGetPropertyValue("Id");
				if (idProperty.PropertyType == typeof(string))
					idString = (string)@object.TryGetPropertyValue("Id");
				if (id.HasValue)
					idString = id.Value.ToString();
			}
			return idString;
		}

		private string InferTypeName<T>() where T : class
		{
			var type = typeof(T);
			var typeName = type.Name;
			if (this.Settings.TypeNameInferrer != null)
				typeName = this.Settings.TypeNameInferrer(typeName);
			if (this.Settings.TypeNameInferrer == null || string.IsNullOrEmpty(typeName))
				typeName = Inflector.MakePlural(type.Name).ToLower();
			return typeName;
		}

		private string CreatePath(string index, string type)
		{
			return "{0}/{1}/".F(index, type);
		}
		private string CreatePath(string index, string type, string id)
		{
			return "{0}/{1}/{2}".F(index, type, id);
		}

		private string GenerateBulkIndexCommand<T>(IEnumerable<T> objects) where T : class
		{
			return this.GenerateBulkCommand<T>(@objects, "index");
		}
		private string GenerateBulkIndexCommand<T>(IEnumerable<T> objects, string index) where T : class
		{
			return this.GenerateBulkCommand<T>(@objects, index, "index");
		}
		private string GenerateBulkIndexCommand<T>(IEnumerable<T> @objects, string index, string typeName) where T : class 
		{
			return this.GenerateBulkCommand<T>(@objects, index, typeName, "index");
		}

		private string GenerateBulkDeleteCommand<T>(IEnumerable<T> objects) where T : class
		{
			return this.GenerateBulkCommand<T>(@objects, "delete");
		}
		private string GenerateBulkDeleteCommand<T>(IEnumerable<T> objects, string index) where T : class
		{
			return this.GenerateBulkCommand<T>(@objects, index, "delete");
		}
		private string GenerateBulkDeleteCommand<T>(IEnumerable<T> @objects, string index, string typeName) where T : class
		{
			return this.GenerateBulkCommand<T>(@objects, index, typeName, "delete");
		}

		private string GenerateBulkCommand<T>(IEnumerable<T> objects, string command) where T : class
		{
			objects.ThrowIfNull("objects");

			var index = this.Settings.DefaultIndex;
			if (string.IsNullOrEmpty(index))
				throw new NullReferenceException("Cannot infer default index for current connection.");

			return this.GenerateBulkIndexCommand<T>(objects, index, command);
		}
		private string GenerateBulkCommand<T>(IEnumerable<T> objects, string index, string command) where T : class
		{
			objects.ThrowIfNull("objects");
			index.ThrowIfNullOrEmpty("index");

			var type = typeof(T);
			var typeName = this.InferTypeName<T>();

			return this.GenerateBulkCommand<T>(objects, index, typeName, command);
		}
		private string GenerateBulkCommand<T>(IEnumerable<T> @objects, string index, string typeName, string command) where T : class
		{
			if (!@objects.Any())
				return null;

			var idSelector = this.CreateIdSelector<T>();


			var sb = new StringBuilder();
			var action = "{{ \"" + command + "\" : {{ \"_index\" : \"{0}\", \"_type\" : \"{1}\", \"_id\" : \"{2}\" }} }}\n";

			//if we can't reflect id let ES create one.
			if (idSelector == null)
				action = "{{ \"" + command + "\" : {{ \"_index\" : \"{0}\", \"_type\" : \"{1}\" }} }}\n".F(index, typeName);

			foreach (var @object in objects)
			{
				string jsonCommand = JsonConvert.SerializeObject(@object, Formatting.None, this.SerializationSettings);
				if (idSelector == null)
					sb.Append(action);
				else
					sb.Append(action.F(index, typeName, idSelector(@object)));
				sb.Append(jsonCommand + "\n");
			}
			var json = sb.ToString();
			return json;
		}
	


		private string AppendToDeletePath(string path, DeleteParameters deleteParameters)
		{
			if (deleteParameters == null)
				return path;

			var parameters = new List<string>();
			
			if (!deleteParameters.Version.IsNullOrEmpty())
				parameters.Add("version="+ deleteParameters.Version);
			if (!deleteParameters.Routing.IsNullOrEmpty())
				parameters.Add("routing=" + deleteParameters.Routing);
			if (!deleteParameters.Parent.IsNullOrEmpty())
				parameters.Add("parent=" + deleteParameters.Parent);

			if (deleteParameters.Replication != Replication.Sync) //sync == default
				parameters.Add("replication=" + deleteParameters.Replication.ToString().ToLower());

			if (deleteParameters.Consistency != Consistency.Quorum) //sync == default
				parameters.Add("consistency=" + deleteParameters.Consistency.ToString().ToLower());

			if (deleteParameters.Refresh) //sync == default
				parameters.Add("refresh=true");

			path += "?" + string.Join("&", parameters);
			return path;
		}

	}
}
