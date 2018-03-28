using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Nest.JsonNetSerializer.Converters;

namespace Nest.JsonNetSerializer
{
	public abstract partial class ConnectionSettingsAwareSerializerBase
	{
		protected Func<JsonSerializerSettings> JsonSerializerSettingsFactory { get; }
		protected Action<ConnectionSettingsAwareContractResolver> ModifyContractResolverCallback { get; }
		protected IEnumerable<JsonConverter> ContractJsonConverters { get; }

		protected IConnectionSettingsValues ConnectionSettings { get; }

		protected IElasticsearchSerializer BuiltinSerializer { get; }

		private List<JsonConverter> Converters { get; }

		protected ConnectionSettingsAwareSerializerBase(IElasticsearchSerializer builtinSerializer, IConnectionSettingsValues connectionSettings)
			: this(builtinSerializer, connectionSettings, null, null, null) { }

		internal ConnectionSettingsAwareSerializerBase(
			IElasticsearchSerializer builtinSerializer,
			IConnectionSettingsValues connectionSettings,
			Func<JsonSerializerSettings> jsonSerializerSettingsFactory,
			Action<ConnectionSettingsAwareContractResolver> modifyContractResolver,
			IEnumerable<JsonConverter> contractJsonConverters)
		{

			JsonSerializerSettingsFactory = jsonSerializerSettingsFactory;
			ModifyContractResolverCallback = modifyContractResolver;
			ContractJsonConverters = contractJsonConverters ?? Enumerable.Empty<JsonConverter>();

			ConnectionSettings = connectionSettings;
			BuiltinSerializer = builtinSerializer;
			this.Converters = new List<JsonConverter>
			{
				new HandleNestTypesOnSourceJsonConverter(BuiltinSerializer),
				new TimeSpanToStringConverter()
			};
			_serializer = CreateSerializer(SerializationFormatting.Indented);
			_collapsedSerializer = CreateSerializer(SerializationFormatting.None);
		}


		private JsonSerializer CreateSerializer(SerializationFormatting formatting)
		{
			var s = CreateJsonSerializerSettings() ?? new JsonSerializerSettings();;
			var converters = CreateJsonConverters() ?? Enumerable.Empty<JsonConverter>();
			var contract = CreateContractResolver();
			s.Formatting = formatting == SerializationFormatting.Indented ? Formatting.Indented : Formatting.None;
			s.ContractResolver = contract;
			foreach (var converter in converters.Concat(this.Converters))
				s.Converters.Add(converter);

			return JsonSerializer.Create(s);
		}

		private IContractResolver CreateContractResolver()
		{
			var contract = new ConnectionSettingsAwareContractResolver(this.ConnectionSettings);
			ModifyContractResolver(contract);
			return contract;
		}

		protected virtual JsonSerializerSettings CreateJsonSerializerSettings() => JsonSerializerSettingsFactory?.Invoke();

		protected virtual IEnumerable<JsonConverter> CreateJsonConverters() => ContractJsonConverters;

		protected virtual void ModifyContractResolver(ConnectionSettingsAwareContractResolver resolver) =>
			ModifyContractResolverCallback?.Invoke(resolver);
	}
}
