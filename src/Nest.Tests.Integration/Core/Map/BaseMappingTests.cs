using System;
using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Integration.Core.Map
{
	[TestFixture]
	public class BaseMappingTests : BaseElasticSearchTests
	{
		protected void DefaultResponseAssertations(IIndicesResponse result)
		{
			result.Should().NotBeNull();
			if (!result.IsValid)
				throw new Exception(result.ConnectionStatus.Result);
			result.IsValid.Should().BeTrue();
		}
	}
}
