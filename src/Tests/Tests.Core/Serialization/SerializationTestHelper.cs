using System;
using Elasticsearch.Net;
using Nest;

namespace Tests.Core.Serialization
{
	public static class SerializationTestHelper
	{
		public static JsonRoundTripper Expect(object expected, bool preserveNullInExpected = false) => new JsonRoundTripper(expected, preserveNullInExpected: preserveNullInExpected);

		public static ObjectRoundTripper<T> Object<T>(T expected) => new ObjectRoundTripper<T>(expected);

		public static IntermediateChangedSettings WithConnectionSettings(Func<ConnectionSettings, ConnectionSettings> settings) =>
			new IntermediateChangedSettings(settings);

		public class IntermediateChangedSettings
		{
			private readonly Func<ConnectionSettings, ConnectionSettings> _connectionSettingsModifier;
			private ISerializerFactory _serializerFactory;

			internal IntermediateChangedSettings(Func<ConnectionSettings, ConnectionSettings> settings)
			{
				this._connectionSettingsModifier = settings;
			}

			public IntermediateChangedSettings WithSerializer(Func<IConnectionSettingsValues, IElasticsearchSerializer> serializerFactory)
			{
				this._serializerFactory = new SerializerFactory(serializerFactory);
				return this;
			}
			public IntermediateChangedSettings WithSerializer(ISerializerFactory serializerFactory)
			{
				this._serializerFactory = serializerFactory;
				return this;
			}

			public JsonRoundTripper Expect(object expected, bool preserveNullInExpected = false) =>
				new JsonRoundTripper(expected, _connectionSettingsModifier, _serializerFactory, preserveNullInExpected);

			public ObjectRoundTripper<T> Object<T>(T expected) => new ObjectRoundTripper<T>(expected, _connectionSettingsModifier, _serializerFactory);

		}
	}
}
