using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	public partial interface IPutIndexTemplateRequest : ITemplateMapping { }

	public partial class PutIndexTemplateRequest
	{
		public IAliases Aliases { get; set; }
		public IReadOnlyCollection<string> IndexPatterns { get; set; }

		public IMappings Mappings { get; set; }

		public int? Order { get; set; }

		public IIndexSettings Settings { get; set; }

		public int? Version { get; set; }
	}

	[DescriptorFor("IndicesPutTemplate")]
	public partial class PutIndexTemplateDescriptor
	{
		IAliases ITemplateMapping.Aliases { get; set; }

		IReadOnlyCollection<string> ITemplateMapping.IndexPatterns { get; set; }

		IMappings ITemplateMapping.Mappings { get; set; }
		int? ITemplateMapping.Order { get; set; }

		IIndexSettings ITemplateMapping.Settings { get; set; }

		int? ITemplateMapping.Version { get; set; }

		public PutIndexTemplateDescriptor Order(int? order) => Assign(order, (a, v) => a.Order = v);

		public PutIndexTemplateDescriptor Version(int? version) => Assign(version, (a, v) => a.Version = v);

		public PutIndexTemplateDescriptor IndexPatterns(params string[] patterns) => Assign(patterns, (a, v) => a.IndexPatterns = v);

		public PutIndexTemplateDescriptor IndexPatterns(IEnumerable<string> patterns) => Assign(patterns?.ToArray(), (a, v) => a.IndexPatterns = v);

		public PutIndexTemplateDescriptor Settings(Func<IndexSettingsDescriptor, IPromise<IIndexSettings>> settingsSelector) =>
			Assign(settingsSelector, (a, v) => a.Settings = v?.Invoke(new IndexSettingsDescriptor())?.Value);

		public PutIndexTemplateDescriptor Mappings(Func<MappingsDescriptor, IPromise<IMappings>> mappingSelector) =>
			Assign(mappingSelector, (a, v) => a.Mappings = v?.Invoke(new MappingsDescriptor())?.Value);

		public PutIndexTemplateDescriptor Aliases(Func<AliasesDescriptor, IPromise<IAliases>> aliasDescriptor) =>
			Assign(aliasDescriptor, (a, v) => a.Aliases = v?.Invoke(new AliasesDescriptor())?.Value);
	}
}
