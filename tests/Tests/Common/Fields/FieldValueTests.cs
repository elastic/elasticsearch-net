// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Threading.Tasks;
using Tests.Serialization;
using VerifyXunit;

namespace Tests.Common.Fields;

[UsesVerify]
public class FieldValueTests : SerializerTestBase
{
	[U]
	public async Task CanSerialize_StringFieldValueKind()
	{
		const string testValue = "test-value";

		var result = await RoundTripAndVerifyJsonAsync(new TestClass() { FieldValue = FieldValue.String(testValue) });

		result.FieldValue.IsString.Should().BeTrue();
		result.FieldValue.TryGetString(out var value).Should().BeTrue();
		value.Should().Be(testValue);
		result.FieldValue.ToString().Should().Be(testValue);
	}

	[U]
	public async Task CanSerialize_DoubleFieldValueKind()
	{
		const double testValue = 1.1;

		var result = await RoundTripAndVerifyJsonAsync(new TestClass() { FieldValue = FieldValue.Double(testValue) });

		result.FieldValue.IsDouble.Should().BeTrue();
		result.FieldValue.TryGetDouble(out var value).Should().BeTrue();
		value.Should().Be(testValue);
		result.FieldValue.ToString().Should().Be("1.1");
	}

	[U]
	public async Task CanSerialize_LongFieldValueKind()
	{
		const long testValue = 1000;

		var result = await RoundTripAndVerifyJsonAsync(new TestClass() { FieldValue = FieldValue.Long(testValue) });

		result.FieldValue.IsLong.Should().BeTrue();
		result.FieldValue.TryGetLong(out var value).Should().BeTrue();
		value.Should().Be(testValue);
		result.FieldValue.ToString().Should().Be("1000");
	}

	[U]
	public async Task CanSerialize_BoolFieldValueKind()
	{
		const bool testValue = true;

		var result = await RoundTripAndVerifyJsonAsync(new TestClass() { FieldValue = FieldValue.Boolean(testValue) });

		result.FieldValue.IsBool.Should().BeTrue();
		result.FieldValue.TryGetBool(out var value).Should().BeTrue();
		value.Should().Be(testValue);
		result.FieldValue.ToString().Should().Be("True");
	}

	[U]
	public async Task CanSerialize_NullFieldValueKind()
	{
		var result = await RoundTripAndVerifyJsonAsync(new TestClass() { FieldValue = FieldValue.Null });

		result.FieldValue.IsNull.Should().BeTrue();
		result.FieldValue.ToString().Should().Be("null");
	}

	[U]
	public void CanDeserialize_CompositeFieldValueKind()
	{
		const string json = @"{""fieldValue"":{""thing"":""value""}}";
		var obj = DeserializeJsonString<TestClass>(json);
		obj.FieldValue.TryGetLazyDocument(out var value).Should().BeTrue();
		var anotherClass = value.Value.As<AnotherClass>();
		anotherClass.Should().NotBeNull();
		anotherClass.Thing.Should().Be("value");
	}

	private class TestClass
	{
		public FieldValue FieldValue { get; set; }
	}

	private class AnotherClass
	{
		public string Thing { get; set; }
	}
}
