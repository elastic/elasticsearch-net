using System;
using Nest;

namespace Tests.Core.Serialization
{
	public static class SerializationTestHelper
	{
		public static JsonRoundTripper Expect(object expected, bool preserveNullInExpected = false) =>
			new JsonRoundTripper(expected, preserveNullInExpected: preserveNullInExpected);

		public static ObjectRoundTripper<T> Object<T>(T expected) => new ObjectRoundTripper<T>(expected);

		public static IntermediateChangedSettings WithConnectionSettings(Func<ConnectionSettings, ConnectionSettings> settings) =>
			new IntermediateChangedSettings(settings);

		public static IntermediateChangedSettings WithSourceSerializer(ConnectionSettings.SourceSerializerFactory factory) =>
			new IntermediateChangedSettings(s => s.EnableDebugMode()).WithSourceSerializer(factory);

		public class IntermediateChangedSettings
		{
			private readonly Func<ConnectionSettings, ConnectionSettings> _connectionSettingsModifier;
			private IPropertyMappingProvider _propertyMappingProvider;
			private ConnectionSettings.SourceSerializerFactory _sourceSerializerFactory;

			internal IntermediateChangedSettings(Func<ConnectionSettings, ConnectionSettings> settings) => _connectionSettingsModifier = settings;

			public JsonRoundTripper Expect(object expected, bool preserveNullInExpected = false) =>
				new JsonRoundTripper(expected, _connectionSettingsModifier, _sourceSerializerFactory, _propertyMappingProvider,
					preserveNullInExpected);

			public ObjectRoundTripper<T> Object<T>(T expected) =>
				new ObjectRoundTripper<T>(expected, _connectionSettingsModifier, _sourceSerializerFactory, _propertyMappingProvider);

			public IntermediateChangedSettings WithPropertyMappingProvider(IPropertyMappingProvider propertyMappingProvider)
			{
				_propertyMappingProvider = propertyMappingProvider;
				return this;
			}

			public IntermediateChangedSettings WithSourceSerializer(ConnectionSettings.SourceSerializerFactory factory)
			{
				_sourceSerializerFactory = factory;
				return this;
			}
		}
	}
}
