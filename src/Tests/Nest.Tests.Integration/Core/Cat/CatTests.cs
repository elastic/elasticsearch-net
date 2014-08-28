using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using Elasticsearch.Net;
using FluentAssertions;
using Nest.Resolvers;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Core.Cat
{
	[TestFixture]
	public class CatTests : IntegrationTests
	{
		private readonly ElasticClient _client;

		public CatTests()
		{
			this._client = this.Client as ElasticClient;
		}


		[Test]
		public void CatAliases()
		{
			var catResponse = this._client.CatAliases(s=>s.V());
			catResponse.Should().NotBeNull();
			catResponse.IsValid.Should().BeTrue();
			catResponse.Records.Should().NotBeEmpty().And.OnlyContain(r => !r.Alias.IsNullOrEmpty());
		}

		[Test]
		public async void CatAliasesAsync()
		{
			var catResponse = await this._client.CatAliasesAsync();
			catResponse.Should().NotBeNull();
			catResponse.IsValid.Should().BeTrue();
			catResponse.Records.Should().NotBeEmpty().And.OnlyContain(r => !r.Alias.IsNullOrEmpty());
		}
		
	}
}
