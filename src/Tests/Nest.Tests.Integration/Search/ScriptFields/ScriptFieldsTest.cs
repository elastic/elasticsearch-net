using System.Linq;
using Nest.Tests.MockData;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Search.ScriptFields
{
	[TestFixture]
	public class ScriptFieldsTest : IntegrationTests
	{
		private string _LookFor = NestTestData.Data.First().Followers.First().FirstName;

		[Test]
		public void SimpleExplain()
		{
			var queryResults = this._client.Search<ElasticsearchProject>(s=>s
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
			Assert.True(queryResults.Hits.All(h=>h.Fields.FieldValue<int[]>("locscriptfield").HasAny()));
		}
	}
}