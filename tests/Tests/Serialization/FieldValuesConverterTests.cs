// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

namespace Tests.Serialization;

public class FieldValuesConverterTests : SerializerTestBase
{
	private const string BasicMatchQueryJson = @"{""user.id"":[""kimchy""],""@timestamp"":[""4098435132000""],""http.response.bytes"":[1070000],""http.response.status_code"":[200]}";

	[U]
	public void CanDeserialize_FieldsValues()
	{
		var stream = WrapInStream(BasicMatchQueryJson);

		var fieldValues = _requestResponseSerializer.Deserialize<FieldValues>(stream);
		fieldValues.Value<string>("user.id").Should().Be("kimchy");
		fieldValues.Values<string>("@timestamp").Should().HaveCount(1);
		fieldValues.Values<string>("@timestamp")[0].Should().Be("4098435132000");
	}
}
