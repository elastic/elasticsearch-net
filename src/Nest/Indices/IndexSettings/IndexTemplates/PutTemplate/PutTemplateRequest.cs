using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IPutTemplateRequest 
	{
		TemplateMapping TemplateMapping { get; set; }
	}

	public partial class PutTemplateRequest 
	{
		//TODO Merge this state object onto this object
		public TemplateMapping TemplateMapping { get; set; }
	}

	[DescriptorFor("IndicesPutTemplate")]
	public partial class PutTemplateDescriptor 
	{
		private IPutTemplateRequest Self => this;

		private readonly IConnectionSettingsValues _connectionSettings;

		//TODO Merge this state object onto this descriptor
		TemplateMapping IPutTemplateRequest.TemplateMapping { get; set; }
		
		//TODO ConnectionSettings why?

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

		public PutTemplateDescriptor Settings(Func<IndexSettingsDescriptor, IIndexSettings> settingsSelector)
		{
			Self.TemplateMapping.Settings = settingsSelector?.Invoke(new IndexSettingsDescriptor());
			return this;
		}

		public PutTemplateDescriptor Mappings(Func<MappingsDescriptor, IMappings> mappingSelector)
		{
			Self.TemplateMapping.Mappings = mappingSelector?.Invoke(new MappingsDescriptor());
			return this;
		}

		public PutTemplateDescriptor Warmers(Func<WarmersDescriptor, IWarmers> warmerSelector)
		{
			Self.TemplateMapping.Warmers = warmerSelector?.Invoke(new WarmersDescriptor());
			return this;
		}

		public PutTemplateDescriptor Aliases(Func<AliasesDescriptor, IAliases> aliasDescriptor)
		{
			Self.TemplateMapping.Aliases = aliasDescriptor?.Invoke(new AliasesDescriptor());
			return this;
		}
	}
}
