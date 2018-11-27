using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Nest.JsonNetSerializer.Converters;
using System.Runtime.Serialization;
using Newtonsoft.Json.Serialization;

namespace Nest.JsonNetSerializer
{
	public abstract partial class ConnectionSettingsAwareSerializerBase
	{
		protected ConnectionSettingsAwareSerializerBase(IElasticsearchSerializer builtinSerializer, IConnectionSettingsValues connectionSettings)
			: this(builtinSerializer, connectionSettings, null, null, null) { }

		internal ConnectionSettingsAwareSerializerBase(
			IElasticsearchSerializer builtinSerializer,
			IConnectionSettingsValues connectionSettings,
			Func<JsonSerializerSettings> jsonSerializerSettingsFactory,
			Action<ConnectionSettingsAwareContractResolver> modifyContractResolver,
			IEnumerable<JsonConverter> contractJsonConverters
		)
		{
			JsonSerializerSettingsFactory = jsonSerializerSettingsFactory;
			ModifyContractResolverCallback = modifyContractResolver;
			ContractJsonConverters = contractJsonConverters ?? Enumerable.Empty<JsonConverter>();

			ConnectionSettings = connectionSettings;
			BuiltinSerializer = builtinSerializer;
			Converters = new List<JsonConverter>
			{
				new HandleNestTypesOnSourceJsonConverter(BuiltinSerializer),
				new TimeSpanToStringConverter()
			};
			_serializer = CreateSerializer(SerializationFormatting.Indented);
			_collapsedSerializer = CreateSerializer(SerializationFormatting.None);
		}

		protected IElasticsearchSerializer BuiltinSerializer { get; }

		protected IConnectionSettingsValues ConnectionSettings { get; }
		protected IEnumerable<JsonConverter> ContractJsonConverters { get; }
		protected Func<JsonSerializerSettings> JsonSerializerSettingsFactory { get; }
		protected Action<ConnectionSettingsAwareContractResolver> ModifyContractResolverCallback { get; }

		private List<JsonConverter> Converters { get; }


		private JsonSerializer CreateSerializer(SerializationFormatting formatting)
		{
			var s = CreateJsonSerializerSettings() ?? new JsonSerializerSettings();
			;
			var converters = CreateJsonConverters() ?? Enumerable.Empty<JsonConverter>();
			var contract = CreateContractResolver();
			s.Formatting = formatting == SerializationFormatting.Indented ? Formatting.Indented : Formatting.None;
			s.ContractResolver = contract;
			foreach (var converter in converters.Concat(Converters))
				s.Converters.Add(converter);

			return JsonSerializer.Create(s);
		}

		private IContractResolver CreateContractResolver()
		{
			var contract = new ConnectionSettingsAwareContractResolver(ConnectionSettings);
			ModifyContractResolver(contract);
			return contract;
		}

		protected virtual JsonSerializerSettings CreateJsonSerializerSettings() => JsonSerializerSettingsFactory?.Invoke();

		protected virtual IEnumerable<JsonConverter> CreateJsonConverters() => ContractJsonConverters;

		protected virtual void ModifyContractResolver(ConnectionSettingsAwareContractResolver resolver) =>
			ModifyContractResolverCallback?.Invoke(resolver);
	}
}
