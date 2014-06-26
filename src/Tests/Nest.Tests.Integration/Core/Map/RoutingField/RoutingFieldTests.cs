using NUnit.Framework;
using Nest.Tests.MockData.Domain;
using System.Reflection;

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
					.SetPath(p => p.Name)
					.SetRequired()
				)
			);
			this.DefaultResponseAssertations(result);
		}
		[Test]
		public void RoutingFieldUsingString()
		{
			var result = this.Client.Map<ElasticsearchProject>(m => m
				.RoutingField(a => a
					.SetPath("my_difficult_field_name")
					.SetRequired()
				)
			);
			this.DefaultResponseAssertations(result);
		}
	}
}
