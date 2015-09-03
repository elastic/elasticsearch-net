using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IProperty : IFieldMapping
	{
		PropertyName Name { get; set; }

		[JsonProperty("type")]
		TypeName Type { get; set; }

		[JsonProperty("index_name")]
		string IndexName { get; set; }

		[JsonProperty("store")]
		bool? Store { get; set; }

		[JsonProperty("doc_values")]
		bool? DocValues { get; set; }

		[JsonProperty("fields", DefaultValueHandling = DefaultValueHandling.Ignore)]
		IProperties Fields { get; set; }

		[JsonProperty("similarity")]
		SimilarityOption? Similarity { get; set; }

		[JsonProperty("copy_to")]
		IEnumerable<FieldName> CopyTo { get; set; }
	}

	public abstract class Property : IProperty
	{
		public Property(TypeName typeName)
		{
			Type = typeName;
		}

		internal Property(TypeName typeName, ElasticsearchPropertyAttribute attribute)
			: this(typeName)
		{
			DocValues = attribute.DocValues;
			IndexName = attribute.IndexName;
			Similarity = attribute.Similarity;
			Store = attribute.Store;
		}

		public PropertyName Name { get; set; }
		public virtual TypeName Type { get; set; }
		public IEnumerable<FieldName> CopyTo { get; set; }
		public bool? DocValues { get; set; }
		public IProperties Fields { get; set; }
		public string IndexName { get; set; }
		public SimilarityOption? Similarity { get; set; }
		public bool? Store { get; set; }
	}
}
