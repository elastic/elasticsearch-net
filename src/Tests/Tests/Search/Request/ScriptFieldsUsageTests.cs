using System;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.Search.Request
{
    /**
     * Allows to return a script evaluation (based on different fields) for each hit.
     *
     * Script fields can work on fields that are not stored, and allow to return custom values to
     * be returned (the evaluated value of the script).
     *
     * Script fields can also access the actual `_source` document and extract specific elements to
     * be returned from it by using `params['_source']`.
     *
     * Script fields can be accessed on the response using <<returned-fields,`.Fields`>>, similarly to stored fields.
     *
     * See the Elasticsearch documentation on {ref_current}/search-request-script-fields.html[script fields]
     * for more detail.
     */
    public class ScriptFieldsUsageTests : SearchUsageTestBase
	{
		public ScriptFieldsUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			script_fields = new
			{
				test1 = new
				{
					script = new
					{
						source = "doc['my_field_name'].value * 2",
					}
				},
				test2 = new
				{
					script = new
					{
						source = "doc['my_field_name'].value * params.factor",
						@params = new
						{
							factor = 2.0
						}
					}
				}
			}
		};

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.ScriptFields(sf => sf
				.ScriptField("test1", sc => sc
					.Source("doc['my_field_name'].value * 2")
				)
				.ScriptField("test2", sc => sc
					.Source("doc['my_field_name'].value * params.factor")
					.Params(p => p
						.Add("factor", 2.0)
					)
				)
			);

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>
			{
				ScriptFields = new ScriptFields
				{
					{ "test1", new ScriptField { Script = new InlineScript("doc['my_field_name'].value * 2") } },
					{ "test2", new InlineScript("doc['my_field_name'].value * params.factor")
						{
							Params = new FluentDictionary<string, object>
							{
								{ "factor", 2.0 }
							}
						}
					}
				}
			};
	}
}
