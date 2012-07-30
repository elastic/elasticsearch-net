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
		private static ConcurrentDictionary<Type, Func<object, string>> IdDelegates = new ConcurrentDictionary<Type, Func<object, string>>();

		internal string CreatePathFor<T>(T @object) where T : class
		{
			var index = this.Settings.GetIndexForType<T>();
			if (string.IsNullOrEmpty(index))
				throw new NullReferenceException("Cannot infer default index for current connection.");
			return this.CreatePathFor<T>(@object, index);

		}
		
		internal string CreatePathFor<T>(T @object, string index) where T : class
		{
			//var type = typeof(T);
			var typeName = this.TypeNameResolver.GetTypeNameFor<T>();
			return this.CreatePathFor<T>(@object, index, typeName);

		}
		
		internal string CreatePathFor<T>(T @object, string index, string type) where T : class
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

		internal string CreatePathFor<T>(T @object, string index, string type, string id) where T : class
		{
			@object.ThrowIfNull("object");
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");

			return this.CreatePath(index, type, id);
		}

		internal string CreatePath(string index)
		{
			index.ThrowIfNullOrEmpty("index");
			return "{0}/".F(Uri.EscapeDataString(index));
		}

		internal string CreatePath(string index, string type)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			return "{0}/{1}/".F(Uri.EscapeDataString(index), Uri.EscapeDataString(type));
		}

		internal string CreatePath(string index, string type, string id)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");
			return "{0}/{1}/{2}".F(Uri.EscapeDataString(index), Uri.EscapeDataString(type), Uri.EscapeDataString(id));
		}

		private string AppendSimpleParametersToPath(string path, ISimpleUrlParameters urlParameters)
		{
			if (urlParameters == null)
				return path;

			var parameters = new List<string>();

			if (urlParameters.Replication != Replication.Sync) //sync == default
				parameters.Add("replication=" + urlParameters.Replication.ToString().ToLower());


			if (urlParameters.Refresh) //false == default
				parameters.Add("refresh=true");

			path += "?" + string.Join("&", parameters.ToArray());
			return path;
		}
	 
		private string AppendDeleteByQueryParametersToPath(string path, DeleteByQueryParameters urlParameters)
		{
			if (urlParameters == null)
				return path;

			var parameters = new List<string>();

			if (urlParameters.Replication != Replication.Sync) //sync == default
				parameters.Add("replication=" + urlParameters.Replication.ToString().ToLower());

			if (urlParameters.Consistency != Consistency.Quorum) //quorum == default
				parameters.Add("consistency=" + urlParameters.Replication.ToString().ToLower());

			if (!urlParameters.Routing.IsNullOrEmpty())
				parameters.Add("routing=" + urlParameters.Routing);

			path += "?" + string.Join("&", parameters.ToArray());
			return path;
		}
	 
		private string AppendParametersToPath(string path, IUrlParameters urlParameters)
		{
			if (urlParameters == null)
				return path;

			var parameters = new List<string>();

			if (!urlParameters.Version.IsNullOrEmpty())
				parameters.Add("version=" + urlParameters.Version);
			if (!urlParameters.Routing.IsNullOrEmpty())
				parameters.Add("routing=" + urlParameters.Routing);
			if (!urlParameters.Parent.IsNullOrEmpty())
				parameters.Add("parent=" + urlParameters.Parent);

			if (urlParameters.Replication != Replication.Sync) //sync == default
				parameters.Add("replication=" + urlParameters.Replication.ToString().ToLower());

			if (urlParameters.Consistency != Consistency.Quorum) //quorum == default
				parameters.Add("consistency=" + urlParameters.Consistency.ToString().ToLower());

			if (urlParameters.Refresh) //false == default
				parameters.Add("refresh=true");

			if (urlParameters is IndexParameters)
				this.AppendIndexParameters(parameters, (IndexParameters)urlParameters);

			path += "?" + string.Join("&", parameters.ToArray());
			return path;
		}

		private List<string> AppendIndexParameters(List<string> parameters, IndexParameters indexParameters)
		{
			if (indexParameters == null)
				return parameters;

			if (!indexParameters.Timeout.IsNullOrEmpty())
				parameters.Add("timeout=" + indexParameters.Timeout);

			if (indexParameters.VersionType != VersionType.Internal) //internal == default
				parameters.Add("version_type=" + indexParameters.VersionType.ToString().ToLower());

			return parameters;
		}

		internal Func<T, string> CreateIdSelector<T>() where T : class
		{
			//TODO this idselection stuff for the bulk seems obsolete.
			Func<T, string> idSelector = (@object) => this.GetIdFor(@object);
			return idSelector;
		}

		internal static Func<T, object> MakeDelegate<T, U>(MethodInfo @get)
		{
			var f = (Func<T, U>)Delegate.CreateDelegate(typeof(Func<T, U>), @get);
			return t => f(t);
		}

		public string GetIdFor<T>(T @object)
		{
			var type = typeof(T);
			Func<object, string> cachedLookup;
			if (IdDelegates.TryGetValue(type, out cachedLookup))
				return cachedLookup(@object);

			var esTypeAtt = PropertyNameResolver.GetElasticPropertyFor(type);
			var propertyName = (esTypeAtt != null) ? esTypeAtt.IdProperty : string.Empty;
			if (string.IsNullOrWhiteSpace(propertyName))
				propertyName = "Id";

			var idProperty = type.GetProperty(propertyName);
			if (idProperty == null)
			{
				throw new Exception("Could not infer id for object of type" + type.FullName);
			}
			try
			{
				var getMethod = idProperty.GetGetMethod();
				var method = typeof(ElasticClient).GetMethod("MakeDelegate", BindingFlags.Static | BindingFlags.NonPublic);
				var generic = method.MakeGenericMethod(type, getMethod.ReturnType);
				Func<T, object> func = (Func<T, object>)generic.Invoke(null, new[] { getMethod });
				cachedLookup = o =>
				{
					T obj = (T)o;
					var v = func(obj);
					return v.ToString();
				};
				IdDelegates.TryAdd(type, cachedLookup);
				return cachedLookup(@object);

			}
			catch (Exception e)
			{
				var value = idProperty.GetValue(@object, null);
				return value.ToString();
			}
		}

		private string GetPathForDynamic(SearchDescriptor<dynamic> descriptor)
		{
			string indices;
			if (descriptor._Indices.HasAny())
				indices = string.Join(",", descriptor._Indices);
			else if (descriptor._Indices != null || descriptor._AllIndices) //if set to empty array asume all
				indices = "_all";
			else
				indices = this.Settings.DefaultIndex;

			string types = (descriptor._Types.HasAny()) ? string.Join(",", descriptor._Types) : null;

			return this.PathJoin(indices, types, descriptor._Routing);
		}
		internal string GetPathForTyped<T>(SearchDescriptor<T> descriptor) where T : class
		{
			string indices;
			if (descriptor._Indices.HasAny())
				indices = string.Join(",", descriptor._Indices);
			else if (descriptor._Indices != null || descriptor._AllIndices) //if set to empty array asume all
				indices = "_all";
			else
				indices = this.Settings.GetIndexForType<T>();

			var types = this.TypeNameResolver.GetTypeNameFor<T>();
			if (descriptor._Types.HasAny())
				types = string.Join(",", descriptor._Types);
			else if (descriptor._Types != null || descriptor._AllTypes) //if set to empty array assume all
				types = null;

			return this.PathJoin(indices, types, descriptor._Routing);
		}
		private string GetPathForDynamic(QueryPathDescriptor<dynamic> descriptor)
		{
			string indices;
			if (descriptor._Indices.HasAny())
				indices = string.Join(",", descriptor._Indices);
			else if (descriptor._Indices != null || descriptor._AllIndices) //if set to empty array asume all
				indices = "_all";
			else
				indices = this.Settings.DefaultIndex;

			string types = (descriptor._Types.HasAny()) ? string.Join(",", descriptor._Types) : null;

			return this.PathJoin(indices, types, descriptor._Routing, "_query");
		}
		private string GetPathForTyped<T>(QueryPathDescriptor<T> descriptor) where T : class
		{
			string indices;
			if (descriptor._Indices.HasAny())
				indices = string.Join(",", descriptor._Indices);
			else if (descriptor._Indices != null || descriptor._AllIndices) //if set to empty array asume all
				indices = "_all";
			else
				indices = this.Settings.GetIndexForType<T>();

			var types = this.TypeNameResolver.GetTypeNameFor<T>();
			if (descriptor._Types.HasAny())
				types = string.Join(",", descriptor._Types);
			else if (descriptor._Types != null || descriptor._AllTypes) //if set to empty array assume all
				types = null;

			return this.PathJoin(indices, types, descriptor._Routing, "_query");
		}
		private string PathJoin(string indices, string types, string routing, string extension = "_search")
		{
			string path = string.Concat(!string.IsNullOrEmpty(types) ?
											 this.CreatePath(indices, types) :
											 this.CreatePath(indices), extension);
			if (!String.IsNullOrEmpty(routing))
			{
				path = "{0}?routing={1}".F(path, routing);
			}
			return path;
		}

	}
}
