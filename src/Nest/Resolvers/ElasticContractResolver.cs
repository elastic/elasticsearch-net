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
	public class ElasticContractResolver : DefaultContractResolver
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

		public ElasticContractResolver(IConnectionSettings connectionSettings) : base(true)
		{
			this.ConnectionSettings = connectionSettings;
		}

		protected override string ResolvePropertyName(string propertyName)
		{
      if (this.ConnectionSettings.DefaultPropertyNameInferrer != null)
        return this.ConnectionSettings.DefaultPropertyNameInferrer(propertyName);

			return propertyName.ToCamelCase();
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

			property.Ignored = att.OptOut;
			return property;
		}

	}
}
