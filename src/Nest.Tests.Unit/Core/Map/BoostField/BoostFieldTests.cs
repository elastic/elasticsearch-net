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
			var result = this._client.MapFluent<ElasticSearchProject>(m => m
				.BoostField(a => a
					.SetName(p => p.Name)
					.SetNullValue(1.0)
				)
			);
			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod()); 
		}
		[Test]
		public void BoostFieldUsingString()
		{
			var result = this._client.MapFluent<ElasticSearchProject>(m => m
				.BoostField(a => a
					.SetName("my_difficult_field_name")
					.SetNullValue(0.9)
				)
			);
			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod());
		}
	}
}
