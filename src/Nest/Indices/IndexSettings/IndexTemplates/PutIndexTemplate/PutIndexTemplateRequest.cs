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
		private IPutIndexTemplateRequest Self => this;

		//TODO Merge this state object onto this descriptor
		TemplateMapping IPutIndexTemplateRequest.TemplateMapping { get; set; }
		
		/// <summary>
		/// Initialize the descriptor using the values from for instance a previous Get Template Mapping call.
		/// </summary>
		public PutIndexTemplateDescriptor InitializeUsing(TemplateMapping templateMapping)
		{
			Self.TemplateMapping = templateMapping;
			return this;
		}

		public PutIndexTemplateDescriptor Order(int order)
		{
			Self.TemplateMapping.Order = order;
			return this;
		}

		public PutIndexTemplateDescriptor Template(string template)
		{
			template.ThrowIfNull("name");
			Self.TemplateMapping.Template = template;
			return this;
		}

		public PutIndexTemplateDescriptor Settings(Func<IndexSettingsDescriptor, IIndexSettings> settingsSelector)
		{
			Self.TemplateMapping.Settings = settingsSelector?.Invoke(new IndexSettingsDescriptor());
			return this;
		}

		public PutIndexTemplateDescriptor Mappings(Func<MappingsDescriptor, IMappings> mappingSelector)
		{
			Self.TemplateMapping.Mappings = mappingSelector?.Invoke(new MappingsDescriptor());
			return this;
		}

		public PutIndexTemplateDescriptor Warmers(Func<WarmersDescriptor, IWarmers> warmerSelector)
		{
			Self.TemplateMapping.Warmers = warmerSelector?.Invoke(new WarmersDescriptor());
			return this;
		}

		public PutIndexTemplateDescriptor Aliases(Func<AliasesDescriptor, IAliases> aliasDescriptor)
		{
			Self.TemplateMapping.Aliases = aliasDescriptor?.Invoke(new AliasesDescriptor());
			return this;
		}
	}
}
