using System;
using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.ObjectInitializer.Aliases
{
	[TestFixture]
	public class GetAliasMoreUrlTests : BaseJsonTests
	{

		[Test]
		public void NoIndexAndAlias()
		{
			var request = new GetAliasRequest();
			var response = this._client.GetAlias(request);
			
			var status = response.ConnectionStatus;
			if (Type.GetType("Mono.Runtime") != null)
				status.RequestUrl.Should().EndWith("/_alias/%2A");
			else 
				status.RequestUrl.Should().EndWith("/_alias/*");
			
			status.RequestMethod.Should().Be("GET");
		}

		[Test]
		public void IndexAndAlias()
		{
			var request = new GetAliasesRequest()
			{
				Indices = new IndexNameMarker[] { "my-index" } ,
				Alias = "my-alias"  
			};
			var response = this._client.GetAliases(request);
			
			var status = response.ConnectionStatus;
		
			status.RequestUrl.Should().EndWith("/my-index/_aliases/my-alias");
			status.RequestMethod.Should().Be("GET");
		}
	}
}
