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
	public class GetTemplateRequestTests : BaseJsonTests
	{

		[Test]
		public void DefaultPath()
		{
			var result = this._client.GetTemplate("myTestTemplateName");
			Assert.NotNull(result, "GetTemplate result should never be null");
		}
	}
}
