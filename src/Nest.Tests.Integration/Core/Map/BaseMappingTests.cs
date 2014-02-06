using System;
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
			this._client.DeleteMapping(dm=>dm.Index<ElasticSearchProject>().Type<ElasticSearchProject>());
		}

		protected void DefaultResponseAssertations(IIndicesResponse result)
		{
			result.Should().NotBeNull();
			if (!result.IsValid)
				throw new Exception(result.ConnectionStatus.Result);
			result.IsValid.Should().BeTrue();
		}
	}
}
