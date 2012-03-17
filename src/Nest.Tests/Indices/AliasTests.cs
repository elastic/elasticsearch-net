using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest;
using Nest.TestData;
using Nest.TestData.Domain;
using NUnit.Framework;
using Nest.Mapping;

namespace Nest.Tests.Search
{
	[TestFixture]
	public class AliasTest : BaseElasticSearchTests
	{
		[Test]
		public void SimpleAddRemoveAlias()
		{
			var r = this.ConnectedClient.Alias("nest_test_data", "nest_test_data2");
			Assert.True(r.IsValid);
			Assert.True(r.OK);
			Assert.True(r.Acknowledged);
			var count1 = this.ConnectedClient.Count(new [] {"nest_test_data" }, q=>q.MatchAll());
			var count2 = this.ConnectedClient.Count(new[] { "nest_test_data2" }, q => q.MatchAll());
			Assert.AreEqual(count1.Count, count2.Count);
			r = this.ConnectedClient.RemoveAlias("nest_test_data", "nest_test_data2");
			count1 = this.ConnectedClient.Count(new[] { "nest_test_data" }, q => q.MatchAll());
			count2 = this.ConnectedClient.Count(new[] { "nest_test_data2" }, q => q.MatchAll());
			Assert.AreNotEqual(count1.Count, count2.Count);
			Assert.False(count2.IsValid);
		}
		[Test]
		public void SimpleRenameAlias()
		{
			var r = this.ConnectedClient.Alias("nest_test_data", "nest_test_data2");
			Assert.True(r.IsValid);
			Assert.True(r.OK);
			Assert.True(r.Acknowledged);
			var count1 = this.ConnectedClient.Count(new[] { "nest_test_data" }, q => q.MatchAll());
			var count2 = this.ConnectedClient.Count(new[] { "nest_test_data2" }, q => q.MatchAll());
			Assert.AreEqual(count1.Count, count2.Count);
			
			r = this.ConnectedClient.Rename("nest_test_data", "nest_test_data2", "nest_test_data3");
			count1 = this.ConnectedClient.Count(new[] { "nest_test_data" }, q => q.MatchAll());
			count2 = this.ConnectedClient.Count(new[] { "nest_test_data2" }, q => q.MatchAll());
			var count3 = this.ConnectedClient.Count(new[] { "nest_test_data3" }, q => q.MatchAll());
			Assert.AreEqual(count1.Count, count3.Count);
			Assert.AreNotEqual(count1.Count, count2.Count);
			Assert.False(count2.IsValid);

			r = this.ConnectedClient.RemoveAlias("nest_test_data", "nest_test_data3");
			count1 = this.ConnectedClient.Count(new[] { "nest_test_data" }, q => q.MatchAll());
			count3 = this.ConnectedClient.Count(new[] { "nest_test_data3" }, q => q.MatchAll());
			Assert.AreNotEqual(count1.Count, count3.Count);
			Assert.False(count3.IsValid);
		}
	}
}