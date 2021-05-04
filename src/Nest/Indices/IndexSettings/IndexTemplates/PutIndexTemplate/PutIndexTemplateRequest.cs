// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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

		public PutIndexTemplateDescriptor Order(int? order) => Assign(order, (a, v) => a.Order = v);

		public PutIndexTemplateDescriptor Version(int? version) => Assign(version, (a, v) => a.Version = v);

		public PutIndexTemplateDescriptor IndexPatterns(params string[] patterns) => Assign(patterns, (a, v) => a.IndexPatterns = v);

		public PutIndexTemplateDescriptor IndexPatterns(IEnumerable<string> patterns) => Assign(patterns?.ToArray(), (a, v) => a.IndexPatterns = v);

		public PutIndexTemplateDescriptor Settings(Func<IndexSettingsDescriptor, IPromise<IIndexSettings>> settingsSelector) =>
			Assign(settingsSelector, (a, v) => a.Settings = v?.Invoke(new IndexSettingsDescriptor())?.Value);

		public PutIndexTemplateDescriptor Map<T>(Func<TypeMappingDescriptor<T>, ITypeMapping> selector) where T : class =>
			Assign(selector, (a, v) => a.Mappings = v?.Invoke(new TypeMappingDescriptor<T>()));

		public PutIndexTemplateDescriptor Map(Func<TypeMappingDescriptor<object>, ITypeMapping> selector) =>
			Assign(selector, (a, v) => a.Mappings = v?.Invoke(new TypeMappingDescriptor<object>()));

		[Obsolete("Mappings is no longer a dictionary in 7.x, please use the simplified Map() method on this descriptor instead")]
		public PutIndexTemplateDescriptor Mappings(Func<MappingsDescriptor, ITypeMapping> mappingSelector) =>
			Assign(mappingSelector, (a, v) => a.Mappings = v?.Invoke(new MappingsDescriptor()));

		public PutIndexTemplateDescriptor Aliases(Func<AliasesDescriptor, IPromise<IAliases>> aliasDescriptor) =>
			Assign(aliasDescriptor, (a, v) => a.Aliases = v?.Invoke(new AliasesDescriptor())?.Value);
	}
}
