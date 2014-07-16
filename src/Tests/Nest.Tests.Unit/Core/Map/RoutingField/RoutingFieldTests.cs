using NUnit.Framework;
using Nest.Tests.MockData.Domain;
using System.Reflection;

namespace Nest.Tests.Unit.Core.Map.RoutingField
{
	[TestFixture]
	public class RoutingFieldTests : BaseJsonTests
	{
		[Test]
		public void RoutingFieldUsingExpression()
		{
			var result = this._client.Map<ElasticsearchProject>(m => m
				.RoutingField(a => a
					.Path(p => p.Name)
					.Required()
				)
			);
			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod()); 
		}
		[Test]
		public void RoutingFieldUsingString()
		{
			var result = this._client.Map<ElasticsearchProject>(m => m
				.RoutingField(a => a
					.Path("my_difficult_field_name")
					.Required()
				)
			);
			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod());
		}
	}
}
