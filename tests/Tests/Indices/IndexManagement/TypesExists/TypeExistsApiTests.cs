// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;
using static Nest.Infer;

namespace Tests.Indices.IndexManagement.TypesExists
{
	public class TypeExistsApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, ExistsResponse, ITypeExistsRequest, TypeExistsDescriptor, TypeExistsRequest>
	{
		public TypeExistsApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;

		protected override Func<TypeExistsDescriptor, ITypeExistsRequest> Fluent => d => d
			.IgnoreUnavailable();

		protected override HttpMethod HttpMethod => HttpMethod.HEAD;

		protected override TypeExistsRequest Initializer => new TypeExistsRequest(Index<Project>(), "_doc")
		{
			IgnoreUnavailable = true
		};

		protected override string UrlPath => $"/project/_mapping/_doc?ignore_unavailable=true";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Indices.TypeExists(Index<Project>(), "_doc", f),
			(client, f) => client.Indices.TypeExistsAsync(Index<Project>(), "_doc", f),
			(client, r) => client.Indices.TypeExists(r),
			(client, r) => client.Indices.TypeExistsAsync(r)
		);

		protected override TypeExistsDescriptor NewDescriptor() => new TypeExistsDescriptor(Index<Project>(), "doc");
	}
}
