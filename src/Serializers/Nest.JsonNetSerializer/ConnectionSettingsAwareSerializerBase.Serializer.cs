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
		protected IConnectionSettingsValues ConnectionSettings { get; }
		protected IElasticsearchSerializer BuiltinSerializer { get; }

		private List<JsonConverter> Converters { get; }

		protected ConnectionSettingsAwareSerializerBase(
			IElasticsearchSerializer builtinSerializer,
			IConnectionSettingsValues connectionSettings)
		{
			ConnectionSettings = connectionSettings;
			BuiltinSerializer = builtinSerializer;
			this.Converters = new List<JsonConverter>
			{
				new RevertBackToBuiltinSerializer(BuiltinSerializer),
				new TimeSpanToStringConverter()
			};
			_serializer = CreateSerializer(SerializationFormatting.Indented);
			_collapsedSerializer = CreateSerializer(SerializationFormatting.None);

		}

		private JsonSerializer CreateSerializer(SerializationFormatting formatting)
		{
			var s = CreateJsonSerializerSettings();
			var converters = CreateJsonConverters() ?? Enumerable.Empty<JsonConverter>();
			var contract = CreateContractResolver();
			s.Formatting = formatting == SerializationFormatting.Indented ? Formatting.Indented : Formatting.None;
			s.ContractResolver = contract;
			s.Converters = converters.Concat(this.Converters).ToList();
			return JsonSerializer.Create(s);
		}
		private IContractResolver CreateContractResolver()
		{
			var contract = new ConnectionSettingsAwareContractResolver(this.ConnectionSettings);
			ModifyContractResolver(contract);
			return contract;
		}

		protected abstract JsonSerializerSettings CreateJsonSerializerSettings();

		//TODO
		protected abstract IEnumerable<JsonConverter> CreateJsonConverters();

		protected virtual void ModifyContractResolver(ConnectionSettingsAwareContractResolver resolver) { }
	}

	public class JsonNetSerializer : ConnectionSettingsAwareSerializerBase
	{
		private readonly Func<JsonSerializerSettings> _jsonSerializerSettingsFactory;
		private readonly Action<ConnectionSettingsAwareContractResolver> _modifyContractResolver;
		private readonly IEnumerable<JsonConverter> _contractJsonConverters;

		public static IElasticsearchSerializer Default(IElasticsearchSerializer builtin, IConnectionSettingsValues values)
			=> new JsonNetSerializer(builtin, values);

		public JsonNetSerializer(
			IElasticsearchSerializer builtinSerializer,
			IConnectionSettingsValues connectionSettings,
			Func<JsonSerializerSettings> jsonSerializerSettingsFactory = null,
			Action<ConnectionSettingsAwareContractResolver> modifyContractResolver = null,
			IEnumerable<JsonConverter> contractJsonConverters = null)
			: base(builtinSerializer, connectionSettings)
		{
			_jsonSerializerSettingsFactory = jsonSerializerSettingsFactory;
			_modifyContractResolver = modifyContractResolver;
			_contractJsonConverters = contractJsonConverters ?? Enumerable.Empty<JsonConverter>();
		}

		protected override JsonSerializerSettings CreateJsonSerializerSettings() => _jsonSerializerSettingsFactory?.Invoke();

		protected override IEnumerable<JsonConverter> CreateJsonConverters() => _contractJsonConverters;

		protected override void ModifyContractResolver(ConnectionSettingsAwareContractResolver resolver) =>
			_modifyContractResolver?.Invoke(resolver);

	}
}
