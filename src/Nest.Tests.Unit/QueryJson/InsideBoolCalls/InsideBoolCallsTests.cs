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

namespace Nest.Tests.Unit.QueryJson.InsideBoolCalls
{
	[TestFixture]
	public class InsideBoolCallsTests : BaseJsonTests
	{
		[Test]
		public void InsideMust()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
			 .From(0)
			 .Take(10)
			 .Query(q => q
			   .Bool(csq => csq
				 .Must(
				   mf => mf.Term(f => f.Name, "foo") || mf.Term(f => f.Name, "bar")
				   , mf => mf.Term(f => f.Name, "foo2") || mf.Term(f => f.Name, "bar2")
				 )
			   )
			 );

			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}

		[Test]
		public void InsideShould()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
			  .From(0)
			  .Take(10)
			  .Query(q => q
				.Bool(csq => csq
				  .Should(
					sf => sf.Term(f => f.Name, "foo") || sf.Term(f => f.Name, "bar")
					, sf => sf.Term(f => f.Name, "foo2") || sf.Term(f => f.Name, "bar2")
				  )
				)
			  );

			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}

		[Test]
		public void InsideMustNot()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
			  .From(0)
			  .Take(10)
			  .Query(q => q
				.Bool(csq => csq
				  .MustNot(
					mf => mf.Term(f => f.Name, "foo") || mf.Term(f => f.Name, "bar")
					, mf => mf.Term(f => f.Name, "foo2") || mf.Term(f => f.Name, "bar2")
				  )
				)
			  );

			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}

	}
}
