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

namespace Nest.Tests.Unit.FilterJson.ConditionLess
{
	[TestFixture]
	public class ConditionLessTests : BaseJsonTests
	{
		public class Criteria
		{
			public string Name1 { get; set; }
			public string Name2 { get; set; }
			public int? Int1 { get; set; }
			public DateTime? Date { get; set; }
		}
		private readonly Criteria _c = new Criteria();

		private void DoConditionlessQuery(Func<FilterDescriptor<ElasticSearchProject>, BaseFilter> filter)
		{
			var criteria = new Criteria { };
			var s = new SearchDescriptor<ElasticSearchProject>()
				.Strict()
				.From(0)
				.Take(10)
				.Filter(filter);

			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod(), "MatchAll");
		}

	
	}
}
