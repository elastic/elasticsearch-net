// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Threading.Tasks;
using Tests.Core.Xunit;
using VerifyXunit;

namespace Tests.Serialization
{
	public class IndexAliasTests
	{
		private const string TestIndexAliasName = "an-index-alias";

		[U]
		public void GetHashCode_Matches()
		{
			IndexAlias alias1 = TestIndexAliasName;
			IndexAlias alias2 = TestIndexAliasName;

			alias1.GetHashCode().Should().Be(alias2.GetHashCode());
		}

		[U]
		public void Equals_IsTrue()
		{
			IndexAlias alias1 = TestIndexAliasName;
			IndexAlias alias2 = TestIndexAliasName;

			alias1.Equals(alias2).Should().BeTrue();
		}

		[U]
		public void Equals_IsTrueForString()
		{
			IndexAlias alias1 = TestIndexAliasName;

			alias1.Equals(TestIndexAliasName).Should().BeTrue();
		}
	}

	[UsesVerify]
	[SystemTextJsonOnly]
	public class IndexAliasSerializationTests : SerializerTestBase
	{
		private const string TestIndexAliasName = "an-index-alias";

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
			const string json = @"{""indexAlias"":""an-index-alias""}";
			var obj = DeserializeJsonString<TestThing>(json);
			await Verifier.Verify(obj);
		}

		private class TestThing
		{
			public IndexAlias IndexAlias { get; } = TestIndexAliasName;
		}
	}
}
