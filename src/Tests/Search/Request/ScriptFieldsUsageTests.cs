using System;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;

namespace Tests.Search.Request
{
	public class ScriptFieldsUsageTests : SearchUsageTestBase
	{
		public ScriptFieldsUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			script_fields = new {
			    test1 = new {
			      script = new {
			        inline = "doc['my_field_name'].value * 2"
			      }
			    },
			    test2 = new {
			      script = new {
			        inline = "doc['my_field_name'].value * factor",
			        @params = new {
			          factor = 2.0
			        }
			      }
			    }
			  }
		};

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.ScriptFields(sf=>sf
				.ScriptField("test1", sc=>sc
					.Inline("doc['my_field_name'].value * 2")
				)
				.ScriptField("test2", sc=>sc
					.Inline("doc['my_field_name'].value * factor")
					.Params(p=>p
						.Add("factor", 2.0)
					)
				)
			);

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>
			{
				ScriptFields = new ScriptFields
				{
					{ "test1", new ScriptField
					{
						Script = new InlineScript("doc['my_field_name'].value * 2")
					} },
					{ "test2", new InlineScript("doc['my_field_name'].value * factor")
					{
						Params = new FluentDictionary<string, object>
						{
							{ "factor", 2.0 }
						}
					} }
				}
			};
	}
}
