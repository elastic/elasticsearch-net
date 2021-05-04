// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Tests.Core.Serialization;

namespace Tests.Core.Extensions
{
	public static class SerializationTesterAssertionExtensions
	{
		public static void ShouldBeValid(this SerializationResult result, string message = null)
		{
			if (result.Success) return;

			throw new Exception($@"Expected serialization to succeed but failed.
{(message ?? string.Empty) + result}
");
		}

		public static T AssertRoundTrip<T>(this SerializationTester tester, T @object, string message = null, bool preserveNullInExpected = false)
		{
			var roundTripResult = tester.RoundTrips(@object, preserveNullInExpected);
			roundTripResult.ShouldBeValid(message);
			return roundTripResult.Result;
		}

		public static T AssertRoundTrip<T>(this SerializationTester tester, T @object, object expectedJson, string message = null,
			bool preserveNullInExpected = false
		)
		{
			var roundTripResult = tester.RoundTrips(@object, expectedJson, preserveNullInExpected);
			roundTripResult.ShouldBeValid(message);
			return roundTripResult.Result;
		}

		public static T AssertDeserialize<T>(this SerializationTester tester, object json, string message = null, bool preserveNullInExpected = false)
		{
			var roundTripResult = tester.Deserializes<T>(json, preserveNullInExpected);
			roundTripResult.ShouldBeValid(message);
			return roundTripResult.Result;
		}

		public static void AssertSerialize<T>(this SerializationTester tester, T @object, object expectedJson, string message = null,
			bool preserveNullInExpected = false
		)
		{
			var roundTripResult = tester.Serializes(@object, expectedJson, preserveNullInExpected);
			roundTripResult.ShouldBeValid(message);
		}
	}
}
