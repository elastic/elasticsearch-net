using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IPutIndexTemplateRequest : ITemplateMapping
	{
	}

	public partial class PutIndexTemplateRequest 
	{
		public string Template { get; set; }

		public int? Order { get; set; }

		public IIndexSettings Settings { get; set; }

		public IMappings Mappings { get; set; }

		public IWarmers Warmers { get; set; }

		public IAliases Aliases { get; set; }
	}

	[DescriptorFor("IndicesPutTemplate")]
	public partial class PutIndexTemplateDescriptor 
	{
		string ITemplateMapping.Template { get; set; }

		int? ITemplateMapping.Order { get; set; }

		IIndexSettings ITemplateMapping.Settings { get; set; }

		IMappings ITemplateMapping.Mappings { get; set; }

		IWarmers ITemplateMapping.Warmers { get; set; }

		IAliases ITemplateMapping.Aliases { get; set; }

		public PutIndexTemplateDescriptor Order(int order) => Assign(a => a.Order = order);

		public PutIndexTemplateDescriptor Template(string template)=> Assign(a => a.Template = template);

		public PutIndexTemplateDescriptor Settings(Func<IndexSettingsDescriptor, IIndexSettings> settingsSelector) =>
			Assign(a => a.Settings = settingsSelector?.Invoke(new IndexSettingsDescriptor()));

		public PutIndexTemplateDescriptor Mappings(Func<MappingsDescriptor, IMappings> mappingSelector) =>
			Assign(a => a.Mappings = mappingSelector?.Invoke(new MappingsDescriptor()));

		public PutIndexTemplateDescriptor Warmers(Func<WarmersDescriptor, IWarmers> warmerSelector) =>
			Assign(a => a.Warmers = warmerSelector?.Invoke(new WarmersDescriptor()));

		public PutIndexTemplateDescriptor Aliases(Func<AliasesDescriptor, IAliases> aliasDescriptor) =>
			Assign(a => a.Aliases = aliasDescriptor?.Invoke(new AliasesDescriptor()));
	}
}
