using NUnit.Framework;
using Nest.Tests.MockData.Domain;
using System.Reflection;

namespace Nest.Tests.Unit.Core.MoreLikeThis
{
	[TestFixture]
	public class MltSearchBodyTests : BaseJsonTests
	{
		[Test]
		public void SearchBodyEndsUpInPost()
		{
			var result = this._client.MoreLikeThis<ElasticsearchProject>(mlt => mlt
				.Id(1)
				.MltFields(p => p.Country, p => p.Content)
				.Search(s=>s
					.From(0)
					.Take(20)
					.Filter(f=>f.Term(p=>p.Country, "The Netherlands"))
					.MatchAll()
				)
			);
			var status = result.ConnectionStatus;
			//We are using the search descriptor so this should trigger the POST
			StringAssert.AreEqualIgnoringCase("POST", status.RequestMethod);
			this.JsonEquals(status.Request, MethodInfo.GetCurrentMethod());
		}
	}
}
