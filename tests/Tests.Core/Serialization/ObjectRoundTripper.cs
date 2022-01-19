using System;
using Elastic.Clients.Elasticsearch;
using Tests.Core.Extensions;

namespace Tests.Core.Serialization
{
	public class ObjectRoundTripper<T> : RoundTripperBase
	{
		private readonly T _object;

		internal ObjectRoundTripper(T @object,
			Func<ElasticsearchClientSettings, ElasticsearchClientSettings> settingsModifier = null,
			ElasticsearchClientSettings.SourceSerializerFactory sourceSerializerFactory = null,
			IPropertyMappingProvider propertyMappingProvider = null
		)
			: base(settingsModifier, sourceSerializerFactory, propertyMappingProvider) => _object = @object;

		public ObjectRoundTripper<T> PreserveNull()
		{
			PreserveNullInExpected = true;
			return this;
		}

		public T RoundTrips() => Tester.AssertRoundTrip(_object);

		public T RoundTrips(object expectedJson) => Tester.AssertRoundTrip(_object, expectedJson, preserveNullInExpected: PreserveNullInExpected);
	}
}
