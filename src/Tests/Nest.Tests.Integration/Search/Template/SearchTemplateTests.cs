using FluentAssertions;
using Nest.Tests.MockData.Domain;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.Tests.Integration.Search.Template
{
	[TestFixture]
	public class SearchTemplateTests : IntegrationTests
	{
		[Test]
		public void SearchTemplateByQuery()
		{
			var template = "{\"from\": \"{{my_from}}\",\"size\": \"{{my_size}}\",\"query\": { \"match\": {\"{{my_field}}\": {\"query\": \"{{my_value}}\" }}}}";

			var result = this.Client.SearchTemplate<ElasticsearchProject>(s => s
				.Template(template)
				.Params(p => p
					.Add("my_from", 0)
					.Add("my_size", 5)
					.Add("my_field", "name")
					.Add("my_value", "em-elasticsearch")
				)
			);

			result.IsValid.Should().BeTrue();
			result.Hits.Count().Should().BeGreaterThan(0);
		}
	}
}

