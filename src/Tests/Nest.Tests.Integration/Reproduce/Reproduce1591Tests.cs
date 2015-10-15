using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Integration.Reproduce
{
	[TestFixture]
	public class Reproduce1591Tests : IntegrationTests
	{
		private readonly string _aliasName = Guid.NewGuid().ToString();
		private readonly string _indexName = Guid.NewGuid().ToString();

		[SetUp]
		public void SetUp()
		{
			Client.CreateIndex(_indexName);
		}

		[TearDown]
		public void Teardown()
		{
			Client.DeleteIndex(_indexName);
		}

		[Test]
		public void Test()
		{
			var putAliasResponse = Client.PutAlias(d => d
				.Index(_indexName)
				.Name(_aliasName)
				.IndexRouting("indexrouting")
				.SearchRouting("searchrouting"));

			putAliasResponse.IsValid.Should().BeTrue();

			var catResponse = Client.CatAliases(d => d.V()).Records
				.FirstOrDefault(x => x.Index == _indexName);

			catResponse.Should().NotBeNull();
			catResponse.Alias.Should().Be(_aliasName);
			catResponse.IndexRouting.Should().Be("indexrouting");
			catResponse.SearchRouting.Should().Be("searchrouting");
		}
	}
}