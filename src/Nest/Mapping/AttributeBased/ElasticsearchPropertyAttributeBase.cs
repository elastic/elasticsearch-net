using System;
using System.Collections.Generic;
using System.Reflection;
using Elasticsearch.Net;
using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[AttributeUsage(AttributeTargets.Property)]
	[DataContract]
	public abstract class ElasticsearchPropertyAttributeBase : Attribute, IProperty, IPropertyMapping, IJsonProperty
	{
		protected ElasticsearchPropertyAttributeBase(FieldType type) => Self.Type = type.GetStringValue();

		public bool Ignore { get; set; }

		public string Name { get; set; }

		public int Order { get; } = -2;

		IDictionary<string, object> IProperty.LocalMetadata { get; set; }

		PropertyName IProperty.Name { get; set; }
		private IProperty Self => this;
		string IProperty.Type { get; set; }

		public static ElasticsearchPropertyAttributeBase From(MemberInfo memberInfo) =>
			memberInfo.GetCustomAttribute<ElasticsearchPropertyAttributeBase>(true);
	}
}
