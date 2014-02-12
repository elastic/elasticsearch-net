using NUnit.Framework;
using Nest.Tests.MockData.Domain;
using System.Reflection;

namespace Nest.Tests.Unit.Core.Map.TtlField
{
	[TestFixture]
	public class TtlFieldTests : BaseJsonTests
	{
		[Test]
		public void TtlFieldSerializes()
		{
			var result = this._client.Map<ElasticsearchProject>(m => m
				.TtlField(t => t
					.SetDisabled(false)
					.SetDefault("1d")
				)
			);
			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod()); 
		}
	}
}
