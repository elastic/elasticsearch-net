// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Text;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using Elastic.Transport.Products.Elasticsearch.Failures;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;

namespace Tests.Reproduce
{
	public class GithubPr3815
	{
		[U(Skip = "Needs to be fixed but not on this branch")]
		public void CanDeserializeNestedError()
		{
			var nestedCausedByError = @"{
				""status"": 400,
				""error"": {
					""type"": ""illegal_argument_exception"",
					""reason"": ""failed to execute script"",
					""caused_by"": {
						""type"": ""script_exception"",
						""reason"": ""failed to run inline script [use(java.lang.Exception) {throw new Exception(\""Customized Exception\"")}] using lang [groovy]"",
						""caused_by"": {
							""type"": ""privileged_action_exception"",
							""reason"": null,
							""caused_by"": {
								""type"": ""exception"",
								""reason"": ""Custom Exception""
							}
						}
					}
				}
			}";

			var bytes = Encoding.UTF8.GetBytes(nestedCausedByError);
			var connection = new InMemoryConnection(bytes, 400);
			var settings = new ConnectionSettings(new SingleNodeConnectionPool(new Uri("http://localhost:9200")), connection);
			var client = new ElasticClient(settings);

			CheckCustomDeserialiseMethods(() => client.Cat.Help());
			CheckCustomDeserialiseMethods(() => client.Nodes.HotThreads());
			CheckCustomDeserialiseMethods(() => client.MultiGet());
			CheckCustomDeserialiseMethods(() => client.MultiSearch());
			CheckCustomDeserialiseMethods(() => client.Sql.Translate());
			CheckCustomDeserialiseMethods(() => client.Security.GetCertificates());
		}

		private static void CheckCustomDeserialiseMethods(Func<ResponseBase> perform)
		{
			var response = perform();
			response.ShouldNotBeValid();
			var se = response.ServerError;
			se.Status.Should().Be(400);
			se.Should().NotBeNull();
			ShouldDeserialize(se.Error);
			ShouldDeserialize(se.Error.CausedBy);
			ShouldDeserialize(se.Error.CausedBy.CausedBy, true);
			ShouldDeserialize(se.Error.CausedBy.CausedBy.CausedBy);
		}

		private static void ShouldDeserialize(ErrorCause error, bool nullReason = false)
		{
			error.Should().NotBeNull();
			error.Type.Should().NotBeNullOrEmpty();
			if (nullReason)
				error.Reason.Should().BeNull();
			else
				error.Reason.Should().NotBeNullOrEmpty();
		}
	}
}
