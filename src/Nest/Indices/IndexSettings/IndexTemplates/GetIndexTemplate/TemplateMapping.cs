// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public interface ITemplateMapping
	{
		[DataMember(Name ="aliases")]
		IAliases Aliases { get; set; }

		[DataMember(Name ="index_patterns")]
		IReadOnlyCollection<string> IndexPatterns { get; set; }

		[DataMember(Name = "mappings")]
		ITypeMapping Mappings { get; set; }

		[DataMember(Name ="order")]
		int? Order { get; set; }

		[DataMember(Name ="settings")]
		IIndexSettings Settings { get; set; }

		[DataMember(Name ="version")]
		int? Version { get; set; }
	}

	public class TemplateMapping : ITemplateMapping
	{
		public IAliases Aliases { get; set; }
		public IReadOnlyCollection<string> IndexPatterns { get; set; } = EmptyReadOnly<string>.Collection;

		public ITypeMapping Mappings { get; set; }

		public int? Order { get; set; }

		public IIndexSettings Settings { get; set; }

		public int? Version { get; set; }
	}
}
