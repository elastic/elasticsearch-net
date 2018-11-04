using System;
using System.Reflection;
using Newtonsoft.Json;

namespace Nest
{
	[AttributeUsage(AttributeTargets.Property)]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public abstract class ElasticsearchDocValuesPropertyAttributeBase : ElasticsearchCorePropertyAttributeBase, IDocValuesProperty
	{
		protected ElasticsearchDocValuesPropertyAttributeBase(FieldType type) : base(type) { }

		[Obsolete("Please use overload taking FieldType")]
		protected ElasticsearchDocValuesPropertyAttributeBase(string typeName) : base(typeName) { }

		[Obsolete("Please use overload taking FieldType")]
		protected ElasticsearchDocValuesPropertyAttributeBase(Type type) : base(type) { }

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
