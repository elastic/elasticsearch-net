// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using Elastic.Clients.Elasticsearch;

namespace Tests.Core.Serialization
{
	public class IntermediateChangedSettings
	{
		private readonly Func<ElasticsearchClientSettings, ElasticsearchClientSettings> _transportClientSettingsModifier;
		private IPropertyMappingProvider _propertyMappingProvider;
		private ElasticsearchClientSettings.SourceSerializerFactory _sourceSerializerFactory;

		internal IntermediateChangedSettings(Func<ElasticsearchClientSettings, ElasticsearchClientSettings> settings) => _transportClientSettingsModifier = settings;

		public IntermediateChangedSettings WithSourceSerializer(ElasticsearchClientSettings.SourceSerializerFactory factory)
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
			new(expected, _transportClientSettingsModifier, _sourceSerializerFactory, _propertyMappingProvider,
				preserveNullInExpected);

		public ObjectRoundTripper<T> Object<T>(T expected) =>
			new(expected, _transportClientSettingsModifier, _sourceSerializerFactory, _propertyMappingProvider);
	}
}
