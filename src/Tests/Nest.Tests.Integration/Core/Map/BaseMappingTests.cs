using Elasticsearch.Net;
using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;
using Shared.Extensions;
using System;

namespace Nest.Tests.Integration.Core.Map
{
	[TestFixture]
	public class BaseMappingTests : IntegrationTests
	{
		[TestFixtureSetUp]
		public void Initialize()
		{
			this._client.DeleteMapping(dm=>dm.Index<ElasticsearchProject>().Type<ElasticsearchProject>());
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
