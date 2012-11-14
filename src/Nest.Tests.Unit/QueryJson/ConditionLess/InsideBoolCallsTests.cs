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

namespace Nest.Tests.Unit.QueryJson.ConditionLess
{
	[TestFixture]
	public class ConditionLessTests : BaseJsonTests
	{
		public class Criteria
		{
			public string Name1 { get; set; }
			public string Name2 { get; set; }
		}

		[Test]
		public void NullTermShouldBeMatchAll()
		{
			var criteria = new Criteria { };
			var s = new SearchDescriptor<ElasticSearchProject>()
			 .From(0)
			 .Take(10)
			 .Query(q => q.Term(f => f.Name, criteria.Name1));

			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}


	}
}
