using System;
using Nest;

namespace Tests.Core.Serialization
{
	public static class SerializationTestHelper
	{
		public static JsonRoundTripper Expect(object expected, bool preserveNullInExpected = false) => new JsonRoundTripper(expected, preserveNullInExpected: preserveNullInExpected);

		public static ObjectRoundTripper<T> Object<T>(T expected) => new ObjectRoundTripper<T>(expected);

		public static IntermediateChangedSettings WithConnectionSettings(Func<ConnectionSettings, ConnectionSettings> settings) =>
			new IntermediateChangedSettings(settings);

		public static IntermediateChangedSettings WithSourceSerializer(ConnectionSettings.SourceSerializerFactory factory) =>
			new IntermediateChangedSettings(s=>s.EnableDebugMode()).WithSourceSerializer(factory);

		public class IntermediateChangedSettings
		{
			private readonly Func<ConnectionSettings, ConnectionSettings> _connectionSettingsModifier;
			private ConnectionSettings.SourceSerializerFactory _sourceSerializerFactory;
			private IPropertyMappingProvider _propertyMappingProvider;

			internal IntermediateChangedSettings(Func<ConnectionSettings, ConnectionSettings> settings)
			{
				this._connectionSettingsModifier = settings;
			}

			public IntermediateChangedSettings WithSourceSerializer(ConnectionSettings.SourceSerializerFactory factory)
			{
				this._sourceSerializerFactory = factory;
				return this;
			}

			public IntermediateChangedSettings WithPropertyMappingProvider(IPropertyMappingProvider propertyMappingProvider)
			{
				this._propertyMappingProvider = propertyMappingProvider;
				return this;
			}

			public JsonRoundTripper Expect(object expected, bool preserveNullInExpected = false) =>
				new JsonRoundTripper(expected, _connectionSettingsModifier, this._sourceSerializerFactory, this._propertyMappingProvider, preserveNullInExpected);

			public ObjectRoundTripper<T> Object<T>(T expected) =>
				new ObjectRoundTripper<T>(expected, _connectionSettingsModifier, this._sourceSerializerFactory, this._propertyMappingProvider);

		}
	}
}
