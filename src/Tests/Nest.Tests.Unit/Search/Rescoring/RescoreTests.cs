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
using Nest.Resolvers;

namespace Nest.Tests.Unit.Search.Rescoring
{
	[TestFixture]
	public class RescoreTests : BaseJsonTests
	{
		[Test]
		public void RescoreSerializes()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.MatchAll()
				.Rescore(r=>r
					.WindowSize(500)
					.RescoreQuery(rq=>rq
						.QueryWeight(1.0)
						.RescoreQueryWeight(2.0)
						.ScoreMode(ScoreMode.Multiply)
						.Query(q=>q.Term(p=>p.Name, "nest"))
					)
				);
			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}

	}
}
