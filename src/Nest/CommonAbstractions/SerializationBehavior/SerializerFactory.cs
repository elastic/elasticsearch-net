using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
		private Func<IConnectionSettingsValues, IElasticsearchSerializer> _serializerFactoryFunc;
		private Action<JsonSerializerSettings, IConnectionSettingsValues> _settingsModifier;

		public SerializerFactory() { }
		public SerializerFactory(Func<IConnectionSettingsValues, IElasticsearchSerializer> serializerFactoryFunc) : this(serializerFactoryFunc, null) { }

		public SerializerFactory(Action<JsonSerializerSettings, IConnectionSettingsValues> settingsModifier) : this(null, settingsModifier) { }

		public SerializerFactory(Func<IConnectionSettingsValues, IElasticsearchSerializer> serializerFactoryFunc, Action<JsonSerializerSettings, IConnectionSettingsValues> settingsModifier)
		{
			this._serializerFactoryFunc = serializerFactoryFunc;
			this._settingsModifier = settingsModifier;
		}

		public IElasticsearchSerializer Create(IConnectionSettingsValues settings) =>
			this._serializerFactoryFunc?.Invoke(settings) ?? new JsonNetSerializer(settings, this._settingsModifier);

		public IElasticsearchSerializer CreateStateful(IConnectionSettingsValues settings, JsonConverter converter) =>
			new JsonNetSerializer(settings, converter);
	}
}
