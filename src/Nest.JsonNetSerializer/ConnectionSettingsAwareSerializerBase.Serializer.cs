// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq;
using Elastic.Transport;
using Nest.JsonNetSerializer.Converters;
using Newtonsoft.Json;

namespace Nest.JsonNetSerializer
{
	public abstract partial class ConnectionSettingsAwareSerializerBase
	{
		protected ConnectionSettingsAwareSerializerBase(ITransportSerializer builtinSerializer, IConnectionSettingsValues connectionSettings)
			: this(builtinSerializer, connectionSettings, null, null, null) { }

		internal ConnectionSettingsAwareSerializerBase(
			ITransportSerializer builtinSerializer,
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
				new HandleNestTypesOnSourceJsonConverter(BuiltinSerializer, connectionSettings.MemoryStreamFactory),
				new TimeSpanToStringConverter()
			};
			_serializer = CreateSerializer(SerializationFormatting.Indented);
			_collapsedSerializer = CreateSerializer(SerializationFormatting.None);
		}

		protected ITransportSerializer BuiltinSerializer { get; }

		protected IConnectionSettingsValues ConnectionSettings { get; }
		protected IEnumerable<JsonConverter> ContractJsonConverters { get; }
		protected Func<JsonSerializerSettings> JsonSerializerSettingsFactory { get; }
		protected Action<ConnectionSettingsAwareContractResolver> ModifyContractResolverCallback { get; }

		private List<JsonConverter> Converters { get; }

		private JsonSerializer CreateSerializer(SerializationFormatting formatting)
		{
			var s = CreateJsonSerializerSettings() ?? new JsonSerializerSettings();
			var converters = CreateJsonConverters() ?? Enumerable.Empty<JsonConverter>();
			var contract = CreateContractResolver();
			s.Formatting = formatting == SerializationFormatting.Indented ? Formatting.Indented : Formatting.None;
			s.ContractResolver = contract;
			foreach (var converter in converters.Concat(Converters))
				s.Converters.Add(converter);

			return JsonSerializer.Create(s);
		}

		protected virtual ConnectionSettingsAwareContractResolver CreateContractResolver()
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
