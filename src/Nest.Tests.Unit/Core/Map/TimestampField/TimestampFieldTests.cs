using NUnit.Framework;
using Nest.Tests.MockData.Domain;
using System.Reflection;

namespace Nest.Tests.Unit.Core.Map.TimestampField
{
	[TestFixture]
	public class TimestampFieldTests : BaseJsonTests
	{
		[Test]
		public void TimestampFieldUsingExpression()
		{
			var result = this._client.Map<ElasticSearchProject>(m => m
				.TimestampField(a => a
					.SetPath(p => p.Name)
					.SetDisabled()
				)
			);
			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod()); 
		}
		[Test]
		public void TimestampFieldUsingString()
		{
			var result = this._client.Map<ElasticSearchProject>(m => m
				.TimestampField(a => a
					.SetPath("my_difficult_field_name")
					.SetDisabled(false)
				)
			);
			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod());
		}
	}
}
