// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Linq;
using System.Threading.Tasks;
using Tests.Core.Xunit;
using Tests.Domain;
using VerifyXunit;

namespace Tests.Serialization
{
	public class IndicesTests
	{
		private const string TestIndexName = "an-index";

		[U]
		public void StoresOnlyAllValue_WhenIndexNamesContainsAll()
		{
			Indices indices = new[] { "index-1", "_all", "index-2" };

			indices.Should().HaveCount(1);
			indices.First().Should().Be(Indices.AllValue);
		}

		[U]
		public void StoresOnlyAllValue_WhenIndexNamesAllWildcard()
		{
			Indices indices = new[] { "index-1", "index-2", "*" };

			indices.Should().HaveCount(1);
			indices.First().Should().Be(Indices.AllValue);
		}

		[U]
		public void StoresOnlyAllValue_WhenParseStringContainsAll()
		{
			var indices = Indices.Parse("index-1,index-2,_all,index-3");
			indices.Should().HaveCount(1);
			indices.First().Should().Be(Indices.AllValue);
		}

		[U]
		public void StoresOnlyAllValue_WhenParseStringContainsAllWildcard()
		{
			var indices = Indices.Parse("index-1,index-2,*,index-3");
			indices.Should().HaveCount(1);
			indices.First().Should().Be(Indices.AllValue);
		}

		[U]
		public void DoesNotAdd_WhenIndicesAll()
		{
			var indices = Indices.All;

			indices = indices.And<Project>();

			indices.Should().HaveCount(1);
			indices.First().Should().Be(Indices.AllValue);
		}

		[U]
		public void GetHashCode_Matches_ForAll()
		{
			var indices1 = Indices.Parse("*");
			var indices2 = Indices.All;

			indices1.GetHashCode().Should().Be(indices2.GetHashCode());
		}

		[U]
		public void GetHashCode_Matches()
		{
			Indices indices1 = TestIndexName;
			Indices indices2 = TestIndexName;

			indices1.GetHashCode().Should().Be(indices2.GetHashCode());
		}

		[U]
		public void Equals_IsTrue()
		{
			Indices indices1 = TestIndexName;
			Indices indices2 = TestIndexName;

			indices1.Equals(indices2).Should().BeTrue();
		}

		[U]
		public void Equals_IsTrueForString()
		{
			Indices indices = TestIndexName;

			indices.Equals(Indices.Parse(TestIndexName)).Should().BeTrue();
		}
	}

	[UsesVerify]
	[SystemTextJsonOnly]
	public class IndicesSerializationTests : SerializerTestBase
	{
		[U]
		public async Task Serializes_All_Correctly()
		{
			var obj = new TestThing(Indices.All);
			var serialisedJson = await SerializeAndGetJsonStringAsync(obj);
			await Verifier.VerifyJson(serialisedJson);
		}

		[U]
		public void Deserializes_All_Correctly()
		{
			const string json = @"{""indices"":""_all""}";
			var obj = DeserializeJsonString<TestThing>(json);
			obj.Indices.Should().HaveCount(1);
			obj.Indices.First().Name.Should().Be("_all");
		}

		[U]
		public async Task Serializes_Correctly()
		{
			var obj = new TestThing(Indices.Parse("index-a,index-b"));
			var serialisedJson = await SerializeAndGetJsonStringAsync(obj);
			await Verifier.VerifyJson(serialisedJson);
		}

		private class TestThing
		{
			public TestThing(Indices indices) => Indices = indices;

			public Indices Indices { get; init; }
		}
	}
}
