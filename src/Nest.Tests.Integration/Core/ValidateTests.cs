using NUnit.Framework;
using Nest.Tests.MockData.Domain;
using System.Linq;

namespace Nest.Tests.Integration.Core
{
	[TestFixture]
	public class ValidateIntegrationTests : IntegrationTests
	{
		[Test]
		public void TestValidation()
		{
			var response = this._client.Validate<ElasticSearchProject>(v=>v
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
			var response = this._client.Validate<ElasticSearchProject>(q => q
				.Explain()
				.Q("loc:asdasd")
			);
			Assert.NotNull(response);
			Assert.True(response.IsValid);
			Assert.False(response.Valid);
			Assert.NotNull(response.Explanations);
			Assert.True(response.Explanations.HasAny());
			var explanation = response.Explanations.First();
			Assert.AreEqual(explanation.Index, _settings.DefaultIndex);
			Assert.False(explanation.Valid);
			Assert.False(explanation.Error.IsNullOrEmpty());
		}
	}
}
