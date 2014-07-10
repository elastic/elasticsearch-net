using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Integration.Core.Map.TtlField
{
	[TestFixture]
	public class TtlFieldTests : BaseMappingTests
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
			this.DefaultResponseAssertations(result);
		}
	}
}
