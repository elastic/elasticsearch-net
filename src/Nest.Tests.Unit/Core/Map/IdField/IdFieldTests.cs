using System;
using System.Linq;
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
			var result = this._client.Map<ElasticSearchProject>(m => m
				.IdField(i => i
					.SetIndex("not_analyzed")
					.SetPath("myOtherId")
					.SetStored(false)
				)
			);
			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod()); 
		}
	}
}
