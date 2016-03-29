using System;
using System.Linq.Expressions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using static Tests.Framework.RoundTripper;
using Xunit;
using FluentAssertions;

namespace Tests.ClientConcepts.HighLevel.Inferrence.PropertyNames
{
	/** == Property Names */
	[Collection(IntegrationContext.Indexing)]
	public class PropertyNames : SimpleIntegration
	{
		private IElasticClient _client;

		public PropertyNames(IndexingCluster cluster) : base(cluster)
		{
			_client = cluster.Node.Client();
		}

		/** Property names resolve to the last token */
		[U] public void PropertyNamesAreResolvedToLastToken()
		{
			Expression<Func<Project, object>> expression = p => p.Name.Suffix("raw");
			Expect("raw").WhenSerializing<PropertyName>(expression);
		}

		/** :multi-field: {ref_current}/_multi_fields.html
		 *Property names cannot contain a `.` (dot), because of the potential for ambiguity with
		 *a field that is mapped as a {multi-field}[`multi_field`].
		 *
		 *NEST allows the call to go to Elasticsearch, deferring the naming conventions to the server side and,
		 * in the case of dots in field names, returns a `400 Bad Response` with a server error indicating the reason.
		 */
		[I] public void PropertyNamesContainingDotsCausesElasticsearchServerError()
		{
			var createIndexResponse = _client.CreateIndex("random-" + Guid.NewGuid().ToString().ToLowerInvariant(), c => c
				.Mappings(m => m
					.Map("type-with-dot", mm => mm
						.Properties(p => p
							.Text(s => s
								.Name("name-with.dot")
							)
						)
					)
				)
			);

			/** The response is not valid */
			createIndexResponse.IsValid.Should().BeFalse();

			/** `DebugInformation` provides an audit trail of information to help diagnose the issue */
			createIndexResponse.DebugInformation.Should().NotBeNullOrEmpty();

			/** `ServerError` contains information from the response from Elasticsearch */
			createIndexResponse.ServerError.Should().NotBeNull();
			createIndexResponse.ServerError.Status.Should().Be(400);
			createIndexResponse.ServerError.Error.Should().NotBeNull();
			createIndexResponse.ServerError.Error.RootCause.Should().NotBeNullOrEmpty();

			var rootCause = createIndexResponse.ServerError.Error.RootCause[0];

			rootCause.Reason.Should().Be("Field name [name-with.dot] cannot contain '.'");
			rootCause.Type.Should().Be("mapper_parsing_exception");
		}
	}
}
