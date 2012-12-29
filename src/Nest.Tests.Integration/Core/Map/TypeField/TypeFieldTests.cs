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
			var result = this._client.MapFluent<ElasticSearchProject>(m => m
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
			var result = this._client.MapFluent<ElasticSearchProject>(m => m
				.TypeField(t => t
					.SetIndexed(false)
					.SetStored(false)
				)
			);
			this.DefaultResponseAssertations(result);
		}
	}
}
