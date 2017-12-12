using System;
using System.Reflection;
using Newtonsoft.Json;

namespace Nest
{
	[AttributeUsage(AttributeTargets.Property)]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public abstract class ElasticsearchDocValuesPropertyAttributeBase : ElasticsearchCorePropertyAttributeBase, IDocValuesProperty
	{
		private IDocValuesProperty Self => this;

		bool? IDocValuesProperty.DocValues { get; set; }

		public bool DocValues
		{
			get => Self.DocValues.GetValueOrDefault(true);
			set => Self.DocValues = value;
		}

		protected ElasticsearchDocValuesPropertyAttributeBase(FieldType type) : base(type) { }

		public new static ElasticsearchDocValuesPropertyAttributeBase From(MemberInfo memberInfo)
		{
			return memberInfo.GetCustomAttribute<ElasticsearchDocValuesPropertyAttributeBase>(true);
		}
	}
}
