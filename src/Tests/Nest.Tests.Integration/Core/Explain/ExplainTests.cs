using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using Elasticsearch.Net;
using FluentAssertions;
using Nest.Resolvers;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Core.Explain
{
	[TestFixture]
	public class ExplainTests : IntegrationTests
	{
		[Test]
		public void ExplainOnDocumentWithSource()
		{
			var elasticSearchProject = this.Client.Source<ElasticsearchProject>(4);
			
			Assert.NotNull(elasticSearchProject);
			Assert.IsNotNullOrEmpty(elasticSearchProject.Name);

			var explainResponse = this.Client.Explain<ElasticsearchProject>(e => e
				.IdFrom(elasticSearchProject)
				.Query(q=>q.Term(p=> p.Name.Suffix("sort"), elasticSearchProject.Name))
				.SourceEnabled()
			);

			explainResponse.IsValid.Should().BeTrue();
			explainResponse.Matched.Should().BeTrue();

			explainResponse.Explanation.Should().NotBeNull();
			explainResponse.Explanation.Value.Should().BeGreaterOrEqualTo(0.0f);
			explainResponse.Explanation.Description.Should().NotBeEmpty();
			explainResponse.Explanation.Details.Should().NotBeEmpty().And.NotContain(d=>d.Description.IsNullOrEmpty());

			explainResponse.Get.Should().NotBeNull();
			explainResponse.Get.Found.Should().BeTrue();
			explainResponse.Get.Source.Should().NotBeNull();
			explainResponse.Get.Source.Name.ShouldAllBeEquivalentTo(elasticSearchProject.Name);
			

			explainResponse.Source.Name.ShouldAllBeEquivalentTo(elasticSearchProject.Name);

		}
		
		[Test]
		public void ExplainOnDocumentWithFields()
		{
			var elasticSearchProject = this.Client.Source<ElasticsearchProject>(4);
			
			Assert.NotNull(elasticSearchProject);
			Assert.IsNotNullOrEmpty(elasticSearchProject.Name);

			var explainResponse = this.Client.Explain<ElasticsearchProject>(new ExplainRequest<ElasticsearchProject>(elasticSearchProject)
			{
				Fields = new List<PropertyPathMarker>
				{
					{ Property.Path<ElasticsearchProject>(p=>p.Name) }
				},
				Query = new QueryContainer(new TermQuery
				{
					Field = "name.sort",
					Value = elasticSearchProject.Name
				})
				
			});

			explainResponse.IsValid.Should().BeTrue();
			explainResponse.Matched.Should().BeTrue();

			explainResponse.Explanation.Should().NotBeNull();
			explainResponse.Explanation.Value.Should().BeGreaterOrEqualTo(0.0f);
			explainResponse.Explanation.Description.Should().NotBeEmpty();
			explainResponse.Explanation.Details.Should().NotBeEmpty().And.NotContain(d=>d.Description.IsNullOrEmpty());

			explainResponse.Get.Should().NotBeNull();
			explainResponse.Get.Found.Should().BeTrue();

			var name = explainResponse.Fields.FieldValues(p => p.Name).FirstOrDefault();
			name.ShouldAllBeEquivalentTo(elasticSearchProject.Name);

		}
		
	}
}
