using NUnit.Framework;
using Nest.Tests.MockData.Domain;
using System.Linq;

namespace Nest.Tests.Integration.Core
{
	[TestFixture]
	public class ValidateIntegrationTests : BaseElasticSearchTests
	{
		[Test]
		public void TestValidation()
		{
			this.ResetIndexes();
			var response = this._client.Validate<ElasticSearchProject>(q=>q
				.Term(f=>f.Country, "netherlands")
			);
			Assert.NotNull(response);
			Assert.True(response.IsValid);
			Assert.True(response.Valid);
		}
		[Test]
		public void TestValidationWithExplain()
		{
			this.ResetIndexes();
			var response = this._client.Validate<ElasticSearchProject>(q => q
				.Explain()
				.UseSimpleQueryString("loc:asdasd")
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
