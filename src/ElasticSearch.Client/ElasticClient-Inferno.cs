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
			var type = typeof(T);
			var typeName = this.InferTypeName<T>();
			return this.CreatePathFor<T>(@object, index, typeName);
			var path = this.createPath(index, typeName);

			var idProperty = type.GetProperty("Id");
			int? id = null;
			string idString = string.Empty;
			if (idProperty != null)
			{
				if (idProperty.PropertyType == typeof(int))
					id = (int?)@object.TryGetPropertyValue("Id");
				if (idProperty.PropertyType == typeof(string))
					idString = (string)@object.TryGetPropertyValue("Id");
				if (id.HasValue)
					idString = id.Value.ToString();
				if (!string.IsNullOrEmpty(idString))
					path = this.createPath(index, typeName, idString);
			}
			return path;
		}
		private string CreatePathFor<T>(T @object, string index, string type) where T : class
		{
			@object.ThrowIfNull("object");
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");

			var path = this.createPath(index, type);

			var id = this.GetIdFor<T>(@object);
			if (!string.IsNullOrEmpty(id))
				path = this.createPath(index, type, id);

			return path;

		}
		private string CreatePathFor<T>(T @object, string index, string type, string id) where T : class
		{
			@object.ThrowIfNull("object");
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");

			return this.createPath(index, type, id);
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

		private string createPath(string index, string type)
		{
			return "{0}/{1}/".F(index, type);
		}
		private string createPath(string index, string type, string id)
		{
			return "{0}/{1}/{2}".F(index, type, id);
		}

	}
}
