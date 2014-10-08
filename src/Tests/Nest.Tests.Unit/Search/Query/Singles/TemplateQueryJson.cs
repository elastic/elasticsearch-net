using Nest.Tests.MockData.Domain;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.Tests.Unit.Search.Query.Singles
{
	[TestFixture]
	public class TemplateQueryJson : BaseJsonTests
	{
		[Test]
		public void TemplateQuery()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.Template(t => t
						.Query("match_{{template}}")
						.Params(p => p
							.Add("template", "all")
						)
					)
			);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10,
				  query: {
					template: {
					  query: ""match_{{template}}"",
					  ""params"": {
						template: ""all""
					  }
					}
				  }
				}";

			Assert.True(json.JsonEquals(expected), json);
		}

		[Test]
		public void TemplateQueryWithEscapeString()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.Template(t => t
						.Query("\"match_{{template}}\": {}}\"")
						.Params(p => p
							.Add("template", "all")
						)
					)
			);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10,
				  query: {
					template: {
					  query: ""\""match_{{template}}\"": {}}\"""",
					  ""params"": {
						template: ""all""
					  }
					}
				  }
				}";

			Assert.True(json.JsonEquals(expected), json);
		}
	}
}
