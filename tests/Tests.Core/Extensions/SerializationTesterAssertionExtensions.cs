/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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
