using System;
using System.Reflection;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(PropertyJsonConverter))]
	public interface IProperty : IFieldMapping
	{
		PropertyName Name { get; set; }

		[JsonProperty("type")]
		TypeName Type { get; set; }

		[JsonProperty("index_name")]
		[Obsolete("Removed in 2.0.0. Use CopyTo instead.")]
		string IndexName { get; set; }

		[JsonProperty("store")]
		bool? Store { get; set; }

		[JsonProperty("doc_values")]
		bool? DocValues { get; set; }

		[JsonProperty("fields", DefaultValueHandling = DefaultValueHandling.Ignore)]
		IProperties Fields { get; set; }

		SimilarityOption? Similarity { get; set; }

		[JsonProperty("similarity")]
		[Obsolete("This is a temporary binary backwards compatible fix to allow named similarities in 2.0.0. Removed in 5.0.0")]
		string CustomSimilarity { get; set; }

		[JsonProperty("copy_to")]
		Fields CopyTo { get; set; }

	}

	public interface IPropertyWithClrOrigin
	{
		PropertyInfo ClrOrigin { get; set; }
	}

	public abstract class PropertyBase : IProperty, IPropertyWithClrOrigin
	{
		protected PropertyBase(TypeName typeName)
		{
			Type = typeName;
		}

		public PropertyName Name { get; set; }
		public virtual TypeName Type { get; set; }
		public Fields CopyTo { get; set; }
		public bool? DocValues { get; set; }
		public IProperties Fields { get; set; }
		public string IndexName { get; set; }
#pragma warning disable 618
		public SimilarityOption? Similarity { get { return this.CustomSimilarity?.ToEnum<SimilarityOption>(); } set { this.CustomSimilarity = value.GetStringValue(); } }
#pragma warning restore 618
		[Obsolete("This is a temporary binary backwards compatible fix to allow named similarities in 2.0.0. Removed in 5.0.0")]
		public string CustomSimilarity { get; set; }
		public bool? Store { get; set; }
		PropertyInfo IPropertyWithClrOrigin.ClrOrigin { get; set; }
	}
}
