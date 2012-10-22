using System;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

using Newtonsoft.Json.Linq;
using System.Linq;
using System.Reflection;
using System.Globalization;

namespace Nest.Resolvers
{

	public class ElasticResolver : DefaultContractResolver
	{
    public ElasticResolver() : base(true) { }

		protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
		{
			var property = base.CreateProperty(member, memberSerialization);
			
			var attributes = member.GetCustomAttributes(typeof(ElasticPropertyAttribute), false);
			if (attributes == null || !attributes.Any())
				return property;

			var att = attributes.First() as ElasticPropertyAttribute;
			if (!att.Name.IsNullOrEmpty())
				property.PropertyName = att.Name;
			return property;
		}
		protected override string ResolvePropertyName(string propertyName)
		{
      return base.ResolvePropertyName(propertyName).ToCamelCase();
		}
    public string Resolve(string name)
    {
      return this.ResolvePropertyName(name).ToCamelCase();
    }
	}
  public class ElasticCamelCaseResolver : ElasticResolver
  {
    public ElasticCamelCaseResolver() : base() { }

    protected override string ResolvePropertyName(string propertyName)
    {
      return propertyName.ToCamelCase();
    }
  }
}
