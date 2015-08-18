using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IElasticsearchProperty : IFieldMapping
	{
		FieldName Name { get; set; }
		TypeName Type { get; set; }

		[JsonProperty("index_name")]
		string IndexName { get; set; }

		[JsonProperty("store")]
		bool? Store { get; set; }

		[JsonProperty("doc_values")]
		bool? DocValues { get; set; }

		[JsonProperty("fields", DefaultValueHandling = DefaultValueHandling.Ignore), JsonConverter(typeof(PropertyConverter))]
		IDictionary<FieldName, IElasticsearchProperty> Fields { get; set; }

		[JsonProperty("similarity")]
		SimilarityOption? Similarity { get; set; }

		[JsonProperty("copy_to")]
		IEnumerable<FieldName> CopyTo { get; set; }
	}

	public abstract class ElasticsearchProperty : IElasticsearchProperty
	{
		public ElasticsearchProperty(TypeName typeName)
		{
			Type = typeName;
		}

		internal ElasticsearchProperty(TypeName typeName, ElasticPropertyAttribute attribute)
			: this(typeName)
		{
			DocValues = attribute.DocValues;
			IndexName = attribute.IndexName;
			Similarity = attribute.Similarity;
			Store = attribute.Store;
		}

		public FieldName Name { get; set; }
		public virtual TypeName Type { get; set; }
		public IEnumerable<FieldName> CopyTo { get; set; }
		public bool? DocValues { get; set; }
		public IDictionary<FieldName, IElasticsearchProperty> Fields { get; set; }
		public string IndexName { get; set; }
		public SimilarityOption? Similarity { get; set; }
		public bool? Store { get; set; }
	}
}
