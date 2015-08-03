using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	public interface IElasticType : IFieldMapping
	{
		FieldName Name { get; set; }
		TypeName Type { get; set; }

		[JsonProperty("index_name")]
		string IndexName { get; set; }

		[JsonProperty("store")]
		bool Store { get; set; }

		[JsonProperty("doc_values")]
		bool DocValues { get; set; }

		[JsonProperty("fields", DefaultValueHandling = DefaultValueHandling.Ignore), JsonConverter(typeof(ElasticTypeConverter))]
		IDictionary<FieldName, IElasticType> Fields { get; set; }

		[JsonProperty("similarity")]
		SimilarityOption Similarity { get; set; }

		[JsonProperty("copy_to")]
		IEnumerable<FieldName> CopyTo { get; set; }

		[JsonProperty("fielddata")]
		IFielddata Fielddata { get; set; }
	}

	public abstract class ElasticType : IElasticType
	{
		public ElasticType(TypeName typeName)
		{
			this.Type = typeName;
		}

		public FieldName Name { get; set; }
		public virtual TypeName Type { get; set; }
		public IEnumerable<FieldName> CopyTo { get; set; }
		public bool DocValues { get; set; }
		public IFielddata Fielddata { get; set; }
		public IDictionary<FieldName, IElasticType> Fields { get; set; }
		public string IndexName { get; set; }
		public SimilarityOption Similarity { get; set; }
		public bool Store { get; set; }
	}
}
