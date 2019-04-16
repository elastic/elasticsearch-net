using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	[MapsApi("indices.put_template.json")]
	public partial interface IPutIndexTemplateRequest : ITemplateMapping { }

	public partial class PutIndexTemplateRequest
	{
		public IAliases Aliases { get; set; }
		public IReadOnlyCollection<string> IndexPatterns { get; set; }

		public ITypeMapping Mappings { get; set; }

		public int? Order { get; set; }

		public IIndexSettings Settings { get; set; }

		public int? Version { get; set; }
	}

	public partial class PutIndexTemplateDescriptor
	{
		IAliases ITemplateMapping.Aliases { get; set; }

		IReadOnlyCollection<string> ITemplateMapping.IndexPatterns { get; set; }

		ITypeMapping ITemplateMapping.Mappings { get; set; }
		int? ITemplateMapping.Order { get; set; }

		IIndexSettings ITemplateMapping.Settings { get; set; }

		int? ITemplateMapping.Version { get; set; }

		public PutIndexTemplateDescriptor Order(int? order) => Assign(a => a.Order = order);

		public PutIndexTemplateDescriptor Version(int? version) => Assign(a => a.Version = version);

		public PutIndexTemplateDescriptor IndexPatterns(params string[] patterns) => Assign(a => a.IndexPatterns = patterns);

		public PutIndexTemplateDescriptor IndexPatterns(IEnumerable<string> patterns) => Assign(a => a.IndexPatterns = patterns?.ToArray());

		public PutIndexTemplateDescriptor Settings(Func<IndexSettingsDescriptor, IPromise<IIndexSettings>> settingsSelector) =>
			Assign(a => a.Settings = settingsSelector?.Invoke(new IndexSettingsDescriptor())?.Value);

		public PutIndexTemplateDescriptor Map<T>(Func<TypeMappingDescriptor<T>, ITypeMapping> selector) where T : class =>
			Assign(a => a.Mappings = selector?.Invoke(new TypeMappingDescriptor<T>()));

		public PutIndexTemplateDescriptor Map(Func<TypeMappingDescriptor<object>, ITypeMapping> selector) =>
			Assign(a => a.Mappings = selector?.Invoke(new TypeMappingDescriptor<object>()));

		[Obsolete("Mappings is no longer a dictionary in 7.x, please use the simplified Map() method on this descriptor instead")]
		public PutIndexTemplateDescriptor Mappings(Func<MappingsDescriptor, ITypeMapping> mappingSelector) =>
			Assign(a => a.Mappings = mappingSelector?.Invoke(new MappingsDescriptor()));

		public PutIndexTemplateDescriptor Aliases(Func<AliasesDescriptor, IPromise<IAliases>> aliasDescriptor) =>
			Assign(a => a.Aliases = aliasDescriptor?.Invoke(new AliasesDescriptor())?.Value);
	}
}
