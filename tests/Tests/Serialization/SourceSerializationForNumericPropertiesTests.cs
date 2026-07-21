// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

namespace Tests.Serialization;

public class SourceSerializationForNumericPropertiesTests : SerializerTestBase
{
	[U]
	public void FloatValuesIncludeDecimal_AndAreNotRounded()
	{
		var numericTests = new NumericTests { Float = 1.0f };

		var json = SourceSerializeAndGetJsonString(numericTests);

		json.Should().Be("{\"float\":1.0}");

		var deserialized = SourceDeserializeJsonString<NumericTests>(json);

		deserialized.Float.Should().Be(1.0f);
	}

	[U]
	public void FloatMinValue_SerializesCorrectly()
	{
		var numericTests = new NumericTests { Float = float.MinValue };

		var json = SourceSerializeAndGetJsonString(numericTests);

		json.Should().Be("{\"float\":-3.40282347E+38}");

		var deserialized = SourceDeserializeJsonString<NumericTests>(json);

		deserialized.Float.Should().Be(float.MinValue);
	}

	[U]
	public void FloatMaxValue_SerializesCorrectly()
	{
		var numericTests = new NumericTests { Float = float.MaxValue };

		var json = SourceSerializeAndGetJsonString(numericTests);

		json.Should().Be("{\"float\":3.40282347E+38}");

		var deserialized = SourceDeserializeJsonString<NumericTests>(json);

		deserialized.Float.Should().Be(float.MaxValue);
	}

	[U]
	public void DoubleValuesIncludeFractionalPart_AndAreNotRounded()
	{
		var numericTests = new NumericTests { Double = 1.0 };

		var json = SourceSerializeAndGetJsonString(numericTests);

		json.Should().Be("{\"double\":1.0}");

		var deserialized = SourceDeserializeJsonString<NumericTests>(json);

		deserialized.Double.Should().Be(1.0);
	}

	[U]
	public void DoubleMinValue_SerializesCorrectly()
	{
		var numericTests = new NumericTests { Double = double.MinValue };

		var json = SourceSerializeAndGetJsonString(numericTests);

		json.Should().Be("{\"double\":-1.7976931348623157E+308}");

		var deserialized = SourceDeserializeJsonString<NumericTests>(json);

		deserialized.Double.Should().Be(double.MinValue);
	}

	[U]
	public void DoubleMaxValue_SerializesCorrectly()
	{
		var numericTests = new NumericTests { Double = double.MaxValue };

		var json = SourceSerializeAndGetJsonString(numericTests);

		json.Should().Be("{\"double\":1.7976931348623157E+308}");

		var deserialized = SourceDeserializeJsonString<NumericTests>(json);

		deserialized.Double.Should().Be(double.MaxValue);
	}

	[U]
	public void DoubleAsString_DeserializesCorrectly()
	{
		var json = "{\"double\":\"1.0\"}";

		var deserialized = SourceDeserializeJsonString<NumericTests>(json);

		deserialized.Double.Should().Be(1.0);
	}

	[U]
	public void DecimalValuesIncludeDecimal_AndAreNotRounded()
	{
		var numericTests = new NumericTests { Decimal = 1.0m };

		var json = SourceSerializeAndGetJsonString(numericTests);

		json.Should().Be("{\"decimal\":1.0}");
	}

	private class NumericTests
	{
		public float? Float { get; set; }
		public double? Double { get; set; }
		public decimal? Decimal { get; set; }
	}
}
