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

namespace Nest.Tests.Unit.Search.ScriptFields
{
	[TestFixture]
	public class ScriptFieldTests : BaseJsonTests
	{
		[Test]
		public void TestScriptFields()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.ScriptFields(sf=>sf
					.Add("test1", sff=>sff
						.Script("doc['loc'].value * multiplier")
						.Params(sp=>sp
							.Add("multiplier", 4)
						)
					)
				);

			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}
	}
}
