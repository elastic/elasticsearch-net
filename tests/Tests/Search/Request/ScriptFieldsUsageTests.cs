// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Search.Request
{
	// ReSharper disable InvalidXmlDocComment
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
	 * See the Elasticsearch documentation on {ref_current}/search-request-body.html#request-body-search-script-fields[script fields]
	 * for more detail.
	 */
	// ReSharper restore InvalidXmlDocComment
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
						source = "doc['numberOfCommits'].value * 2",
					}
				},
				test2 = new
				{
					script = new
					{
						source = "doc['numberOfCommits'].value * params.factor",
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
					.Source("doc['numberOfCommits'].value * 2")
				)
				.ScriptField("test2", sc => sc
					.Source("doc['numberOfCommits'].value * params.factor")
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
					{ "test1", new ScriptField { Script = new InlineScript("doc['numberOfCommits'].value * 2") } },
					{
						"test2", new InlineScript("doc['numberOfCommits'].value * params.factor")
						{
							Params = new FluentDictionary<string, object>
							{
								{ "factor", 2.0 }
							}
						}
					}
				}
			};


		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			foreach (var fields in response.Fields)
			{
				fields.Value<int>("test1").Should().BeGreaterOrEqualTo(0);
				fields.Value<double>("test2").Should().BeGreaterOrEqualTo(0);
			}
		}
	}
}
