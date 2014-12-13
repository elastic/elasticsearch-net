using System.Linq;
using Nest.Tests.MockData;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;
using Elasticsearch.Net;

namespace Nest.Tests.Integration.Search.ScriptFields
{
	[TestFixture]
	public class ScriptFieldsTest : IntegrationTests
	{
		private string _LookFor = NestTestData.Data.First().Followers.First().FirstName;

		[Test]
		public void SimpleExplain()
		{
			var queryResults = this.Client.Search<ElasticsearchProject>(s=>s
				.From(0)
				.Size(10)
				.MatchAll()
				.Fields(f=>f.Name)
				.ScriptFields(sf=>sf
					.Add("locscriptfield", sff=>sff
						.Script("doc['loc'].value * multiplier")
						.Params(sp=>sp
							.Add("multiplier", 4)
						)
					)
				)
			);
			Assert.True(queryResults.IsValid);
			Assert.True(queryResults.Hits.Any());
			Assert.True(queryResults.Hits.All(h=>h.Fields.FieldValues<int[]>("locscriptfield").HasAny()));
		}

	    [Test]
		[SkipVersion("0 - 1.2.9", "Indexed scripts introduced in ES 1.3")]
	    public void IndexedScript()
	    {
	        var script = "doc['loc'].value * multiplier";
	        var scriptId = "test";
	        var lang = "groovy";

	        var putResult = this.Client.PutScript(s => s.Lang(lang).Id(scriptId).Script(script));

            var queryResults = this.Client.Search<ElasticsearchProject>(s => s
                .From(0)
                .Size(10)
                .MatchAll()
                .Fields(f => f.Name)
                .ScriptFields(sf => sf
                    .Add("indexedscriptfield", sff => sff
                        .ScriptId(scriptId)
                        .Lang(lang)
                        .Params(sp => sp
                            .Add("multiplier", 4)
                        )
                    )
                )
            );

            Assert.True(queryResults.IsValid);
            Assert.True(queryResults.Hits.Any());
            Assert.True(queryResults.Hits.All(h => h.Fields.FieldValues<int[]>("indexedscriptfield").HasAny()));
	    }
	}
}