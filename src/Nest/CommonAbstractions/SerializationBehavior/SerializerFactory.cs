using System;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public interface ISerializerFactory
	{
		IElasticsearchSerializer Create(IConnectionSettingsValues settings);

		IElasticsearchSerializer CreateStateful(IConnectionSettingsValues settings, JsonConverter converter);
	}

	public class SerializerFactory : ISerializerFactory
	{
		private readonly Func<IConnectionSettingsValues, IElasticsearchSerializer> _serializerFactoryFunc;
		private readonly Action<JsonSerializerSettings, IConnectionSettingsValues> _settingsModifier;

		public SerializerFactory() { }

		public SerializerFactory(Func<IConnectionSettingsValues, IElasticsearchSerializer> serializerFactoryFunc) :
			this(serializerFactoryFunc, null) { }

		public SerializerFactory(Action<JsonSerializerSettings, IConnectionSettingsValues> settingsModifier) : this(null, settingsModifier) { }

		public SerializerFactory(Func<IConnectionSettingsValues, IElasticsearchSerializer> serializerFactoryFunc,
			Action<JsonSerializerSettings, IConnectionSettingsValues> settingsModifier
		)
		{
			_serializerFactoryFunc = serializerFactoryFunc;
			_settingsModifier = settingsModifier;
		}

		public IElasticsearchSerializer Create(IConnectionSettingsValues settings) =>
			_serializerFactoryFunc?.Invoke(settings) ?? new JsonNetSerializer(settings, _settingsModifier);

		public IElasticsearchSerializer CreateStateful(IConnectionSettingsValues settings, JsonConverter converter) =>
			new JsonNetSerializer(settings, converter);
	}
}
