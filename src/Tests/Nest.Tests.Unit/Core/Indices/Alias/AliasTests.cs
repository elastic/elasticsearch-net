using Nest.Tests.MockData.Domain;
using NUnit.Framework;
using System.Reflection;

namespace Nest.Tests.Unit.Core.Indices.Alias
{
	[TestFixture]
	public class AliasTests : BaseJsonTests
	{
		[Test]
		public void PutAlias()
		{
			var result = this._client.PutAlias(a => a
				.Name("my-alias")
				.Index("my-index")
				.Routing("12")
				.Filter<ElasticsearchProject>(f => f
					.Term(t => t.Name, "nest")
				)
			);

			Assert.IsTrue(result.ConnectionStatus.RequestUrl.EndsWith("/my-index/_alias/my-alias"));
			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod());
		}
	}
}
