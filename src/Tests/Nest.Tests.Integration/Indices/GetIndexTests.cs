using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Indices
{
	[TestFixture]
	public class GetIndexTests : IntegrationTests
	{
		[Test]
		[SkipVersion("0 - 1.3.9", "Get Index API is a 1.4 feature")]
		public void GetSingleIndex()
		{
			var indexName = ElasticsearchConfiguration.NewUniqueIndexName();
			var aliasName = ElasticsearchConfiguration.NewUniqueIndexName();
			var warmerName = "get_comfy";
			var create = this.Client.CreateIndex(s => s
				.Index(indexName)
				.AddAlias(aliasName, a=>a
					.IndexRouting("routing")
					.SearchRouting("routing")
					.Filter<object>(f=>f.Term("country", "value"))
				)
				.AddMapping<ElasticsearchProject>(map=>map
					.MapFromAttributes()
				)
				.Similarity(sim=>sim
					.CustomSimilarities(cs => cs
						.Add("my_bm25_similarity", new BM25Similarity
						{
							K1 = 2.0,
							B = 0.75,
							Normalization = "h1",
							NormalizationH1C = "1.0",
							DiscountOverlaps = true
						})
					)
				)
				.Analysis(a=>a
					.Tokenizers(p=>p
						.Add("myTokenizer", new KeywordTokenizer())
					)
					.TokenFilters(tf=>tf
						.Add("myTokenFilter1", new AsciiFoldingTokenFilter())
						.Add("myTokenFilter2", new UniqueTokenFilter())
					)
					.CharFilters(cf=>cf
						.Add("my_html", new HtmlStripCharFilter())
					)
					.Analyzers(aa => aa.Add("myCustom", new CustomAnalyzer
					{
						Tokenizer = "myTokenizer",
						Filter = new string[] { "myTokenFilter1", "myTokenFilter2" },
						CharFilter = new string[] { "my_html" },
						Alias = new string[] { "alias1", "alias2" }
					}))
				)
				.AddWarmer(cw=>cw
					.WarmerName(warmerName)
					.Type<ElasticsearchProject>()
					.Search(search=>search.Query(q=>q.Term("field", "value")))
				)
				.Settings(settings=>settings
					.Add("somesetting", 1)
				)
			);
			create.IsValid.Should().BeTrue();

			var r = this.Client.GetIndex(f=>f.Index(indexName));
			r.IsValid.Should().BeTrue();

			r.Indices.Should().NotBeEmpty().And.ContainKey(indexName);

			var index = r.Indices[indexName];
			index.Aliases.Should().NotBeEmpty().And.ContainKey(aliasName);
			index.Warmers.Should().NotBeEmpty().And.ContainKey(warmerName);
			index.Mappings.Should().NotBeEmpty().And.ContainSingle(p=>p.Name.EqualsString("elasticsearchprojects"));
			index.Settings.Should().NotBeEmpty().And.ContainKey("somesetting");
			index.Analysis.Should().NotBeNull();
			index.Similarity.Should().NotBeNull();

			var alias = index.Aliases[aliasName];
			alias.IndexRouting.Should().Be("routing");
			alias.SearchRouting.Should().Be("routing");
			IQueryContainer filter = alias.Filter;
			filter.Should().NotBeNull();
			filter.Term.Should().NotBeNull();
			filter.Term.Field.Should().Be("country");
			filter.Term.Value.Should().Be("value");

			var warmer = index.Warmers[warmerName];
			warmer.Should().NotBeNull();
			warmer.Types.ShouldBeEquivalentTo(new TypeNameMarker[] { "elasticsearchprojects" });
			warmer.Source.Should().NotBeNull();
			warmer.Source.Query.Should().NotBeNull();
			warmer.Source.Query.Term.Should().NotBeNull();
			warmer.Source.Query.Term.Field.Should().Be("field");
			warmer.Source.Query.Term.Value.Should().Be("value");
			warmer.Name.Should().Be(warmerName);

			var mapping = index.Mappings.FirstOrDefault(m=>m.Name.EqualsString("elasticsearchprojects"));
			mapping.Properties.Should().NotBeEmpty();

			var someSetting = index.Settings["somesetting"];
			someSetting.Should().NotBeNull();
			someSetting.Should().Be("1");

			string uuid = index.AsExpando.uuid;
			string created = index.AsExpando.version.created;
			uuid.Should().NotBeNullOrWhiteSpace();
			created.Should().NotBeNullOrWhiteSpace();

			index.Analysis.Analyzers.Should().NotBeEmpty().And.ContainKey("myCustom");
			var analyzer = index.Analysis.Analyzers["myCustom"] as CustomAnalyzer;
			analyzer.Should().NotBeNull();
			analyzer.Tokenizer.Should().Be("myTokenizer");

			index.Similarity.CustomSimilarities.Should().NotBeEmpty().And.ContainKey("my_bm25_similarity");
			var similarity = index.Similarity.CustomSimilarities["my_bm25_similarity"] as BM25Similarity;
			similarity.Should().NotBeNull();



		}
		
	}
}