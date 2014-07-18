using NUnit.Framework;
using Nest.Tests.MockData.Domain;
using System.Reflection;

namespace Nest.Tests.Unit.Core.Map.BoostField
{
	[TestFixture]
	public class BoostFieldTests : BaseJsonTests
	{
		[Test]
		public void BoostFieldUsingExpression()
		{
			var result = this._client.Map<ElasticsearchProject>(m => m
				.BoostField(a => a
					.Name(p => p.Name)
					.NullValue(1.0)
				)
			);
			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod()); 
		}
		[Test]
		public void BoostFieldUsingString()
		{
			var result = this._client.Map<ElasticsearchProject>(m => m
				.BoostField(a => a
					.Name("my_difficult_field_name")
					.NullValue(0.9)
				)
			);
			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod());
		}
	}
}
