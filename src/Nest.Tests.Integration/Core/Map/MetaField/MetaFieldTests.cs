using NUnit.Framework;
using Nest.Tests.MockData.Domain;
using System.Reflection;

namespace Nest.Tests.Integration.Core.Map.MetaField
{
	[TestFixture]
	public class MetaFieldTests : BaseMappingTests
	{
		[Test]
		public void MetaFieldSerializes()
		{
			var result = this._client.MapFluent<ElasticSearchProject>(m => m
				.Meta(d => d
					.Add("attr1", "value1")
					.Add("attr2", new { attr3 = "value3" })
					.Add("Attr4", 10)
				)
			);
			//make sure Attr4 is not serialized as attr4
			StringAssert.DoesNotContain("attr4", result.ConnectionStatus.Request, result.ConnectionStatus.Request);
			this.DefaultResponseAssertations(result);
		}
	}
}
