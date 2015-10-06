using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IPutIndexTemplateRequest 
	{
		TemplateMapping TemplateMapping { get; set; }
	}

	public partial class PutIndexTemplateRequest 
	{
		//TODO Merge this state object onto this object
		public TemplateMapping TemplateMapping { get; set; }
	}

	[DescriptorFor("IndicesPutTemplate")]
	public partial class PutIndexTemplateDescriptor 
	{
		//TODO Merge this state object onto this descriptor
		TemplateMapping IPutIndexTemplateRequest.TemplateMapping { get; set; }

		/// <summary>
		/// Initialize the descriptor using the values from for instance a previous Get Template Mapping call.
		/// </summary>
		public PutIndexTemplateDescriptor InitializeUsing(TemplateMapping templateMapping) => Assign(a => a.TemplateMapping = templateMapping);

		public PutIndexTemplateDescriptor Order(int order) => Assign(a => a.TemplateMapping.Order = order);

		public PutIndexTemplateDescriptor Template(string template)=> Assign(a => a.TemplateMapping.Template = template);

		public PutIndexTemplateDescriptor Settings(Func<IndexSettingsDescriptor, IIndexSettings> settingsSelector) =>
			Assign(a => a.TemplateMapping.Settings = settingsSelector?.Invoke(new IndexSettingsDescriptor()));

		public PutIndexTemplateDescriptor Mappings(Func<MappingsDescriptor, IMappings> mappingSelector) =>
			Assign(a => a.TemplateMapping.Mappings = mappingSelector?.Invoke(new MappingsDescriptor()));

		public PutIndexTemplateDescriptor Warmers(Func<WarmersDescriptor, IWarmers> warmerSelector) =>
			Assign(a => a.TemplateMapping.Warmers = warmerSelector?.Invoke(new WarmersDescriptor()));

		public PutIndexTemplateDescriptor Aliases(Func<AliasesDescriptor, IAliases> aliasDescriptor) =>
			Assign(a => a.TemplateMapping.Aliases = aliasDescriptor?.Invoke(new AliasesDescriptor()));
	}
}
