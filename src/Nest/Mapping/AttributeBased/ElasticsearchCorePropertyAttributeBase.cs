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

		private ICoreProperty Self => this;

		bool? ICoreProperty.Store { get; set; }

		IProperties ICoreProperty.Fields { get; set; }

		Fields ICoreProperty.CopyTo { get; set; }

		Union<SimilarityOption, string> ICoreProperty.Similarity { get; set; }

		public string Similarity {
			set { Self.Similarity = value; }
			get
			{
				return Self.Similarity?.Match(f => f.GetStringValue(), str => str);
			}
		}
		public bool Store { get { return Self.Store.GetValueOrDefault(); } set { Self.Store = value; } }

		public new static ElasticsearchCorePropertyAttributeBase From(MemberInfo memberInfo)
		{
			return memberInfo.GetCustomAttribute<ElasticsearchCorePropertyAttributeBase>(true);
		}
	}
}
