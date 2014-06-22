using Elasticsearch.Net;
using Elasticsearch.Net.Connection;
using Nest.Domain;
using Nest.Resolvers;
using Newtonsoft.Json;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Nest
{
	[DescriptorFor("IndicesCreate")]
	public partial class CreateIndexDescriptor : IndexPathDescriptorBase<CreateIndexDescriptor, CreateIndexRequestParameters>,
		IPathInfo<CreateIndexRequestParameters>
	{
		internal IndexSettings _IndexSettings = new IndexSettings();
		private IConnectionSettingsValues _connectionSettings;


		public CreateIndexDescriptor(IConnectionSettingsValues connectionSettings)
		{
			this._connectionSettings = connectionSettings;
		}

		/// <summary>
		/// Initialize the descriptor using the values from for instance a previous Get Index Settings call.
		/// </summary>
		public CreateIndexDescriptor InitializeUsing(IndexSettings indexSettings)
		{
			this._IndexSettings = indexSettings;
			return this;
		}

		/// <summary>
		/// Set the number of shards (if possible) for the new index.
		/// </summary>
		/// <param name="shards"></param>
		/// <returns></returns>
		public CreateIndexDescriptor NumberOfShards(int shards)
		{
			this._IndexSettings.NumberOfShards = shards;
			return this;
		}

		/// <summary>
		/// Set the number of replicas (if possible) for the new index.
		/// </summary>
		/// <param name="replicas"></param>
		/// <returns></returns>
		public CreateIndexDescriptor NumberOfReplicas(int replicas)
		{
			this._IndexSettings.NumberOfReplicas = replicas;
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
				this._IndexSettings.Settings.Add(kv.Key, kv.Value);
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

			if (this._IndexSettings.Aliases == null)
				this._IndexSettings.Aliases = new Dictionary<string, CreateAliasDescriptor>();

			this._IndexSettings.Aliases.Add(aliasName, alias);

			return this;
		}
			

		private CreateIndexDescriptor RemoveMapping(TypeNameMarker marker)
		{
			this._IndexSettings.Mappings = this._IndexSettings.Mappings.Where(m => m.Type != marker).ToList();
			return this;
		}

		/// <summary>
		/// Add a new mapping for T
		/// </summary>
		public CreateIndexDescriptor AddMapping<T>(Func<PutMappingDescriptor<T>, PutMappingDescriptor<T>> typeMappingDescriptor) where T : class
		{
			typeMappingDescriptor.ThrowIfNull("typeMappingDescriptor");
			var d = typeMappingDescriptor(new PutMappingDescriptor<T>(this._connectionSettings));
			var typeMapping = d._Mapping;

			if (d._Type != null)
			{
				typeMapping.Name = d._Type.Name != null ? (PropertyNameMarker)d._Type.Name : d._Type.Type;
			}
			else
			{
				typeMapping.Name = typeof(T);
			}

			this._IndexSettings.Mappings.Add(typeMapping);

			return this;
		}

		/// <summary>
		/// Add a new mapping using the first rootObjectMapping parameter as the base to construct the new mapping.
		/// Handy if you wish to reuse a mapping.
		/// </summary>
		public CreateIndexDescriptor AddMapping<T>(RootObjectMapping rootObjectMapping, Func<PutMappingDescriptor<T>, PutMappingDescriptor<T>> typeMappingDescriptor) where T : class
		{
			typeMappingDescriptor.ThrowIfNull("typeMappingDescriptor");
			var d = typeMappingDescriptor(new PutMappingDescriptor<T>(this._connectionSettings) { _Mapping = rootObjectMapping,});
			var typeMapping = d._Mapping;

			if (d._Type != null)
			{
				typeMapping.Name = d._Type.Name != null ? (PropertyNameMarker)d._Type.Name : d._Type.Type;
			}
			else
			{
				typeMapping.Name = typeof (T);
			}

			this._IndexSettings.Mappings.Add(typeMapping);

			return this;
		}

		public CreateIndexDescriptor AddWarmer(Func<CreateWarmerDescriptor, CreateWarmerDescriptor> warmerSelector)
		{
			warmerSelector.ThrowIfNull("warmerSelector");
			var descriptor = warmerSelector(new CreateWarmerDescriptor());

			var mapping = new WarmerMapping { Name = descriptor._WarmerName, Types = descriptor._Types, Source = descriptor._SearchDescriptor };
			this._IndexSettings.Warmers.Add(descriptor._WarmerName, mapping);

			return this;
		}

		public CreateIndexDescriptor DeleteWarmer(string warmerName)
		{
			this._IndexSettings.Warmers.Remove(warmerName);
			return this;
		}

		/// <summary>
		/// Set up analysis tokenizers, filters, analyzers
		/// </summary>
		public CreateIndexDescriptor Analysis(Func<AnalysisDescriptor, AnalysisDescriptor> analysisSelector)
		{
			analysisSelector.ThrowIfNull("analysisSelector");
			var analysis = analysisSelector(new AnalysisDescriptor(this._IndexSettings.Analysis));
			this._IndexSettings.Analysis = analysis == null ? null : analysis._AnalysisSettings;
			return this;
		}


		ElasticsearchPathInfo<CreateIndexRequestParameters> IPathInfo<CreateIndexRequestParameters>.ToPathInfo(IConnectionSettingsValues settings)
		{
			var pathInfo = base.ToPathInfo(settings, this._QueryString);
			pathInfo.HttpMethod = PathInfoHttpMethod.POST;
			return pathInfo;
		}

	}
}
