using NUnit.Framework;
using Nest.Tests.MockData.Domain;
using System.Reflection;

namespace Nest.Tests.Unit.Core.Map.IdField
{
	[TestFixture]
	public class IdFieldTests : BaseJsonTests
	{
		[Test]
		public void IdFieldSerializesFully()
		{
			var result = this._client.Map<ElasticsearchProject>(m => m
				.IdField(i => i
					.Index("not_analyzed")
					.Path("myOtherId")
					.Store(false)
				)
			);
			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod()); 
		}
	}
}
