using NUnit.Framework;
using Nest.Tests.MockData.Domain;
using System.Reflection;

namespace Nest.Tests.Integration.Core.Map.TypeField
{
	[TestFixture]
	public class TypeFieldTests : BaseMappingTests
	{
		[Test]
		public void TypeFieldSerializesYes()
		{
			var result = this._client.Map<ElasticsearchProject>(m => m
				.TypeField(t => t
					.SetIndexed()
					.SetStored()
				)
			);
			this.DefaultResponseAssertations(result);
		}
		[Test]
		public void TypeFieldSerializesNo()
		{
			var result = this._client.Map<ElasticsearchProject>(m => m
				.TypeField(t => t
					.SetIndexed(NonStringIndexOption.no)
					.SetStored(false)
				)
			);
			this.DefaultResponseAssertations(result);
		}
	}
}
