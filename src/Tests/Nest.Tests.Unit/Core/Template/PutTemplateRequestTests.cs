using Elasticsearch.Net;
using NUnit.Framework;

namespace Nest.Tests.Unit.Core.Template
{
	[TestFixture]
	public class PutTemplateRequestTests : BaseJsonTests
	{

		[Test]
		public void DefaultPath()
		{
			var result = this._client.PutTemplate("myTestTemplateName", t=>t.Template("tmasdasda"));
			Assert.NotNull(result, "PutTemplate result should not be null");
			var status = result.ConnectionStatus;
			StringAssert.Contains("USING NEST IN MEMORY CONNECTION", result.ConnectionStatus.ResponseRaw.Utf8String());
			StringAssert.EndsWith("/_template/myTestTemplateName", status.RequestUrl);
			StringAssert.AreEqualIgnoringCase("PUT", status.RequestMethod);
		}
	}
}
