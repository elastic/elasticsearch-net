using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Integration.Core.Map.RoutingField
{
	[TestFixture]
	public class RoutingFieldTests : BaseMappingTests
	{
		[Test]
		public void RoutingFieldUsingExpression()
		{
			var result = this.Client.Map<ElasticsearchProject>(m => m
				.RoutingField(a => a
					.Path(p => p.Name)
					.Required()
				)
			);
			this.DefaultResponseAssertations(result);
		}
		[Test]
		public void RoutingFieldUsingString()
		{
			var result = this.Client.Map<ElasticsearchProject>(m => m
				.RoutingField(a => a
					.Path("my_difficult_field_name")
					.Required()
				)
			);
			this.DefaultResponseAssertations(result);
		}
	}
}
