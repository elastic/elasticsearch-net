using NUnit.Framework;
using Nest.Tests.MockData.Domain;
using System.Reflection;

namespace Nest.Tests.Unit.Core.Map.MetaField
{
	[TestFixture]
	public class MetaFieldTests : BaseJsonTests
	{
		[Test]
		public void MetaFieldSerializes()
		{
			var result = this._client.MapFluent<ElasticSearchProject>(m => m
				.Meta(d => d
					.Add("attr1", "value1")
					.Add("attr2", new { attr3 = "value3" })
					.Add("attr4", 10)
				)
			);
			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod()); 
		}
	}
}
