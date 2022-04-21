// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Threading.Tasks;
using Tests.Core.Xunit;
using VerifyXunit;

namespace Tests.Serialization
{
	public class DataStreamNamesTests
	{
		private static readonly string[] TestDataStreamNames = new[] { "test-datastream-1", "test-datastream-2" };

		[U]
		public void ImplictlyCreatedFromCommaSeparatedNames()
		{
			DataStreamNames dataStreamNames = "test-datastream-1,test-datastream-2";
			dataStreamNames.Names.Count.Should().Be(2);
		}

		[U]
		public void GetHashCode_Matches_WhenOrderAndNamesAreTheSame()
		{
			DataStreamNames dsn1 = TestDataStreamNames;
			DataStreamNames dsn2 = TestDataStreamNames;

			dsn1.GetHashCode().Should().Be(dsn2.GetHashCode());
		}

		[U]
		public void Equals_IsTrue_WhenOrderAndNamesAreTheSame()
		{
			DataStreamNames dsn1 = TestDataStreamNames;
			DataStreamNames dsn2 = TestDataStreamNames;

			dsn1.Equals(dsn2).Should().BeTrue();
		}

		[U]
		public void Equals_IsTrue_WhenOrderIsDifferent()
		{
			DataStreamNames dsn1 = TestDataStreamNames;
			DataStreamNames dsn2 = new[] { "test-datastream-2", "test-datastream-1" };

			dsn1.Equals(dsn2).Should().BeTrue();
		}

		[U]
		public void GetHashCode_Matches_WhenOrderIsDifferent()
		{
			DataStreamNames dsn1 = TestDataStreamNames;
			DataStreamNames dsn2 = new[] { "test-datastream-2", "test-datastream-1" };

			dsn1.GetHashCode().Should().Be(dsn2.GetHashCode());
		}

		[U]
		public void ToString_ReturnsExpectedValue()
		{
			DataStreamNames dataStreamNames = TestDataStreamNames;
			dataStreamNames.ToString().Should().Be("test-datastream-1,test-datastream-2");
		}

		[U]
		public void GetString_ReturnsExpectedValue()
		{
			DataStreamNames dataStreamNames = TestDataStreamNames;
			var urlParameter = dataStreamNames as IUrlParameter;
			urlParameter.GetString(null).Should().Be("test-datastream-1,test-datastream-2");
		}
	}

	[UsesVerify]
	[SystemTextJsonOnly]
	public class DataStreamNamesSerializationTests : SerializerTestBase
	{
		private static readonly string[] TestDataStreamNames = new[] { "test-datastream-1", "test-datastream-2" };

		[U]
		public async Task SerializesCorrectly()
		{
			var obj = new TestThing();
			var serialisedJson = await SerializeAndGetJsonStringAsync(obj);
			await Verifier.VerifyJson(serialisedJson);
		}

		[U]
		public async void DeserializesCorrectly()
		{
			const string json = @"{""dataStreamName"":[""test-datastream-1"",""test-datastream-2""]}";
			var obj = DeserializeJsonString<TestThing>(json);
			await Verifier.Verify(obj);
		}

		private class TestThing
		{
			public DataStreamNames DataStreamName { get; } = TestDataStreamNames;
		}
	}
}
