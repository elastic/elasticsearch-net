// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class GetIndexTemplateV2Response : ResponseBase
	{
		[DataMember(Name = "index_templates")]
		public IReadOnlyCollection<IndexTemplateItem> IndexTemplates { get; set; }
	}

	[DataContract]
	public class IndexTemplateItem
	{
		[DataMember(Name = "name")]
		public string Name { get; set; }

		[DataMember(Name = "index_template")]
		public IndexTemplate IndexTemplate { get; set; }
	}

	[DataContract]
	public class IndexTemplate
	{
		[DataMember(Name = "index_patterns")]
		public IEnumerable<string> IndexPatterns { get; set; }

		[DataMember(Name = "composed_of")]
		public IEnumerable<string> ComposedOf { get; set; }

		[DataMember(Name = "template")]
		public ITemplate Template { get; set; }

		[DataMember(Name = "data_stream")]
		public DataStream DataStream { get; set; }

		[DataMember(Name = "priority")]
		public int? Priority { get; set; }

		[DataMember(Name = "version")]
		public long? Version { get; set; }

		[DataMember(Name = "_meta")]
		public IDictionary<string, object> Meta { get; set; }

		[DataMember(Name = "allow_auto_create")]
		public bool? AllowAutoCreate { get; set; }
	}
}
