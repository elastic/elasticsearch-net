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
		public IConnectionSettings ConnectionSettings { get; private set; }
		public ElasticResolver(IConnectionSettings connectionSettings) : base(true)
		{
			this.ConnectionSettings = connectionSettings;
		}

		protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
		{
			var property = base.CreateProperty(member, memberSerialization);

			var attributes = member.GetCustomAttributes(typeof(IElasticPropertyAttribute), false);
			if (attributes == null || !attributes.Any())
				return property;

			var att = attributes.First() as IElasticPropertyAttribute;
			if (!att.Name.IsNullOrEmpty())
				property.PropertyName = att.Name;
			return property;
		}
		protected override string ResolvePropertyName(string propertyName)
		{
			return base.ResolvePropertyName(propertyName);
		}
		public string Resolve(string name)
		{
			return this.ResolvePropertyName(name);
		}
	}
	public class ElasticCamelCaseResolver : ElasticResolver
	{
		public ElasticCamelCaseResolver(IConnectionSettings connectionSettings) : base(connectionSettings) { }

		protected override string ResolvePropertyName(string propertyName)
		{
			return propertyName.ToCamelCase();
		}
	}
}
