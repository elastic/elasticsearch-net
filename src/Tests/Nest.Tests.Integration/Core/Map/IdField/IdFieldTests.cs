using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Integration.Core.Map.IdField
{
	[TestFixture]
	public class IdFieldTests : BaseMappingTests
	{
		[Test]
		public void IdFieldSerializesFully()
		{
			var result = this.Client.Map<ElasticsearchProject>(m => m
				.IdField(i => i
					.Index("not_analyzed")
					.Path("myOtherId")
					.Store(false)
				)
			);
			this.DefaultResponseAssertations(result);
		}
	}
}
