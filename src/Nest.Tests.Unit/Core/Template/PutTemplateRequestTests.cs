using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Nest;
using Newtonsoft.Json.Converters;
using Nest.Resolvers.Converters;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Core.Template
{
	[TestFixture]
	public class PutTemplateRequestTests : BaseJsonTests
	{

		[Test]
		public void DefaultPath()
		{
			var templateMapping = new TemplateMapping { Template = "whatever" };

			var result = this._client.PutTemplate("myTestTemplateName", templateMapping);
			Assert.NotNull(result, "PutTemplate result should not be null");
			var status = result.ConnectionStatus;
			StringAssert.Contains("USING NEST IN MEMORY CONNECTION", result.ConnectionStatus.Result);
			StringAssert.EndsWith("/_template/myTestTemplateName", status.RequestUrl);
			StringAssert.AreEqualIgnoringCase("PUT", status.RequestMethod);
		}
	}
}
