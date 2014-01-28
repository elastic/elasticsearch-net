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

namespace Nest.Tests.Unit.Search.SearchType
{
	[TestFixture]
	public class SearchTypeTests : BaseJsonTests
	{
		[Test]
		public void SearchTypeDoesNotPolluteQueryObject()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.MatchAll()
				.SearchType(SearchTypeOptions.QueryAndFetch);
			this.JsonEquals(s, System.Reflection.MethodBase.GetCurrentMethod());
		}
		
	}
}
