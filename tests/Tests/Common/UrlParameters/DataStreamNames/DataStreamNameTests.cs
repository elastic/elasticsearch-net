// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Threading.Tasks;
using Tests.Core.Xunit;
using VerifyXunit;

namespace Tests.Serialization
{
	public class DataStreamNameTests
	{
		private const string TestDataStreamName = "test-datastream";

		[U]
		public void GetHashCode_Matches()
		{
			DataStreamName dsn1 = TestDataStreamName;
			DataStreamName dsn2 = TestDataStreamName;

			dsn1.GetHashCode().Should().Be(dsn2.GetHashCode());
		}

		[U]
		public void Equals_IsTrue()
		{
			DataStreamName dsn1 = TestDataStreamName;
			DataStreamName dsn2 = TestDataStreamName;

			dsn1.Equals(dsn2).Should().BeTrue();
		}

		[U]
		public void Equals_IsTrueForString()
		{
			DataStreamName dsn1 = TestDataStreamName;

			dsn1.Equals(TestDataStreamName).Should().BeTrue();
		}
	}

	[UsesVerify]
	[SystemTextJsonOnly]
	public class DataStreamNameSerializationTests : SerializerTestBase
	{
		private const string TestDataStreamName = "test-datastream";

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
			const string json = @"{""dataStreamName"":""test-datastream""}";
			var obj = DeserializeJsonString<TestThing>(json);
			await Verifier.Verify(obj);
		}

		private class TestThing
		{
			public DataStreamName DataStreamName { get; } = TestDataStreamName;
		}
	}
}
