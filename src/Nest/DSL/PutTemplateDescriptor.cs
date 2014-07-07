using System;
using System.Collections.Generic;
using Elasticsearch.Net;

namespace Nest
{
	[DescriptorFor("IndicesPutTemplate")]
	public partial class PutTemplateDescriptor : NamePathDescriptor<PutTemplateDescriptor, PutTemplateRequestParameters>
	{
		private readonly IConnectionSettingsValues _connectionSettings;

		internal TemplateMapping _TemplateMapping { get; set; }

		public PutTemplateDescriptor(IConnectionSettingsValues connectionSettings)
		{
			_connectionSettings = connectionSettings;
			this._TemplateMapping = new TemplateMapping();
		}


		/// <summary>
		/// Initialize the descriptor using the values from for instance a previous Get Template Mapping call.
		/// </summary>
		public PutTemplateDescriptor InitializeUsing(TemplateMapping templateMapping)
		{
			this._TemplateMapping = templateMapping;
			return this;
		}

		public PutTemplateDescriptor Order(int order)
		{
			this._TemplateMapping.Order = order;
			return this;
		}

		public PutTemplateDescriptor Template(string template)
		{
			template.ThrowIfNull("name");
			this._TemplateMapping.Template = template;
			return this;
		}

		public PutTemplateDescriptor Settings(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> settingsSelector)
		{
			settingsSelector.ThrowIfNull("settingsDescriptor");
			this._TemplateMapping.Settings = settingsSelector(this._TemplateMapping.Settings ?? new FluentDictionary<string, object>());
			return this;
		}

		public PutTemplateDescriptor AddMapping<T>(Func<PutMappingDescriptor<T>, PutMappingDescriptor<T>> mappingSelector)
			where T : class
		{
			mappingSelector.ThrowIfNull("mappingSelector");
			var putMappingDescriptor = mappingSelector(new PutMappingDescriptor<T>(this._connectionSettings));
			putMappingDescriptor.ThrowIfNull("rootObjectMappingDescriptor");

			var inferrer = new ElasticInferrer(this._connectionSettings);
			var typeName = inferrer.TypeName(putMappingDescriptor._Type ?? typeof(T));
			if (typeName == null)
				return this;
			this._TemplateMapping.Mappings[typeName] = putMappingDescriptor._Mapping;
			return this;
		}

		public PutTemplateDescriptor AddWarmer<T>(Func<CreateWarmerDescriptor, CreateWarmerDescriptor> warmerSelector)
			where T : class
		{
			warmerSelector.ThrowIfNull("warmerSelector");
			var warmerDescriptor = warmerSelector(new CreateWarmerDescriptor());
			warmerDescriptor.ThrowIfNull("warmerDescriptor");
			warmerDescriptor._WarmerName.ThrowIfNull("warmer has no name");

			var warmer = new WarmerMapping
			{
				Name = warmerDescriptor._WarmerName, 
				Types = warmerDescriptor._Types, 
				Source = warmerDescriptor._SearchDescriptor
			};

			this._TemplateMapping.Warmers[warmerDescriptor._WarmerName] = warmer;
			return this;

		}
		
		public PutTemplateDescriptor AddAlias(string aliasName, Func<CreateAliasDescriptor, CreateAliasDescriptor> addAliasDescriptor = null)
		{
			aliasName.ThrowIfNull("aliasName");
			addAliasDescriptor = addAliasDescriptor ?? (a=>a);
			var alias = addAliasDescriptor(new CreateAliasDescriptor());
			if (this._TemplateMapping.Aliases == null)
				this._TemplateMapping.Aliases = new Dictionary<string, CreateAliasDescriptor>();

			this._TemplateMapping.Aliases[aliasName] = alias;
			return this;

		}

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<PutTemplateRequestParameters> pathInfo)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.PUT;
			pathInfo.Name = this._Name;
		}

	}
}
