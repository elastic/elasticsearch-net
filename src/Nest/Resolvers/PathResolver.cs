using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Nest.Resolvers
{
	public class PathResolver
	{
    private ElasticInferrer Infer { get; set; } 

		private readonly IConnectionSettings _connectionSettings;

		public PathResolver(IConnectionSettings connectionSettings)
		{
			connectionSettings.ThrowIfNull("hasDefaultIndices");
			this._connectionSettings = connectionSettings;
      this.Infer = new ElasticInferrer(this._connectionSettings);
		}
		
		public string CreateGetPath<T>(GetDescriptor<T> d) where T : class
		{
			var index = d._Index ?? this.Infer.IndexName<T>();
			var type = d._Type ?? this.Infer.TypeName<T>();
			var id = d._Id;
			id.ThrowIfNullOrEmpty("id");

			var path = "/{0}/{1}/{2}".EscapedFormat(index, type.Resolve(this._connectionSettings), id);
			var urlParams = new Dictionary<string, string>();
			if (d._Refresh.HasValue)
				urlParams.Add("refresh", d._Refresh.Value.ToString().ToLower());
			if (d._Realtime.HasValue)
				urlParams.Add("realtime", d._Realtime.Value.ToString().ToLower());
			if (!d._Preference.IsNullOrEmpty())
				urlParams.Add("preference", d._Preference);
			if (!d._Routing.IsNullOrEmpty())
				urlParams.Add("routing", d._Routing);
			if (d._Fields.HasAny())
				urlParams.Add("fields", string.Join(",", d._Fields));

			return path + this.ToQueryString(urlParams);
		}
		
		public string CreatePathFor<T>(T @object, string index = null, string type = null, string id = null) where T : class
		{
			if (index == null)
				index = this.Infer.IndexName<T>();
			if (type == null)
				type = this.Infer.TypeName<T>();
			if (id == null)
				id = this.Infer.Id(@object);

			return this.CreateIndexTypeIdPath(index, type, id);
		}
		
		public string CreateIdOptionalPathFor<T>(T @object, string index = null, string type = null, string id = null) where T : class
		{
			if (index == null)
				index = this.Infer.IndexName<T>();
			if (type == null)
				type = this.Infer.TypeName<T>();
			if (id == null)
				id = this.Infer.Id(@object);

			if (id == null)
				return this.CreateIndexTypePath(index, type);

			return this.CreateIndexTypeIdPath(index, type, id);
		}
		
		
		public string CreateIndexPath(string index, string suffix = null)
		{
			index.ThrowIfNullOrEmpty("index");
			if (suffix != null)
				return "{0}/".EscapedFormat(index) + this.NormalizeSuffix(suffix);
				
			return "{0}/".EscapedFormat(index);
		}
		
		public string CreateIndexPath(IEnumerable<string> indices, string suffix = null)
		{
			indices.ThrowIfEmpty("indices");
			var index = string.Join(",", indices);
			return this.CreateIndexPath(index, suffix);
		}
		
		public string CreateIndexTypePath(string index, string type, string suffix = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			if (suffix != null)
				return "{0}/{1}/".EscapedFormat(index, type) + this.NormalizeSuffix(suffix);

			return "{0}/{1}/".EscapedFormat(index, type);
		}
		
		public string CreateIndexTypePath(IEnumerable<string> indices, IEnumerable<string> types, string suffix = null)
		{
			indices.ThrowIfEmpty("indices");
			types.ThrowIfEmpty("types");
			var index = string.Join(",", indices);
			var type = string.Join(",", types);
			if (suffix != null)
				return "{0}/{1}/".EscapedFormat(index, type) + this.NormalizeSuffix(suffix);

			return "{0}/{1}/".EscapedFormat(index, type);
		}

		public string CreateIndexTypeIdPath(string index, string type, string id, string suffix = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");

			if (suffix != null)
				return "{0}/{1}/{2}/".EscapedFormat(index, type, id) + this.NormalizeSuffix(suffix);

			return "{0}/{1}/{2}".EscapedFormat(index, type, id);
		}


		
		public string AppendSimpleParametersToPath(string path, ISimpleUrlParameters urlParameters)
		{
			if (urlParameters == null)
				return path;

			var parameters = new Dictionary<string, string>();

			if (urlParameters.Replication != Replication.Sync) //sync == default
				parameters.Add("replication", urlParameters.Replication.ToString().ToLower());

			if (urlParameters.Refresh) //false == default
				parameters.Add("refresh","true");

			path = AppendEscapedQueryString(parameters, path);
			return path;
		}

		public string AppendDeleteByQueryParametersToPath(string path, DeleteByQueryParameters urlParameters)
		{
			if (urlParameters == null)
				return path;

			var parameters = new Dictionary<string,string>();

			if (urlParameters.Replication != Replication.Sync) //sync == default
				parameters.Add("replication", urlParameters.Replication.ToString().ToLower());

			if (urlParameters.Consistency != Consistency.Quorum) //quorum == default
				parameters.Add("consistency", urlParameters.Replication.ToString().ToLower());

			if (!urlParameters.Routing.IsNullOrEmpty())
				parameters.Add("routing", urlParameters.Routing);

			path = AppendEscapedQueryString(parameters, path);
			return path;
		}

		public string AppendParametersToPath(string path, IUrlParameters urlParameters)
		{
			if (urlParameters == null)
				return path;

			var parameters = new Dictionary<string, string>();

			if (!urlParameters.Version.IsNullOrEmpty())
				parameters.Add("version", urlParameters.Version);
			if (!urlParameters.Routing.IsNullOrEmpty())
				parameters.Add("routing", urlParameters.Routing);
			if (!urlParameters.Parent.IsNullOrEmpty())
				parameters.Add("parent", urlParameters.Parent);

			if (urlParameters.OpType != OpType.None) // default not set
				parameters.Add("op_type", urlParameters.OpType.ToString().ToLower());

			if (urlParameters.Replication != Replication.Sync) //sync == default
				parameters.Add("replication", urlParameters.Replication.ToString().ToLower());

			if (urlParameters.Consistency != Consistency.Quorum) //quorum == default
				parameters.Add("consistency", urlParameters.Consistency.ToString().ToLower());

			if (urlParameters.Refresh) //false == default
				parameters.Add("refresh", "true");

			if (urlParameters is IndexParameters)
				this.AppendIndexParameters(parameters, (IndexParameters)urlParameters);

			path = AppendEscapedQueryString(parameters, path);
			return path;
		}

		private void AppendIndexParameters(Dictionary<string, string> parameters, IndexParameters indexParameters)
		{
			if (!indexParameters.Timeout.IsNullOrEmpty())
				parameters.Add("timeout", indexParameters.Timeout);

			if (!indexParameters.TTL.IsNullOrEmpty())
				parameters.Add("ttl", indexParameters.TTL);

			if (indexParameters.VersionType != VersionType.Internal) //internal == default
				parameters.Add("version_type", indexParameters.VersionType.ToString().ToLower());
		}

		
		public string GetSearchPathForDynamic(SearchDescriptor<dynamic> descriptor)
		{
			string indices;
			if (descriptor._Indices.HasAny())
				indices = string.Join(",", descriptor._Indices);
			else if (descriptor._Indices != null || descriptor._AllIndices) //if set to empty array asume all
				indices = "_all";
			else
				indices = this._connectionSettings.DefaultIndex;

			string types = (descriptor._Types.HasAny()) ? this.JoinTypes(descriptor._Types) : null;

			var dict = this.GetSearchParameters(descriptor);

			return this.SearchPathJoin(indices, types, dict);
		}

		public string GetSearchPathForTyped<T>(SearchDescriptor<T> descriptor) where T : class
		{
			string indices;
			if (descriptor._Indices.HasAny())
				indices = string.Join(",", descriptor._Indices);
			else if (descriptor._Indices != null || descriptor._AllIndices) //if set to empty array asume all
				indices = "_all";
			else
				indices = this.Infer.IndexName<T>();

			var types = this.Infer.TypeName<T>();
			if (descriptor._Types.HasAny())
				types = this.JoinTypes(descriptor._Types);
			else if (descriptor._Types != null || descriptor._AllTypes) //if set to empty array assume all
				types = null;

			var dict = this.GetSearchParameters(descriptor);

			return this.SearchPathJoin(indices, types, dict);
		}

		
		//TODO merge with GetDeleteByQueryPathForDynamic
		public string GetPathForTyped<T>(QueryPathDescriptor<T> descriptor, string suffix) where T : class
		{
			string indices;
			if (descriptor._Indices.HasAny())
				indices = string.Join(",", descriptor._Indices);
			else if (descriptor._Indices != null || descriptor._AllIndices) //if set to empty array asume all
				indices = "_all";
			else
				indices = this.Infer.IndexName<T>();

			var types = this.Infer.TypeName<T>();
			if (descriptor._Types.HasAny())
				types = this.JoinTypes(descriptor._Types);
			else if (descriptor._Types != null || descriptor._AllTypes) //if set to empty array assume all
				types = null;

			return this.SearchPathJoin(indices, types, descriptor.GetUrlParams(), suffix);
		}

		private string JoinTypes(IEnumerable<TypeNameMarker> markers)
		{
			if (!markers.HasAny())
				return null;
			return string.Join(",", markers.Select(t => t.Resolve(this._connectionSettings)));
		}

		private string NormalizeSuffix(string suffix)
		{
			suffix.ThrowIfNull("suffix");
			if (suffix.StartsWith("/"))
				return suffix.Substring(1, suffix.Length - 1);
			return suffix;
			
		}

		private string JoinParamsAndSegments(IDictionary<string, string> urlparams, params string[] hostSegments)
		{
			var path = string.Join("/", hostSegments.Select(h=>Uri.EscapeDataString(h)));
			if (urlparams != null && urlparams.Any())
			{
				path = AppendEscapedQueryString(urlparams, path);
			}
			return "/" + path;	
		}

		private string SearchPathJoin(string indices, string types, IDictionary<string, string> urlparams, string extension = "_search")
		{
			string path = string.Concat(!string.IsNullOrEmpty(types) ?
											 this.CreateIndexTypePath(indices, types) :
											 this.CreateIndexPath(indices), extension);

			if (urlparams != null && urlparams.Any())
				path = AppendEscapedQueryString(urlparams, path);
			
			return path;
		}

		private Dictionary<string, string> GetSearchParameters<T>(SearchDescriptor<T> descriptor) where T : class
		{
			var dict = new Dictionary<string, string>();
			if (!descriptor._Routing.IsNullOrEmpty())
				dict.Add("routing", descriptor._Routing);
			if (!descriptor._Preference.IsNullOrEmpty())
				dict.Add("preference", descriptor._Preference);
			this.AddSearchType<T>(descriptor, dict);
			return dict;
		}

		private void AddSearchType<T>(SearchDescriptor<T> descriptor, Dictionary<string, string> dict) where T : class
		{
			if (descriptor._SearchType.HasValue)
			{
				switch (descriptor._SearchType.Value)
				{
					case SearchType.Count:
						dict["search_type"] = "count";
						break;
					case SearchType.DfsQueryThenFetch:
						dict["search_type"] =  "dfs_query_then_fetch";
						break;
					case SearchType.DfsQueryAndFetch:
						dict["search_type"] = "dfs_query_and_fetch";
						break;
					case SearchType.QueryThenFetch:
						dict["search_type"] = "query_then_fetch";
						break;
					case SearchType.QueryAndFetch:
						dict["search_type"] =  "query_and_fetch";
						break;
					case SearchType.Scan:
						dict["search_type"] =  "scan";
						break;
				}
			}
		}

		private string AppendEscapedQueryString(IDictionary<string, string> urlparams, string path)
		{
			var queryString = this.ToQueryString(urlparams);
			path += queryString;
			return path;
		}

		private string ToQueryString(IDictionary<string, string> urlParams)
		{
			if (urlParams == null || !urlParams.Any())
				return null;
			var queryString = string.Join("&", urlParams.Select(kv => "{0}={1}".EscapedFormat(kv.Key, kv.Value)));
			return "?{0}".F(queryString);
	
		}
	}
}
