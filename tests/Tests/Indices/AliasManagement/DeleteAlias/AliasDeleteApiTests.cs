// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Transport;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Indices.AliasManagement.DeleteAlias
{

	public class DeleteAliasApiTests
		: ApiIntegrationTestBase<WritableCluster, DeleteAliasResponse, IDeleteAliasRequest, DeleteAliasDescriptor, DeleteAliasRequest>
	{
		public DeleteAliasApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;

		protected override Func<DeleteAliasDescriptor, IDeleteAliasRequest> Fluent => null;
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;
		protected override DeleteAliasRequest Initializer => new DeleteAliasRequest(Infer.AllIndices, Names);
		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/_all/_alias/{CallIsolatedValue + "-alias"}";
		private Names Names => Infer.Names(CallIsolatedValue + "-alias");

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var index in values.Values)
				client.Indices.Create(index, c => c
					.Aliases(aa => aa.Alias(index + "-alias"))
				);
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Indices.DeleteAlias(Infer.AllIndices, Names),
			(client, f) => client.Indices.DeleteAliasAsync(Infer.AllIndices, Names),
			(client, r) => client.Indices.DeleteAlias(r),
			(client, r) => client.Indices.DeleteAliasAsync(r)
		);
	}
}
