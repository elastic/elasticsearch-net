// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Threading.Tasks;
using VerifyXunit;

namespace Tests.Serialization;

[UsesVerify]
public class AvgAggregateDeserializationTests : SerializerTestBase
{
	[U]
	public async Task CanDeserialize_AvgAggregate()
	{
		var json = @"{""aggregations"":{""avg#my-agg-name"":{""value"":75}}}";

		var stream = WrapInStream(json);

		var search = _requestResponseSerializer.Deserialize<BasicSearchResponse>(stream);

		await Verifier.Verify(search);
	}

	[U]
	public async Task CanDeserialize_AvgAggregate_WithNullValue()
	{
		var json = @"{""aggregations"":{""avg#my-agg-name"":{""value"":null}}}";

		var stream = WrapInStream(json);

		var search = _requestResponseSerializer.Deserialize<BasicSearchResponse>(stream);

		await Verifier.Verify(search);
	}
}
