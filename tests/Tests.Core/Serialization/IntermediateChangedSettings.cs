// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Nest;

namespace Tests.Core.Serialization 
{
	public class IntermediateChangedSettings
	{
		private readonly Func<ConnectionSettings, ConnectionSettings> _connectionSettingsModifier;
		private IPropertyMappingProvider _propertyMappingProvider;
		private ConnectionSettings.SourceSerializerFactory _sourceSerializerFactory;

		internal IntermediateChangedSettings(Func<ConnectionSettings, ConnectionSettings> settings) => _connectionSettingsModifier = settings;

		public IntermediateChangedSettings WithSourceSerializer(ConnectionSettings.SourceSerializerFactory factory)
		{
			_sourceSerializerFactory = factory;
			return this;
		}

		public IntermediateChangedSettings WithPropertyMappingProvider(IPropertyMappingProvider propertyMappingProvider)
		{
			_propertyMappingProvider = propertyMappingProvider;
			return this;
		}

		public JsonRoundTripper Expect(object expected, bool preserveNullInExpected = false) =>
			new JsonRoundTripper(expected, _connectionSettingsModifier, _sourceSerializerFactory, _propertyMappingProvider,
				preserveNullInExpected);

		public ObjectRoundTripper<T> Object<T>(T expected) =>
			new ObjectRoundTripper<T>(expected, _connectionSettingsModifier, _sourceSerializerFactory, _propertyMappingProvider);
	}
}