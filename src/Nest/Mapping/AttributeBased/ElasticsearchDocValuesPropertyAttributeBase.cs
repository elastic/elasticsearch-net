using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace Nest
{
	[AttributeUsage(AttributeTargets.Property)]
	[DataContract]
	public abstract class ElasticsearchDocValuesPropertyAttributeBase : ElasticsearchCorePropertyAttributeBase, IDocValuesProperty
	{
		protected ElasticsearchDocValuesPropertyAttributeBase(FieldType type) : base(type) { }

		public bool DocValues
		{
			get => Self.DocValues.GetValueOrDefault(true);
			set => Self.DocValues = value;
		}

		bool? IDocValuesProperty.DocValues { get; set; }
		private IDocValuesProperty Self => this;

		public static new ElasticsearchDocValuesPropertyAttributeBase From(MemberInfo memberInfo) =>
			memberInfo.GetCustomAttribute<ElasticsearchDocValuesPropertyAttributeBase>(true);
	}
}
