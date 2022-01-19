using System;
using Elastic.Clients.Elasticsearch;

namespace Tests.Core.Serialization
{
	public static class SerializationTestHelper
	{
		public static JsonRoundTripper Expect(object expected, bool preserveNullInExpected = false) =>
			new(expected, preserveNullInExpected: preserveNullInExpected);

		public static ObjectRoundTripper<T> Object<T>(T expected) => new(expected);

		public static IntermediateChangedSettings WithConnectionSettings(Func<ElasticsearchClientSettings, ElasticsearchClientSettings> settings) =>
			new(settings);

		public static IntermediateChangedSettings WithSourceSerializer(ElasticsearchClientSettings.SourceSerializerFactory factory) =>
			new IntermediateChangedSettings(s => s.EnableDebugMode()).WithSourceSerializer(factory);
	}
}
