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
			var result = this.Client.Map<ElasticsearchProject>(m => m
				.BoostField(a => a
					.Name(p => p.Name)
					.NullValue(1.0)
				)
			);
			this.DefaultResponseAssertations(result);
		}
		[Test]
		public void BoostFieldUsingString()
		{
			var result = this.Client.Map<ElasticsearchProject>(m => m
				.BoostField(a => a
					.Name("my_difficult_field_name")
					.NullValue(0.9)
				)
			);
			this.DefaultResponseAssertations(result);
		}
	}
}
