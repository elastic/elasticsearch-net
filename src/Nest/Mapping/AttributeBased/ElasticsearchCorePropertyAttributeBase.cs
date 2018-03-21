using System;
using System.Reflection;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[AttributeUsage(AttributeTargets.Property)]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public abstract class ElasticsearchCorePropertyAttributeBase : ElasticsearchPropertyAttributeBase, ICoreProperty
	{
		protected ElasticsearchCorePropertyAttributeBase(FieldType type) : base(type) { }

		private ICoreProperty Self => this;

		bool? ICoreProperty.Store { get; set; }

		IProperties ICoreProperty.Fields { get; set; }

		Fields ICoreProperty.CopyTo { get; set; }

		Union<SimilarityOption, string> ICoreProperty.Similarity { get; set; }

		/// <inheritdoc cref="ICoreProperty" />
		public string Similarity
		{
			set => Self.Similarity = value;
			get => Self.Similarity?.Match(f => f.GetStringValue(), str => str);
		}

		/// <inheritdoc cref="ICoreProperty" />
		public bool Store
		{
			get => Self.Store.GetValueOrDefault();
			set => Self.Store = value;
		}

		public new static ElasticsearchCorePropertyAttributeBase From(MemberInfo memberInfo) =>
			memberInfo.GetCustomAttribute<ElasticsearchCorePropertyAttributeBase>(true);
	}
}
