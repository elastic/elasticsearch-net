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
		public void MapFluent()
		{
			var result = this._client.MapFluent<ElasticSearchProject>(m=>m
				//MapFromAttributes() is shortcut to fill property mapping using the types' attributes and properties
				//Allows us to map the exceptions to the rule and be less verbose.
				.TypeName("elasticsearchprojects2")
				.IndexNames("nest_test_data", "nest_test_data_clone")
				.IgnoreConflicts()
				.MapFromAttributes() 
				
				.SetParent<Person>() //makes no sense but i needed a type :)
				.DisableAllField(false)
				.DisableIndexField(false)
				.DisableSizeField(false)
				.IdField(i=>i
					.SetIndex("not_analyzed")
					.SetPath("myOtherId")
					.SetStored(false)
				)
				.SourceField(s=>s
					.SetDisabled()
					.SetCompression()
					.SetCompressionTreshold("200b")
					.SetExcludes(new []{ "path1.*"})
					.SetIncludes(new [] { "path2.*"})
				)
				.TypeField(t=>t
					.SetIndexed()
					.SetStored()
				)
				.AnalyzerField(a=>a
					.SetPath(p=>p.Name)
					.SetIndexed()
				)
				.BoostField(b=>b
					.SetName(p=>p.LOC)
					.SetNullValue(1.0)
				)
				.RoutingField(r=>r
					.SetPath(p=>p.Country)
					.SetRequired()
				)
				.TimestampField(t=>t
					.SetDisabled(false)
					.SetPath(p=>p.StartedOn)
				)
				.TtlField(t=>t
					.SetDisabled(false)
					.SetDefault("1d")
				)
			);
			throw new Exception(result.ConnectionStatus.Request);
		}
	}
}
