using System;
using System.Collections.Generic;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

using Newtonsoft.Json.Linq;
using System.Linq;
using System.Reflection;
using System.Globalization;
using System.Collections;
using Nest.Resolvers.Converters;

namespace Nest.Resolvers
{
	public class ElasticContractResolver : DefaultContractResolver
	{
		/// <summary>
		/// ConnectionSettings can be requested by JsonConverter's.
		/// </summary>
		public IConnectionSettings ConnectionSettings { get; private set; }

		/// <summary>
		/// Signals to custom converter that it can get serialization state from one of the converters
		/// Ugly but massive performance gain
		/// </summary>
		internal JsonConverterPiggyBackState PiggyBackState { get; set; }

		public ElasticContractResolver(IConnectionSettings connectionSettings)
			: base(true)
		{
			this.ConnectionSettings = connectionSettings;
		}

		protected override JsonContract CreateContract(Type objectType)
		{
			JsonContract contract = base.CreateContract(objectType);

			// this will only be called once and then cached
			if (typeof(IDictionary).IsAssignableFrom(objectType))
				contract.Converter = new DictionaryKeysAreNotPropertyNamesJsonConverter();

			if (objectType == typeof(Facet))
				contract.Converter = new FacetConverter();
			
			if (objectType == typeof(DateTime) || objectType == typeof(DateTime?))
				contract.Converter = new IsoDateTimeConverter();

			if (typeof(IHit<object>).IsAssignableFrom(objectType))
				contract.Converter = new DefaultHitConverter();
			
			if (objectType == typeof(MultiGetResponse))
				contract.Converter = new MultiGetHitConverter();

			if (objectType == typeof(MultiSearchResponse))
				contract.Converter = new MultiSearchConverter();

			if (objectType == typeof(IDictionary<string, AnalyzerBase>))
				contract.Converter = new AnalyzerCollectionConverter();

			if (objectType == typeof(IDictionary<string, TokenFilterBase>))
				contract.Converter = new TokenFilterCollectionConverter();

			if (objectType == typeof(IDictionary<string, TokenizerBase>))
				contract.Converter = new TokenizerCollectionConverter();

			if (objectType == typeof(IDictionary<string, CharFilterBase>))
				contract.Converter = new CharFilterCollectionConverter();

			if (this.ConnectionSettings.ContractConverters.HasAny())
			{
				foreach (var c in this.ConnectionSettings.ContractConverters)
				{
					var converter = c(objectType);
					if (converter == null)
						continue;
					contract.Converter = converter;
					break;
				}
			}

			return contract;
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
