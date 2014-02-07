using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Indices
{
	[TestFixture]
	public class AliasTest : IntegrationTests
	{
		[Test]
		public void SimpleAddRemoveAlias()
		{
			var index = ElasticsearchConfiguration.DefaultIndex;
			var alias = ElasticsearchConfiguration.DefaultIndex + "-2";

			var r = this._client.Alias(a=>a.Add(o=>o.Index(index).Alias(alias)));
			Assert.True(r.IsValid);
			Assert.True(r.OK);
			Assert.True(r.Acknowledged);
			var count1 = this._client.Count<ElasticsearchProject>(c => c.Index(index).Query(q => q.MatchAll()));
			var count2 = this._client.Count<ElasticsearchProject>(c => c.Index(alias).Query(q => q.MatchAll()));
			Assert.AreEqual(count1.Count, count2.Count);
			r = this._client.Alias(a=>a.Remove(o=>o.Index(index).Alias(alias)));
			count1 = this._client.Count<ElasticsearchProject>(c=>c.Index(index).Query(q=>q.MatchAll()));
			count2 = this._client.Count<ElasticsearchProject>(c=>c.Index(alias).Query(q=>q.MatchAll()));
			Assert.AreNotEqual(count1.Count, count2.Count);
			Assert.False(count2.IsValid);
		}
		[Test]
		public void SimpleRenameAlias()
		{
			var index = ElasticsearchConfiguration.DefaultIndex;
			var alias = ElasticsearchConfiguration.DefaultIndex + "-2";

			var r = this._client.Alias(a=>a.Add(o=>o.Index(index).Alias(alias)));
			Assert.True(r.IsValid);
			Assert.True(r.OK);
			Assert.True(r.Acknowledged);
			var count1 = this._client.Count<ElasticsearchProject>(c=>c.Index(index).Query(q=>q.MatchAll()));
			var count2 = this._client.Count<ElasticsearchProject>(c=>c.Index(alias).Query(q=>q.MatchAll()));
			Assert.AreEqual(count1.Count, count2.Count);

			var renamed = index + "-3";

			r = this._client.Alias(a=>a
				.Remove(o=>o.Index(index).Alias(alias))
				.Add(o=>o.Index(index).Alias(renamed))
			);
			count1 = this._client.Count<ElasticsearchProject>(c=>c.Index(index).Query(q=>q.MatchAll()));
			count2 = this._client.Count<ElasticsearchProject>(c=>c.Index(alias).Query(q=>q.MatchAll()));
			var count3 = this._client.Count<ElasticsearchProject>(c=>c.Index(renamed).Query(q=>q.MatchAll()));
			Assert.AreEqual(count1.Count, count3.Count);
			Assert.AreNotEqual(count1.Count, count2.Count);
			Assert.False(count2.IsValid);

			r = this._client.Alias(a=>a.Remove(o=>o.Index(index).Alias(renamed)));
			count1 = this._client.Count<ElasticsearchProject>(c=>c.Index(index).Query(q=>q.MatchAll()));
			count3 = this._client.Count<ElasticsearchProject>(c=>c.Index(renamed).Query(q=>q.MatchAll()));
			Assert.AreNotEqual(count1.Count, count3.Count);
			Assert.False(count3.IsValid);
		}
	}
}