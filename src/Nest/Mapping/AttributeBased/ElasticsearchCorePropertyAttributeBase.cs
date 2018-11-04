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

		[Obsolete("Please use overload taking FieldType")]
		protected ElasticsearchCorePropertyAttributeBase(string typeName) : base(typeName) { }

		[Obsolete("Please use overload taking FieldType")]
		protected ElasticsearchCorePropertyAttributeBase(Type type) : base(type) { }

		public string Similarity
		{
			set => Self.Similarity = value;
			get => Self.Similarity?.Match(f => f.GetStringValue(), str => str);
		}

		public bool Store
		{
			get => Self.Store.GetValueOrDefault();
			set => Self.Store = value;
		}

		Fields ICoreProperty.CopyTo { get; set; }

		IProperties ICoreProperty.Fields { get; set; }

		private ICoreProperty Self => this;

		Union<SimilarityOption, string> ICoreProperty.Similarity { get; set; }

		bool? ICoreProperty.Store { get; set; }

		public static new ElasticsearchCorePropertyAttributeBase From(MemberInfo memberInfo) =>
			memberInfo.GetCustomAttribute<ElasticsearchCorePropertyAttributeBase>(true);
	}
}
