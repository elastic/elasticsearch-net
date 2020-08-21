// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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

	}
}
