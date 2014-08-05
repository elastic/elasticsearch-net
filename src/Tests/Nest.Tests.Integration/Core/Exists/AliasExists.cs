using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Integration.Core.Exists
{
	[TestFixture]
	public class AliasExistsTest : IntegrationTests
	{
		[Test]
		public void ShouldNotExist()
		{
			var r = this.Client.AliasExists("alias", "index");
			Assert.False(r.Exists);
			//404 is a valid response in this case
			Assert.True(r.IsValid);
		}
		[Test]
		public void ShouldExist()
		{
			var aliasResponse = this.Client.Alias(new AliasRequest()
			{
				Actions = new List<IAliasAction>
				{
					{ new AliasAddAction { Add = new AliasAddOperation { Index = ElasticsearchConfiguration.DefaultIndex, Alias = "my-alias"} } }
				}
			});
			aliasResponse.IsValid.Should().BeTrue();
			var r = this.Client.AliasExists("my-alias");
			Assert.True(r.Exists);
			//404 is a valid response in this case
			Assert.True(r.IsValid);
		}
	}
}