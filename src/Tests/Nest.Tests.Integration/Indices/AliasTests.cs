using System.Linq;
using System.Text;
using FluentAssertions;
using Nest.Tests.MockData.DataSources;
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

			var r = this._client.Alias(a => a.Add(o => o.Index(index).Alias(alias)));
			Assert.True(r.IsValid);
			Assert.True(r.Acknowledged);
			var count1 = this._client.Count<ElasticsearchProject>(c => c.Index(index).Query(q => q.MatchAll()));
			var count2 = this._client.Count<ElasticsearchProject>(c => c.Index(alias).Query(q => q.MatchAll()));
			Assert.AreEqual(count1.Count, count2.Count);
			r = this._client.Alias(a => a.Remove(o => o.Index(index).Alias(alias)));
			count1 = this._client.Count<ElasticsearchProject>(c => c.Index(index).Query(q => q.MatchAll()));
			count2 = this._client.Count<ElasticsearchProject>(c => c.Index(alias).Query(q => q.MatchAll()));
			Assert.AreNotEqual(count1.Count, count2.Count);
			Assert.False(count2.IsValid);
		}
		[Test]
		public void SimpleRenameAlias()
		{
			var index = ElasticsearchConfiguration.DefaultIndex;
			var alias = ElasticsearchConfiguration.DefaultIndex + "-2";

			var r = this._client.Alias(a => a.Add(o => o.Index(index).Alias(alias)));
			Assert.True(r.IsValid);
			Assert.True(r.Acknowledged);
			var count1 = this._client.Count<ElasticsearchProject>(c => c.Index(index).Query(q => q.MatchAll()));
			var count2 = this._client.Count<ElasticsearchProject>(c => c.Index(alias).Query(q => q.MatchAll()));
			Assert.AreEqual(count1.Count, count2.Count);

			var renamed = index + "-3";

			r = this._client.Alias(a => a
				.Remove(o => o.Index(index).Alias(alias))
				.Add(o => o.Index(index).Alias(renamed))
			);
			count1 = this._client.Count<ElasticsearchProject>(c => c.Index(index).Query(q => q.MatchAll()));
			count2 = this._client.Count<ElasticsearchProject>(c => c.Index(alias).Query(q => q.MatchAll()));
			var count3 = this._client.Count<ElasticsearchProject>(c => c.Index(renamed).Query(q => q.MatchAll()));
			Assert.AreEqual(count1.Count, count3.Count);
			Assert.AreNotEqual(count1.Count, count2.Count);
			Assert.False(count2.IsValid);

			r = this._client.Alias(a => a.Remove(o => o.Index(index).Alias(renamed)));
			count1 = this._client.Count<ElasticsearchProject>(c => c.Index(index).Query(q => q.MatchAll()));
			count3 = this._client.Count<ElasticsearchProject>(c => c.Index(renamed).Query(q => q.MatchAll()));
			Assert.AreNotEqual(count1.Count, count3.Count);
			Assert.False(count3.IsValid);
		}

		[Test]
		[SkipVersion("1.0", "Adding aliases during index creation not introduced until 1.1")]
		public void AddAliasFromCreateIndex()
		{
			var indexName = ElasticsearchConfiguration.NewUniqueIndexName();
			var aliasName = ElasticsearchConfiguration.NewUniqueIndexName();
			var createIndexResponse = _client.CreateIndex(indexName, c => c
				.NumberOfReplicas(0)
				.NumberOfShards(1)
				.AddAlias(aliasName, a => a.IndexRouting("1"))
			);

			createIndexResponse.IsValid.Should().BeTrue();

			var aliasesResponse = _client.GetAliases(a => a.Index(indexName));
			aliasesResponse.IsValid.Should().BeTrue();
			aliasesResponse.Indices[indexName].Count.Should().Be(1);

			var alias = aliasesResponse.Indices[indexName].First();
			alias.Should().NotBeNull();

			alias.Name.Should().Be(aliasName);
			alias.IndexRouting.Should().Be("1");
		}

		[Test]
		public void AliasesPointingToIndex()
		{
			var indexName = ElasticsearchConfiguration.NewUniqueIndexName();

			var createIndexResponse = _client.CreateIndex(indexName, c => c
				.NumberOfReplicas(0)
				.NumberOfShards(1)
			);

			createIndexResponse.IsValid.Should().BeTrue();

			var aliasName1 = ElasticsearchConfiguration.NewUniqueIndexName();
			var aliasName2 = ElasticsearchConfiguration.NewUniqueIndexName();
			var aliasName3 = ElasticsearchConfiguration.NewUniqueIndexName();

			var aliasResponse1 = _client.Alias(a => a
				.Add(aa => aa
					.Index(indexName)
					.Alias(aliasName1)
					.IndexRouting("1")
				)
			);

			aliasResponse1.IsValid.Should().BeTrue();

			var aliasResponse2 = _client.Alias(a => a
				.Add(aa => aa
					.Index(indexName)
					.Alias(aliasName2)
					.IndexRouting("2")
				)
			);

			aliasResponse2.IsValid.Should().BeTrue();

			var aliasResponse3 = _client.Alias(a => a
				.Add(aa => aa
					.Index(indexName)
					.Alias(aliasName3)
					.IndexRouting("3")
				)
			);

			aliasResponse3.IsValid.Should().BeTrue();

			var aliases = _client.GetAliasesPointingToIndex(indexName);
			aliases.Should().NotBeNull().And.HaveCount(3);
		}

		[Test]
		public void IndicesPointingToAlias()
		{
			var aliasName = ElasticsearchConfiguration.NewUniqueIndexName();

			var indexName1 = ElasticsearchConfiguration.NewUniqueIndexName();
			var createIndexResponse1 = _client.CreateIndex(indexName1);

			createIndexResponse1.IsValid.Should().BeTrue();
			
			var aliasResponse1 = _client.Alias(a => a
				.Add(aa => aa
					.Index(indexName1)
					.Alias(aliasName)
					.IndexRouting("1")
				)
			);

			aliasResponse1.IsValid.Should().BeTrue();

			var indexName2 = ElasticsearchConfiguration.NewUniqueIndexName();
			var createIndexResponse2 = _client.CreateIndex(indexName2);

			createIndexResponse2.IsValid.Should().BeTrue();

			var aliasResponse2 = _client.Alias(a => a
				.Add(aa => aa
					.Index(indexName2)
					.Alias(aliasName)
					.IndexRouting("1")
				)
			);

			aliasResponse2.IsValid.Should().BeTrue();

			var indexName3 = ElasticsearchConfiguration.NewUniqueIndexName();
			var createIndexResponse3 = _client.CreateIndex(indexName3);

			createIndexResponse3.IsValid.Should().BeTrue();

			var aliasResponse3 = _client.Alias(a => a
				.Add(aa => aa
					.Index(indexName3)
					.Alias(aliasName)
					.IndexRouting("1")
				)
			);

			aliasResponse3.IsValid.Should().BeTrue();
			
			var indices = _client.GetIndicesPointingToAlias(aliasName);

			indices.Should().NotBeNull().And.HaveCount(3);
			indices.ShouldAllBeEquivalentTo(new[] { indexName1, indexName2, indexName3 });
		}
		[Test]
		public void AliasWithFilterPointingToIndex()
		{
			var indexName = ElasticsearchConfiguration.NewUniqueIndexName();
			var aliasName = ElasticsearchConfiguration.NewUniqueIndexName();
			var createIndexResponse = _client.CreateIndex(indexName, c => c
				.NumberOfReplicas(0)
				.NumberOfShards(1)
			);

			createIndexResponse.IsValid.Should().BeTrue();

			var aliasResponse = _client.Alias(a => a
				.Add(aa => aa
					.Alias(aliasName)
					.IndexRouting("1")
					.Filter<dynamic>(f => f.Term("foo", "bar")
					)
				)
			);

			aliasResponse.IsValid.Should().BeTrue();

			var aliases = _client.GetAliasesPointingToIndex(indexName);
			aliases.Should().NotBeNull().And.HaveCount(1);
			var alias = aliases.First();
			var filter = alias.Filter;
			filter.Should().NotBeNull();
			var term = filter.Term;
			term.Should().NotBeNull();
			term.Field.Should().Be("foo");
			term.Value.Should().Be("bar");
		}
	}
}