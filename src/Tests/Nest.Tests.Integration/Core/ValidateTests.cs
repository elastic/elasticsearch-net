using NUnit.Framework;
using Nest.Tests.MockData.Domain;
using System.Linq;
using Elasticsearch.Net;

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
			Assert.NotNull(response);
			Assert.True(response.IsValid);
			Assert.True(response.Valid);
		}
		[Test]
		public void TestValidationWithExplain()
		{
			var response = this.Client.Validate<ElasticsearchProject>(q => q
				.Explain()
				.Q("loc:asdasd")
			);
			Assert.NotNull(response);
			Assert.True(response.IsValid);
			Assert.False(response.Valid);
			Assert.NotNull(response.Explanations);
			Assert.True(response.Explanations.HasAny());
			var explanation = response.Explanations.First();
			Assert.AreEqual(explanation.Index, Settings.DefaultIndex);
			Assert.False(explanation.Valid);
			Assert.False(explanation.Error.IsNullOrEmpty());
		}
	}
}
