using NUnit.Framework;
using Nest.Tests.MockData.Domain;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;

namespace Nest.Tests.Integration.Core
{
	[TestFixture]
	public class ValidateIntegrationTests : IntegrationTests
	{
		[Test]
		public void TestValidation()
		{
			var response = this.Client.Validate<ElasticsearchProject>(v=>v
				.Query(q=>q
					.Term(f=>f.Country, "netherlands")
				)
			);
			response.Should().NotBeNull();
			response.IsValid.Should().BeTrue();
			response.Valid.Should().BeTrue();			
		}
		[Test]
		public void TestValidationWithExplain_Invalid()
		{
			var response = this.Client.Validate<ElasticsearchProject>(q => q
				.Explain()
				.Q("loc:asdasd")
			);
			response.Should().NotBeNull();
			response.IsValid.Should().BeTrue();
			response.Valid.Should().BeFalse();
			response.Explanations.Should().NotBeNull().And.NotBeEmpty();
			var explanation = response.Explanations.First();
			explanation.Index.Should().BeEquivalentTo(Settings.DefaultIndex);
			explanation.Valid.Should().BeFalse();
			explanation.Error.Should().NotBeNullOrEmpty();
		}

		[Test]
		public void TestValidationWithExplanation_Valid()
		{
			var response = this.Client.Validate<ElasticsearchProject>(v => v
				.Explain()
				.Q("name:elasticsearch")
			);
			response.Should().NotBeNull();
			response.IsValid.Should().BeTrue();
			response.Explanations.Should().NotBeNull().And.NotBeEmpty();
			var explanation = response.Explanations.First();
			explanation.Index.Should().BeEquivalentTo(Settings.DefaultIndex);
			explanation.Valid.Should().BeTrue();
			explanation.Error.Should().BeNullOrEmpty();
			explanation.Explanation.Should().NotBeNullOrEmpty();
		}
	}
}
