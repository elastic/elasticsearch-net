using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Nest.Tests.MockData;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;
using System.Diagnostics;
using FluentAssertions;

namespace Nest.Tests.Integration.Reproduce
{
	[TestFixture]
	public class Reproduce628Tests : IntegrationTests
	{
		public class Post
		{
			public int Id { get; set; }
			public string Name { get; set; }
		}


		/// <summary>
		/// https://github.com/Mpdreamz/NEST/issues/682
		/// </summary>
		[Test]
		public void AliasesWithDashesAreNotStripped()
		{
			//unique indexaname already contains dasshes but lets be sure about this
			//if the implementation ever changes
			var index = ElasticsearchConfiguration.NewUniqueIndexName() + "-dashes";
			var x = this._client.CreateIndex(index);
			x.Acknowledged.Should().BeTrue();
			var alias = ElasticsearchConfiguration.NewUniqueIndexName() + "-dashed-alias";
			var aliasResult = this._client.Alias(a => a.Add(aa => aa.Index(index).Alias(alias)));
			aliasResult.IsValid.Should().BeTrue();
			aliasResult.Acknowledged.Should().BeTrue();

			var getIndicesResult = _client.GetIndicesPointingToAlias(alias);
			getIndicesResult.Should().NotBeEmpty();

			var indexReturned = getIndicesResult.First();
			indexReturned.Should().Be(index).And.Contain("-dashes");


			var getAliasesResult = this._client.GetAliasesPointingToIndex(index);
			getAliasesResult.Should().NotBeEmpty().And.HaveCount(1);

			var aliasReturned = getAliasesResult.First().Name;
			aliasReturned.Should().Be(alias).And.Contain("-dashed-alias");


			var elasticsearchClient = new ElasticsearchClient(ElasticsearchConfiguration.Settings());
			var dynamicResult = elasticsearchClient.IndicesGetAlias(index);
			IDictionary<string, object> aliases = dynamicResult.Response[index].aliases;
			aliases.Count.Should().Be(1);
			var aliasDynamic = aliases.Keys.First();
			aliasDynamic.Should().Be(alias);

		}

        [Test]
        public void IndexWithDashesAreNotStripped()
        {
            var index = ElasticsearchConfiguration.NewUniqueIndexName() + "-dashes";
            var x = this._client.CreateIndex(index);
            x.Acknowledged.Should().BeTrue();
            var alias = ElasticsearchConfiguration.NewUniqueIndexName() + "-dashes-alias";
            var aliasResult = this._client.Alias(a => a.Add(aa => aa.Index(index).Alias(alias)));
            aliasResult.IsValid.Should().BeTrue();
            aliasResult.Acknowledged.Should().BeTrue();

            var elasticsearchClient = new ElasticsearchClient(ElasticsearchConfiguration.Settings());
            var dynamicResult = elasticsearchClient.IndicesGetAliasForAll(alias);
            dynamicResult.Response.ContainsKey(index).Should().BeTrue();
        }

        [Test]
        public void IndexWithDashesAreNotStripped2()
        {
            var index = ElasticsearchConfiguration.NewUniqueIndexName() + "-dashes";
            var x = this._client.CreateIndex(index);
            x.Acknowledged.Should().BeTrue();
            var alias = ElasticsearchConfiguration.NewUniqueIndexName() + "-dashes-alias";
            var aliasResult = this._client.Alias(a => a.Add(aa => aa.Index(index).Alias(alias)));
            aliasResult.IsValid.Should().BeTrue();
            aliasResult.Acknowledged.Should().BeTrue();

            var elasticsearchClient = new ElasticsearchClient(ElasticsearchConfiguration.Settings());
            var dynamicResult = elasticsearchClient.IndicesGetAlias(alias);
            dynamicResult.Response.ContainsKey(index).Should().BeTrue();
        }


	}
}
