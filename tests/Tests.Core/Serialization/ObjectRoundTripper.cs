// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

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
