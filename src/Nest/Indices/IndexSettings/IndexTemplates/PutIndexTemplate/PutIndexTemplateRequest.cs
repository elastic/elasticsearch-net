using System;

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

		public IAliases Aliases { get; set; }
	}

	[DescriptorFor("IndicesPutTemplate")]
	public partial class PutIndexTemplateDescriptor
	{
		string ITemplateMapping.Template { get; set; }

		int? ITemplateMapping.Order { get; set; }

		IIndexSettings ITemplateMapping.Settings { get; set; }

		IMappings ITemplateMapping.Mappings { get; set; }

		IAliases ITemplateMapping.Aliases { get; set; }

		public PutIndexTemplateDescriptor Order(int order) => Assign(a => a.Order = order);

		public PutIndexTemplateDescriptor Template(string template)=> Assign(a => a.Template = template);

		public PutIndexTemplateDescriptor Settings(Func<IndexSettingsDescriptor, IPromise<IIndexSettings>> settingsSelector) =>
			Assign(a => a.Settings = settingsSelector?.Invoke(new IndexSettingsDescriptor())?.Value);

		public PutIndexTemplateDescriptor Mappings(Func<MappingsDescriptor, IPromise<IMappings>> mappingSelector) =>
			Assign(a => a.Mappings = mappingSelector?.Invoke(new MappingsDescriptor())?.Value);

		public PutIndexTemplateDescriptor Aliases(Func<AliasesDescriptor, IPromise<IAliases>> aliasDescriptor) =>
			Assign(a => a.Aliases = aliasDescriptor?.Invoke(new AliasesDescriptor())?.Value);
	}
}
