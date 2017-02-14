using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[AttributeUsage(AttributeTargets.Property)]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public abstract class ElasticsearchPropertyAttributeBase : Attribute, IProperty, IPropertyMapping
	{
		private IProperty Self => this;

		PropertyName IProperty.Name { get; set; }
		TypeName IProperty.Type { get; set; }
		IDictionary<string, object> IProperty.LocalMetadata { get; set; }

		public string Name { get; set; }
		public bool Ignore { get; set; }

		protected ElasticsearchPropertyAttributeBase(FieldType type)
		{
			Self.Type = type.GetStringValue();
		}

		[Obsolete("Please use overload taking FieldType")]
		protected ElasticsearchPropertyAttributeBase(string typeName)
		{
			Self.Type = typeName;
		}

		[Obsolete("Please use overload taking FieldType")]
		protected ElasticsearchPropertyAttributeBase(Type type)
		{
			Self.Type = type;
		}

		public static ElasticsearchPropertyAttributeBase From(MemberInfo memberInfo)
		{
			return memberInfo.GetCustomAttribute<ElasticsearchPropertyAttributeBase>(true);
		}
	}
}
