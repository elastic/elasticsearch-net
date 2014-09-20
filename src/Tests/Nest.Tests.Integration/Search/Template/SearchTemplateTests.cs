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
		private string _template = "{\"from\": \"{{my_from}}\",\"size\": \"{{my_size}}\",\"query\": { \"match\": {\"{{my_field}}\": {\"query\": \"{{my_value}}\" }}}}";

		[Test]
		public void SearchTemplateByQuery()
		{
			var result = this.Client.SearchTemplate<ElasticsearchProject>(s => s
				.Template(_template)
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

		[Test]
		public void SearchTemplateByQuery_ObjectInitializer()
		{
			var request = new SearchTemplateRequest
			{
				Template = _template,
				Params = new Dictionary<string, object>
				{
					{ "my_from", 0 },
					{ "my_size", 5 },
					{ "my_field", "name" },
					{ "my_value", "em-elasticsearch" }
				}
			};

			var result = this.Client.SearchTemplate<ElasticsearchProject>(request);

			result.IsValid.Should().BeTrue();
			result.Hits.Count().Should().BeGreaterThan(0);
		}

		public void SearchTemplateById()
		{
			var templateName = "myIndexedTemplate";

			var putTemplateResult = this.Client.PutSearchTemplate(t => t
				.Name(templateName)
				.Template(_template)
			);
			putTemplateResult.IsValid.Should().BeTrue();

			var result = this.Client.SearchTemplate<ElasticsearchProject>(s => s
				.Id(templateName)
				.Params(p => p
					.Add("my_from", 0)
					.Add("my_size", 5)
					.Add("my_field", "name")
					.Add("my_value", "em-elasticsearch")
				)
			);

			result.IsValid.Should().BeTrue();
			result.Hits.Count().Should().BeGreaterThan(0);

			var deleteTemplateResult = this.Client.DeleteSearchTemplate(t => t.Name(templateName));
			deleteTemplateResult.IsValid.Should().BeTrue();
		}

		[Test]
		public void PutGetAndDeleteTemplate()
		{
			var templateName = "myIndexedTemplate";

			var putResult = this.Client.PutSearchTemplate(t => t
					.Name(templateName)
					.Template(_template)
				);
			putResult.IsValid.Should().BeTrue();

			var getResult = this.Client.GetSearchTemplate(t => t.Name(templateName));
			getResult.IsValid.Should().BeTrue();
			getResult.Template.ShouldBeEquivalentTo(_template);
			
			var deleteResult = this.Client.DeleteSearchTemplate(t => t.Name(templateName));
			deleteResult.IsValid.Should().BeTrue();
		}
	}
}

