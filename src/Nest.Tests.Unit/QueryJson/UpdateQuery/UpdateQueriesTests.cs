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

namespace Nest.Tests.Unit.QueryJson.UpdateQuery
{
	[TestFixture]
	public class UpdateQueriesTests : BaseJsonTests
	{
		[Test]
		public void FilteredQueryCombines()
		{
			var s = new RoutingQueryPathDescriptor<ElasticSearchProject>()
			  .AllIndices()
			  .AllTypes()
			  .Filtered(fq =>
				  fq.Filter(ff =>
					ff.Term(f => f.Name, "foo")
					|| ff.Term(f => f.Name, "bar")
				  )
			   );

			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}
		[Test]
		public void FilteredQueryCombinesUsingStatic()
		{
			var s = new RoutingQueryPathDescriptor<ElasticSearchProject>()
			  .AllIndices()
			  .AllTypes()
			  .Filtered(fq =>
				  fq.Filter(ff =>
					Filter<ElasticSearchProject>.Term(f => f.Name, "foo")
					|| Filter<ElasticSearchProject>.Term(f => f.Name, "bar")
				  )
			   );

			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}
	}
}
