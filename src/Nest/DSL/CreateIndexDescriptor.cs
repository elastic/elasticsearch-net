using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ICreateIndexRequest : IIndexPath<CreateIndexRequestParameters>
	{
		IndexSettings IndexSettings { get; set; }
	}

	internal static class CreateIndexPathInfo
	{
		public static void Update(ElasticsearchPathInfo<CreateIndexRequestParameters> pathInfo, ICreateIndexRequest request)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.POST;
		}
	}
	
	public partial class CreateIndexRequest : IndexPathBase<CreateIndexRequestParameters>, ICreateIndexRequest
	{
		public CreateIndexRequest(IndexNameMarker index) : base(index) { }

		public IndexSettings IndexSettings { get; set; }
		
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<CreateIndexRequestParameters> pathInfo)
		{
			CreateIndexPathInfo.Update(pathInfo, this);
		}

	}

	[DescriptorFor("IndicesCreate")]
	public partial class CreateIndexDescriptor : IndexPathDescriptorBase<CreateIndexDescriptor, CreateIndexRequestParameters>, ICreateIndexRequest
	{
		private readonly IConnectionSettingsValues _connectionSettings;
		private IndexSettings _indexSettings = new IndexSettings();

		IndexSettings ICreateIndexRequest.IndexSettings
		{
			get { return _indexSettings; }
			set { _indexSettings = value; }
		}

		public CreateIndexDescriptor(IConnectionSettingsValues connectionSettings)
		{
			this._connectionSettings = connectionSettings;
		}

		/// <summary>
		/// Initialize the descriptor using the values from for instance a previous Get Index Settings call.
		/// </summary>
		public CreateIndexDescriptor InitializeUsing(IndexSettings indexSettings)
		{
			this._indexSettings = indexSettings;
			return this;
		}

		/// <summary>
		/// Set the number of shards (if possible) for the new index.
		/// </summary>
		/// <param name="shards"></param>
		/// <returns></returns>
		public CreateIndexDescriptor NumberOfShards(int shards)
		{
			this._indexSettings.NumberOfShards = shards;
			return this;
		}

		/// <summary>
		/// Set the number of replicas (if possible) for the new index.
		/// </summary>
		/// <param name="replicas"></param>
		/// <returns></returns>
		public CreateIndexDescriptor NumberOfReplicas(int replicas)
		{
			this._indexSettings.NumberOfReplicas = replicas;
			return this;
		}

		/// <summary>
		/// Set/Update settings, the index.* prefix is not needed for the keys.
		/// </summary>
		public CreateIndexDescriptor Settings(Action<FluentDictionary<string, object>> settingsSelector)
		{
			settingsSelector.ThrowIfNull("settingsSelector");
			var dict = new FluentDictionary<string, object>();
			settingsSelector(dict);
			foreach (var kv in dict)
			{
				this._indexSettings.Settings.Add(kv.Key, kv.Value);
			}
			return this;
		}

		/// <summary>
		/// Remove an existing mapping by name
		/// </summary>
		public CreateIndexDescriptor RemoveMapping(string typeName)
		{
			TypeNameMarker marker = typeName;
			return this.RemoveMapping(marker);
		}

		/// <summary>
		/// Remove an exisiting mapping by inferred type name
		/// </summary>
		public CreateIndexDescriptor RemoveMapping<T>() where T : class
		{
			TypeNameMarker marker = typeof(T);
			return this.RemoveMapping(marker);
		}

		/// <summary>
		/// Add an alias for this index upon index creation
		/// </summary>
		public CreateIndexDescriptor AddAlias(string aliasName, Func<CreateAliasDescriptor, CreateAliasDescriptor> addAliasSelector = null)
		{
			aliasName.ThrowIfNullOrEmpty("aliasName");
			addAliasSelector = addAliasSelector ?? (a => a);
			var alias = addAliasSelector(new CreateAliasDescriptor());

			if (this._indexSettings.Aliases == null)
				this._indexSettings.Aliases = new Dictionary<string, ICreateAliasOperation>();

			this._indexSettings.Aliases.Add(aliasName, alias);

			return this;
		}
			

		private CreateIndexDescriptor RemoveMapping(TypeNameMarker marker)
		{
			this._indexSettings.Mappings = this._indexSettings.Mappings.Where(m => m.Type != marker).ToList();
			return this;
		}

		/// <summary>
		/// Add a new mapping for T
		/// </summary>
		public CreateIndexDescriptor AddMapping<T>(Func<PutMappingDescriptor<T>, PutMappingDescriptor<T>> typeMappingDescriptor) where T : class
		{
			typeMappingDescriptor.ThrowIfNull("typeMappingDescriptor");
			var d = typeMappingDescriptor(new PutMappingDescriptor<T>(this._connectionSettings));
			IPutMappingRequest request = d;
			var typeMapping = request.Mapping;

			if (request.Type != null)
			{
				typeMapping.Name = request.Type.Name != null ? (PropertyNameMarker)request.Type.Name : request.Type.Type;
			}
			else
			{
				typeMapping.Name = typeof(T);
			}

			this._indexSettings.Mappings.Add(typeMapping);

			return this;
		}

		/// <summary>
		/// Add a new mapping using the first rootObjectMapping parameter as the base to construct the new mapping.
		/// Handy if you wish to reuse a mapping.
		/// </summary>
		public CreateIndexDescriptor AddMapping<T>(RootObjectMapping rootObjectMapping, Func<PutMappingDescriptor<T>, PutMappingDescriptor<T>> typeMappingDescriptor) where T : class
		{
			typeMappingDescriptor.ThrowIfNull("typeMappingDescriptor");

			var selectorIn = new PutMappingDescriptor<T>(this._connectionSettings);
			IPutMappingRequest selectorInRequest = selectorIn;
			selectorInRequest.Mapping = rootObjectMapping;

			var d = typeMappingDescriptor(selectorIn);
			IPutMappingRequest request = d;
			var typeMapping = request.Mapping;

			if (request.Type != null)
			{
				typeMapping.Name = request.Type.Name != null ? (PropertyNameMarker)request.Type.Name : request.Type.Type;
			}
			else
			{
				typeMapping.Name = typeof (T);
			}

			this._indexSettings.Mappings.Add(typeMapping);

			return this;
		}

		public CreateIndexDescriptor AddWarmer(Func<CreateWarmerDescriptor, CreateWarmerDescriptor> warmerSelector)
		{
			warmerSelector.ThrowIfNull("warmerSelector");
			var descriptor = warmerSelector(new CreateWarmerDescriptor());

			var mapping = new WarmerMapping { Name = descriptor._WarmerName, Types = descriptor._Types, Source = descriptor._SearchDescriptor };
			this._indexSettings.Warmers.Add(descriptor._WarmerName, mapping);

			return this;
		}

		public CreateIndexDescriptor DeleteWarmer(string warmerName)
		{
			this._indexSettings.Warmers.Remove(warmerName);
			return this;
		}

		/// <summary>
		/// Set up analysis tokenizers, filters, analyzers
		/// </summary>
		public CreateIndexDescriptor Analysis(Func<AnalysisDescriptor, AnalysisDescriptor> analysisSelector)
		{
			analysisSelector.ThrowIfNull("analysisSelector");
			var analysis = analysisSelector(new AnalysisDescriptor(this._indexSettings.Analysis));
			this._indexSettings.Analysis = analysis == null ? null : analysis._AnalysisSettings;
			return this;
		}
		
		public CreateIndexDescriptor Similarity(Func<SimilarityDescriptor, SimilarityDescriptor> similaritySelector)
		{
			similaritySelector.ThrowIfNull("similaritySelector");
			var similarity = similaritySelector(new SimilarityDescriptor(this._indexSettings.Similarity));
			this._indexSettings.Similarity = similarity == null ? null : similarity._SimilaritySettings;
			return this;
		}

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<CreateIndexRequestParameters> pathInfo)
		{
			CreateIndexPathInfo.Update(pathInfo, this);
		}

	}
}
