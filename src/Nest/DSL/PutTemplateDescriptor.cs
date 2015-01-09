using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IPutTemplateRequest : INamePath<PutTemplateRequestParameters>
	{
		TemplateMapping TemplateMapping { get; set; }
	}

	internal static class PutTemplatePathInfo
	{
		public static void Update(ElasticsearchPathInfo<PutTemplateRequestParameters> pathInfo, IPutTemplateRequest request)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.PUT;
		}
	}
	
	public partial class PutTemplateRequest : NamePathBase<PutTemplateRequestParameters>, IPutTemplateRequest
	{
		public PutTemplateRequest(string name) : base(name)
		{
		}

		public TemplateMapping TemplateMapping { get; set; }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<PutTemplateRequestParameters> pathInfo)
		{
			PutTemplatePathInfo.Update(pathInfo, this);
		}

	}

	[DescriptorFor("IndicesPutTemplate")]
	public partial class PutTemplateDescriptor : NamePathDescriptor<PutTemplateDescriptor, PutTemplateRequestParameters>, IPutTemplateRequest
	{
		private IPutTemplateRequest Self { get { return this; } }

		private readonly IConnectionSettingsValues _connectionSettings;

		TemplateMapping IPutTemplateRequest.TemplateMapping { get; set; }

		public PutTemplateDescriptor(IConnectionSettingsValues connectionSettings)
		{
			_connectionSettings = connectionSettings;
			Self.TemplateMapping = new TemplateMapping();
			Self.TemplateMapping.Mappings = new Dictionary<string, RootObjectMapping>();
			Self.TemplateMapping.Warmers = new Dictionary<string, WarmerMapping>();
			Self.TemplateMapping.Settings = new FluentDictionary<string, object>();
		}


		/// <summary>
		/// Initialize the descriptor using the values from for instance a previous Get Template Mapping call.
		/// </summary>
		public PutTemplateDescriptor InitializeUsing(TemplateMapping templateMapping)
		{
			Self.TemplateMapping = templateMapping;
			return this;
		}

		public PutTemplateDescriptor Order(int order)
		{
			Self.TemplateMapping.Order = order;
			return this;
		}

		public PutTemplateDescriptor Template(string template)
		{
			template.ThrowIfNull("name");
			Self.TemplateMapping.Template = template;
			return this;
		}

		public PutTemplateDescriptor Settings(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> settingsSelector)
		{
			settingsSelector.ThrowIfNull("settingsDescriptor");
			Self.TemplateMapping.Settings = settingsSelector(Self.TemplateMapping.Settings ?? new FluentDictionary<string, object>());
			return this;
		}

		public PutTemplateDescriptor AddMapping<T>(Func<PutMappingDescriptor<T>, PutMappingDescriptor<T>> mappingSelector)
			where T : class
		{
			mappingSelector.ThrowIfNull("mappingSelector");
			var putMappingDescriptor = mappingSelector(new PutMappingDescriptor<T>(this._connectionSettings));
			putMappingDescriptor.ThrowIfNull("rootObjectMappingDescriptor");

			var inferrer = new ElasticInferrer(this._connectionSettings);
			IPutMappingRequest request = putMappingDescriptor;
			var typeName = inferrer.TypeName(request.Type ?? typeof(T));
			if (typeName == null)
				return this;
			Self.TemplateMapping.Mappings[typeName] = request.Mapping;
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

			Self.TemplateMapping.Warmers[warmerDescriptor._WarmerName] = warmer;
			return this;

		}
		
		public PutTemplateDescriptor AddAlias(string aliasName, Func<CreateAliasDescriptor, CreateAliasDescriptor> addAliasDescriptor = null)
		{
			aliasName.ThrowIfNull("aliasName");
			addAliasDescriptor = addAliasDescriptor ?? (a=>a);
			var alias = addAliasDescriptor(new CreateAliasDescriptor());
			if (Self.TemplateMapping.Aliases == null)
				Self.TemplateMapping.Aliases = new Dictionary<string, ICreateAliasOperation>();

			Self.TemplateMapping.Aliases[aliasName] = alias;
			return this;

		}

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<PutTemplateRequestParameters> pathInfo)
		{
			PutTemplatePathInfo.Update(pathInfo, this);
		}

	}
}
