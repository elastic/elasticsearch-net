
using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.Tests.Integration.Search.Query
{
	[TestFixture]
	public class TemplateQueryTests : IntegrationTests
	{
		[Test]
		[SkipVersion("0 - 1.0.9", "Query template added in 1.1")]
		public void TemplateQuery()
		{
			var r = this.Client.Search<ElasticsearchProject>(s => s
				.Query(q => q
					.Template(t => t
						.Query("{ \"match\": { \"{{my_field}}\": { \"query\": \"{{my_value}}\" } } }")
						.Params(p => p
							.Add("my_field", "name")
							.Add("my_value", "em-elasticsearch")
						)
					)
				)
			);

			r.IsValid.Should().BeTrue();
			r.Hits.Count().Should().BeGreaterThan(0);
		}
	}
}
