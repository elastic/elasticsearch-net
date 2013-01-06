using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest.Resolvers
{
	public class PathResolver
	{
		private readonly IConnectionSettings _connectionSettings;
		private readonly IndexNameResolver _indexNameResolver;
		private readonly TypeNameResolver _typeNameResolver;
		private readonly IdResolver _idResolver;

		public PathResolver(IConnectionSettings connectionSettings)
		{
			connectionSettings.ThrowIfNull("hasDefaultIndices");
			this._connectionSettings = connectionSettings;
			this._indexNameResolver = new IndexNameResolver(connectionSettings);
			this._typeNameResolver = new TypeNameResolver();
			this._idResolver = new IdResolver();
		}

		public string CreatePathFor<T>(T @object, string index = null, string type = null, string id = null) where T : class
		{
			if (index == null)
				index = this._indexNameResolver.GetIndexForType<T>();
			if (type == null)
				type = this._typeNameResolver.GetTypeNameFor<T>();
			if (id == null)
				id = this._idResolver.GetIdFor<T>(@object);

			return this.CreateIndexTypeIdPath(index, type, id);
		}
		public string CreateIdOptionalPathFor<T>(T @object, string index = null, string type = null, string id = null) where T : class
		{
			if (index == null)
				index = this._indexNameResolver.GetIndexForType<T>();
			if (type == null)
				type = this._typeNameResolver.GetTypeNameFor<T>();
			if (id == null)
				id = this._idResolver.GetIdFor<T>(@object);

			if (id == null)
				return this.CreateIndexTypePath(index, type);

			return this.CreateIndexTypeIdPath(index, type, id);
		}
		
		public string CreateClusterPath(string suffix = null)
		{
			suffix.ThrowIfNullOrEmpty("suffix");
			return "_cluster/{0}/".F(suffix);
		}

		public string CreateClusterPath(IEnumerable<string> indices, string suffix = null)
		{
			indices.ThrowIfEmpty("indices");
			suffix.ThrowIfNullOrEmpty("suffix");
			var index = string.Join(",", indices);
			return "_cluster/{0}/{1}".F(suffix, index);
		}

		public string CreateNodePath(string suffix = null)
		{
			return suffix.IsNullOrEmpty() ? "_nodes" : "_nodes/{0}/".F(suffix);
		}

		public string CreateNodePath(IEnumerable<string> nodes, string suffix = null)
		{
			nodes.ThrowIfEmpty("indices");
			var nodeStr = string.Join(",", nodes);
			return suffix.IsNullOrEmpty() ? "_nodes/{0}".F(nodeStr) : "_nodes/{0}/{1}".F(nodeStr, suffix);
		}

		//19
		public string CreateIndexPath(string index, string suffix = null)
		{
			index.ThrowIfNullOrEmpty("index");
			if (suffix != null)
				return "{0}/{1}".F(Uri.EscapeDataString(index), this.NormalizeSuffix(suffix));
				
			return "{0}/".F(Uri.EscapeDataString(index));
		}
		
		public string CreateIndexPath(IEnumerable<string> indices, string suffix = null)
		{
			indices.ThrowIfEmpty("indices");
			var index = string.Join(",", indices);
			return this.CreateIndexPath(index, suffix);
		}
		//14
		
		public string CreateIndexTypePath(string index, string type, string suffix = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			index = Uri.EscapeDataString(index);
			type = Uri.EscapeDataString(type);

			if (suffix != null)
				return "{0}/{1}/{2}".F(index, type, this.NormalizeSuffix(suffix));
			return "{0}/{1}/".F(index, type);
		}
		
		public string CreateIndexTypePath(IEnumerable<string> indices, IEnumerable<string> types, string suffix = null)
		{
			indices.ThrowIfEmpty("indices");
			types.ThrowIfEmpty("types");
			var index = string.Join(",", indices);
			var type = string.Join(",", types);

			return "{0}/{1}/".F(Uri.EscapeDataString(index), Uri.EscapeDataString(type));
		}

		//16
		public string CreateIndexTypeIdPath(string index, string type, string id, string suffix = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");

			index = Uri.EscapeDataString(index);
			type = Uri.EscapeDataString(type);
			id = Uri.EscapeDataString(id);

			if (suffix != null)
				return "{0}/{1}/{2}/{3}".F(index, type,id, this.NormalizeSuffix(suffix));

			return "{0}/{1}/{2}".F(index, type, id);
		}



		public string AppendSimpleParametersToPath(string path, ISimpleUrlParameters urlParameters)
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

		public string AppendDeleteByQueryParametersToPath(string path, DeleteByQueryParameters urlParameters)
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

		public string AppendParametersToPath(string path, IUrlParameters urlParameters)
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

		public string GetSearchPathForDynamic(SearchDescriptor<dynamic> descriptor)
		{
			string indices;
			if (descriptor._Indices.HasAny())
				indices = string.Join(",", descriptor._Indices);
			else if (descriptor._Indices != null || descriptor._AllIndices) //if set to empty array asume all
				indices = "_all";
			else
				indices = this._connectionSettings.DefaultIndex;

			string types = (descriptor._Types.HasAny()) ? string.Join(",", descriptor._Types) : null;

			var dict = this.GetSearchParameters(descriptor);

			return this.SearchPathJoin(indices, types, dict, "_search");
		}
		public string GetSearchPathForTyped<T>(SearchDescriptor<T> descriptor) where T : class
		{
			string indices;
			if (descriptor._Indices.HasAny())
				indices = string.Join(",", descriptor._Indices);
			else if (descriptor._Indices != null || descriptor._AllIndices) //if set to empty array asume all
				indices = "_all";
			else
				indices = this._indexNameResolver.GetIndexForType<T>();

			var types = this._typeNameResolver.GetTypeNameFor<T>();
			if (descriptor._Types.HasAny())
				types = string.Join(",", descriptor._Types);
			else if (descriptor._Types != null || descriptor._AllTypes) //if set to empty array assume all
				types = null;

			var dict = this.GetSearchParameters(descriptor);
				

			return this.SearchPathJoin(indices, types, dict, "_search");
		}
		
		public string GetPathForDynamic(QueryPathDescriptor<dynamic> descriptor, string suffix)
		{
			string indices;
			if (descriptor._Indices.HasAny())
				indices = string.Join(",", descriptor._Indices);
			else if (descriptor._Indices != null || descriptor._AllIndices) //if set to empty array asume all
				indices = "_all";
			else
				indices = this._connectionSettings.DefaultIndex;

			string types = (descriptor._Types.HasAny()) ? string.Join(",", descriptor._Types) : null;

			return this.SearchPathJoin(indices, types, descriptor.GetUrlParams(), suffix);
		}
		public string GetPathForTyped<T>(QueryPathDescriptor<T> descriptor, string suffix) where T : class
		{
			string indices;
			if (descriptor._Indices.HasAny())
				indices = string.Join(",", descriptor._Indices);
			else if (descriptor._Indices != null || descriptor._AllIndices) //if set to empty array asume all
				indices = "_all";
			else
				indices = this._indexNameResolver.GetIndexForType<T>();

			var types = this._typeNameResolver.GetTypeNameFor<T>();
			if (descriptor._Types.HasAny())
				types = string.Join(",", descriptor._Types);
			else if (descriptor._Types != null || descriptor._AllTypes) //if set to empty array assume all
				types = null;

			return this.SearchPathJoin(indices, types, descriptor.GetUrlParams(), suffix);
		}

		public string GetMoreLikeThisPathFor<T>(MoreLikeThisDescriptor<T> descriptor) where T : class
		{
			var index = descriptor._Index;
			if (index.IsNullOrEmpty())
				index = this._indexNameResolver.GetIndexForType<T>();

			var type = descriptor._Type;
			if (type.IsNullOrEmpty())
				type = this._typeNameResolver.GetTypeNameFor<T>();

			var id = descriptor._Id;
			
			var dict = new Dictionary<string, string>();
			if (descriptor._Options != null)
			{
				var options = descriptor._Options;
				if (options._Fields.HasAny())
				{
					var fields = string.Join(",", options._Fields);
					dict.Add("mlt_fields", fields);
				}
				if (options._StopWords.HasAny())
				{
					var stopwords = string.Join(",", options._StopWords);
					dict.Add("stop_words", stopwords);
				}
				if (!options._LikeText.IsNullOrEmpty())
					dict.Add("like_text", options._LikeText);
				if (options._TermMatchPercentage != null)
					dict.Add("percent_terms_to_match", options._TermMatchPercentage.ToString());
				if (options._MinTermFrequency != null)
					dict.Add("min_term_freq", options._MinTermFrequency.ToString());
				if (options._MaxQueryTerms != null)
					dict.Add("max_query_terms", options._MaxQueryTerms.ToString());
				if (options._MinDocumentFrequency != null)
					dict.Add("min_doc_freq", options._MinDocumentFrequency.ToString());
				if (options._MaxDocumentFrequency != null)
					dict.Add("max_doc_freq", options._MaxDocumentFrequency.ToString());
				if (options._MinWordLength != null)
					dict.Add("min_word_len", options._MinWordLength.ToString());
				if (options._MaxWordLength != null)
					dict.Add("max_word_len", options._MaxWordLength.ToString());
				if (options._BoostTerms != null)
					dict.Add("boost_terms", options._BoostTerms.ToString());
				if (options._Boost != null)
					dict.Add("boost", options._Boost.ToString());
				if (!options._Analyzer.IsNullOrEmpty())
					dict.Add("analyzer", options._Analyzer);
			}
			if (descriptor._Search != null)
			{
				var searchDict = this.GetSearchParameters(descriptor._Search);
				foreach(var kv in searchDict)
					dict.Add(kv.Key, kv.Value);
				this.AddSearchType(descriptor._Search, dict);
			}

			var path = this.JoinParamsAndSegments(dict, index, type, id, "_mlt");
			return path;
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
			var path = string.Join("/", hostSegments);
			if (urlparams != null && urlparams.Any())
			{
				var queryString = string.Join("&", urlparams.Select(kv => "{0}={1}".F(kv.Key, Uri.EscapeDataString(kv.Value))));
				path += "?{0}".F(queryString);
			}
			return "/" + path;
		}

		private string SearchPathJoin(string indices, string types, IDictionary<string, string> urlparams, string extension = "_search")
		{
			string path = string.Concat(!string.IsNullOrEmpty(types) ?
											 this.CreateIndexTypePath(indices, types) :
											 this.CreateIndexPath(indices), extension);

			if (urlparams != null && urlparams.Any())
			{
				var queryString = string.Join("&", urlparams.Select(kv=> "{0}={1}".F(kv.Key, Uri.EscapeDataString(kv.Value))));
				path += "?{0}".F(queryString);
			}
			return path;
		}

		private Dictionary<string, string> GetSearchParameters<T>(SearchDescriptor<T> descriptor) where T : class
		{
			var dict = new Dictionary<string, string>();
			if (!descriptor._Routing.IsNullOrEmpty())
				dict.Add("routing", descriptor._Routing);
			if (!descriptor._Scroll.IsNullOrEmpty())
				dict.Add("scroll", descriptor._Scroll);
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


		public string CreateGetPath<T>(GetDescriptor<T> d) where T : class
		{
			var index = d._Index ?? this._indexNameResolver.GetIndexForType<T>();
			var type = d._Type ?? this._typeNameResolver.GetTypeNameFor<T>();
			var id = d._Id;
			id.ThrowIfNullOrEmpty("id");

			var path = string.Format("/{0}/{1}/{2}", index, type, id);
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
		private string ToQueryString(IDictionary<string, string> urlParams)
		{
			if (urlParams == null || !urlParams.Any())
				return null;
			var queryString = string.Join("&", urlParams.Select(kv => "{0}={1}".F(kv.Key, Uri.EscapeDataString(kv.Value))));
			return "?{0}".F(queryString);
	
		}
	}
}
