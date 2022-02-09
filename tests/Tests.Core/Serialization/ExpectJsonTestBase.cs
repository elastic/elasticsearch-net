// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch;
using Tests.Core.Extensions;
using Xunit;

namespace Tests.Core.Serialization
{
	public abstract class ExpectJsonTestBase
	{
		protected ExpectJsonTestBase(IElasticsearchClient client) => Tester = new SerializationTester(client);

		protected abstract object ExpectJson { get; }
		protected abstract IReadOnlyList<object> ExpectNdjson { get; }

		protected virtual bool IncludeNullInExpected => true;
		protected virtual bool SupportsDeserialization => true; //TODO Validate all overrides for false whether they truly do not support deserialization
		protected SerializationTester Tester { get; }

		protected void SerializesNdjson<T>(T @object)
		{
			if (@object is null || ExpectNdjson is null)
				return;

			Tester.AssertSerializeNdjson(@object, ExpectNdjson, preserveNullInExpected: IncludeNullInExpected);
		}

		protected string SerializeUsingClient<T>(T @object) => Tester.SerializeUsingClient(@object);

		protected void RoundTripsOrSerializes<T>(T @object)
		{
			if (@object is null || ExpectJson is null)
				return;
			
			if (SupportsDeserialization)
				Tester.AssertRoundTrip(@object, ExpectJson, preserveNullInExpected: IncludeNullInExpected);
			else
				Tester.AssertSerialize(@object, ExpectJson, preserveNullInExpected: IncludeNullInExpected);
		}

		protected void RoundTripsOrSerializes<T>(T @object, bool supportDeserialization)
		{
			if (@object is null || ExpectJson is null)
				return;

			if (supportDeserialization)
				Tester.AssertRoundTrip(@object, ExpectJson, preserveNullInExpected: IncludeNullInExpected);
			else
				Tester.AssertSerialize(@object, ExpectJson, preserveNullInExpected: IncludeNullInExpected);
		}
	}
}
