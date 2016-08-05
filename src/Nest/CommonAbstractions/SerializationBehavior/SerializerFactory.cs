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

		public SerializerFactory()
		{

		}
		public SerializerFactory(Func<IConnectionSettingsValues , IElasticsearchSerializer> serializerFactoryFunc)
		{
			this._serializerFactoryFunc = serializerFactoryFunc;
		}

		public IElasticsearchSerializer Create(IConnectionSettingsValues settings) =>
			this._serializerFactoryFunc?.Invoke(settings) ?? new JsonNetSerializer(settings);

		public IElasticsearchSerializer CreateStateful(IConnectionSettingsValues settings, JsonConverter converter) =>
			new JsonNetSerializer(settings, converter);
	}
}
