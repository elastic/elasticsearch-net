using System;
using System.Linq;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;
using System.Reflection;

namespace Nest.Tests.Unit.Core.Map.TypeField
{
	[TestFixture]
	public class TypeFieldTests : BaseJsonTests
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
			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod()); 
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
			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod());
		}
	}
}
