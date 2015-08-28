using System;
using System.Collections.Generic;
using System.Linq;
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
			pathInfo.HttpMethod = HttpMethod.PUT;
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
		private IPutTemplateRequest Self => this;

		private readonly IConnectionSettingsValues _connectionSettings;

		TemplateMapping IPutTemplateRequest.TemplateMapping { get; set; }

		public PutTemplateDescriptor(IConnectionSettingsValues connectionSettings)
		{
			_connectionSettings = connectionSettings;
			Self.TemplateMapping = new TemplateMapping();
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
			throw new NotImplementedException();
		}

		public PutTemplateDescriptor AddMapping<T>(Func<PutMappingDescriptor<T>, PutMappingDescriptor<T>> mappingSelector)
			where T : class
		{
			throw new NotImplementedException();
		}

		public PutTemplateDescriptor AddWarmer<T>(Func<CreateWarmerDescriptor, CreateWarmerDescriptor> warmerSelector)
			where T : class
		{
			throw new NotImplementedException();
			//warmerSelector.ThrowIfNull("warmerSelector");
			//var warmerDescriptor = warmerSelector(new CreateWarmerDescriptor());
			//warmerDescriptor.ThrowIfNull("warmerDescriptor");
			//warmerDescriptor._WarmerName.ThrowIfNull("warmer has no name");

			//var warmer = new WarmerMapping
			//{
			//	Name = warmerDescriptor._WarmerName, 
			//	Types = warmerDescriptor._Types, 
			//	Source = warmerDescriptor._SearchDescriptor
			//};

			//Self.TemplateMapping.Warmers[warmerDescriptor._WarmerName] = warmer;
			//return this;

		}
		
		public PutTemplateDescriptor AddAlias(string aliasName, Func<BulkAliasDescriptor, BulkAliasDescriptor> addAliasDescriptor = null)
		{
			throw new NotImplementedException();
			//aliasName.ThrowIfNull("aliasName");
			//addAliasDescriptor = addAliasDescriptor ?? (a=>a);
			//var alias = addAliasDescriptor(new BulkAliasDescriptor());
			//if (Self.TemplateMapping.Aliases == null)
			//	Self.TemplateMapping.Aliases = new Dictionary<string, IAlias>();

			//Self.TemplateMapping.Aliases[aliasName] = alias;
			//return this;

		}

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<PutTemplateRequestParameters> pathInfo)
		{
			PutTemplatePathInfo.Update(pathInfo, this);
		}

	}
}
