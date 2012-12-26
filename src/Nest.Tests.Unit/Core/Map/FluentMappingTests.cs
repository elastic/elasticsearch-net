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

namespace Nest.Tests.Unit.Core.Map
{
	[TestFixture]
	public class FluentMappingTests : BaseJsonTests
	{
		[Test]
		public void GetSimple()
		{
			var mapping = new TypeMappingDescriptor<ElasticSearchProject>()
				//MapFromAttributes() is shortcut to fill property mapping using the types' attributes and properties
				//Allows us to map the exceptions to the rule and be less verbose.
				.MapFromAttributes() 
				.TypeName("elasticsearchprojects2")
				.SetParent<Person>() //makes no sense but i needed a type :)
				.IdFieldMapping(i=>i
					.SetIndex("not_analyzed")
					.SetPath("myOtherId")
					.SetStored(false)
				)
				.SourceFieldMapping(s=>s
					.SetDisabled()
					.SetCompression()
					.SetCompressionTreshold("200b")
					.SetExcludes(new []{ "path1.*"})
					.SetIncludes(new [] { "path2.*"})
				)
				.TypeFieldMapping(t=>t
					.SetIndexed()
					.SetStored()
				)
				.AllFieldMapping(a=>a.SetDisabled())
				.AnalyzerFieldMapping(a=>a
					.SetPath(p=>p.Name)
					.SetIndexed()
				)
				.BoostFieldMapping(b=>b
					.SetName(p=>p.LOC)
					.SetNullValue(1.0)
				)
				.RoutingFieldMapping(r=>r
					.SetPath(p=>p.Country)
					.SetRequired()
				)
				;

		}
	}
}
