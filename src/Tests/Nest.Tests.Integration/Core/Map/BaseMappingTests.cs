using System;
using Elasticsearch.Net;
using FluentAssertions;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Integration.Core.Map
{
	[TestFixture]
	public class BaseMappingTests : IntegrationTests
	{
		[TestFixtureSetUp]
		public void Initialize()
		{
			this._client.DeleteMapping<ElasticsearchProject>();
		}

		protected void DefaultResponseAssertations(IIndicesResponse result)
		{
			result.Should().NotBeNull();
			if (!result.IsValid)
				throw new Exception(result.ConnectionStatus.ResponseRaw.Utf8String());
			result.IsValid.Should().BeTrue();
		}
	}
}
