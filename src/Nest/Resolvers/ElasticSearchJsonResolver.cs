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
		/// <summary>
		/// ConnectionSettings can be requested by JsonConverter's.
		/// </summary>
		public IConnectionSettings ConnectionSettings { get; private set; }

		/// <summary>
		/// The ConcreteTypeConverter piggy backs on the resolver to inject meta data.
		/// This is a bit of a hack but a massive performance gain.
		/// </summary>
		internal ConcreteTypeConverter ConcreteTypeConverter { get; set; }

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
