using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest;
using Nest.TestData;
using Nest.TestData.Domain;
using NUnit.Framework;


namespace Nest.Tests.Search
{
	[TestFixture]
	public class ExistsTest : BaseElasticSearchTests
	{
		[Test]
		public void ShouldNotExist()
		{
			var r = this.ConnectedClient.IndexExists("yadadadadadaadada");
			Assert.False(r.Exists);
			//404 is a valid response in this case
			Assert.True(r.IsValid);
		}
		[Test]
		public void ShouldExist()
		{
			var r = this.ConnectedClient.IndexExists(this.Settings.DefaultIndex);
			Assert.True(r.Exists);
			//404 is a valid response in this case
			Assert.True(r.IsValid);
		}
	}
}