using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Integration.Core.Map.BoostField
{
	[TestFixture]
	public class BoostFieldTests : BaseMappingTests
	{
		[Test]
		public void BoostFieldUsingExpression()
		{
			var result = this._client.Map<ElasticsearchProject>(m => m
				.BoostField(a => a
					.SetName(p => p.Name)
					.SetNullValue(1.0)
				)
			);
			this.DefaultResponseAssertations(result);
		}
		[Test]
		public void BoostFieldUsingString()
		{
			var result = this._client.Map<ElasticsearchProject>(m => m
				.BoostField(a => a
					.SetName("my_difficult_field_name")
					.SetNullValue(0.9)
				)
			);
			this.DefaultResponseAssertations(result);
		}
	}
}
