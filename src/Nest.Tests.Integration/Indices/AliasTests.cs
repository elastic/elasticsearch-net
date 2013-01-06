using NUnit.Framework;

namespace Nest.Tests.Integration.Indices
{
	[TestFixture]
	public class AliasTest : IntegrationTests
	{
		[Test]
		public void SimpleAddRemoveAlias()
		{
			var r = this._client.Alias("nest_test_data", "nest_test_data2");
			Assert.True(r.IsValid);
			Assert.True(r.OK);
			Assert.True(r.Acknowledged);
			var count1 = this._client.Count(new [] {"nest_test_data" }, q=>q.MatchAll());
			var count2 = this._client.Count(new[] { "nest_test_data2" }, q => q.MatchAll());
			Assert.AreEqual(count1.Count, count2.Count);
			r = this._client.RemoveAlias("nest_test_data", "nest_test_data2");
			count1 = this._client.Count(new[] { "nest_test_data" }, q => q.MatchAll());
			count2 = this._client.Count(new[] { "nest_test_data2" }, q => q.MatchAll());
			Assert.AreNotEqual(count1.Count, count2.Count);
			Assert.False(count2.IsValid);
		}
		[Test]
		public void SimpleRenameAlias()
		{
			var r = this._client.Alias("nest_test_data", "nest_test_data2");
			Assert.True(r.IsValid);
			Assert.True(r.OK);
			Assert.True(r.Acknowledged);
			var count1 = this._client.Count(new[] { "nest_test_data" }, q => q.MatchAll());
			var count2 = this._client.Count(new[] { "nest_test_data2" }, q => q.MatchAll());
			Assert.AreEqual(count1.Count, count2.Count);
			
			r = this._client.Rename("nest_test_data", "nest_test_data2", "nest_test_data3");
			count1 = this._client.Count(new[] { "nest_test_data" }, q => q.MatchAll());
			count2 = this._client.Count(new[] { "nest_test_data2" }, q => q.MatchAll());
			var count3 = this._client.Count(new[] { "nest_test_data3" }, q => q.MatchAll());
			Assert.AreEqual(count1.Count, count3.Count);
			Assert.AreNotEqual(count1.Count, count2.Count);
			Assert.False(count2.IsValid);

			r = this._client.RemoveAlias("nest_test_data", "nest_test_data3");
			count1 = this._client.Count(new[] { "nest_test_data" }, q => q.MatchAll());
			count3 = this._client.Count(new[] { "nest_test_data3" }, q => q.MatchAll());
			Assert.AreNotEqual(count1.Count, count3.Count);
			Assert.False(count3.IsValid);
		}
	}
}