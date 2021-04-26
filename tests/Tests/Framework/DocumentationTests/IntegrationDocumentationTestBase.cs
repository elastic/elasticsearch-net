/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;
using Nest;
using Tests.Core.Client;
using Tests.Core.ManagedElasticsearch.Clusters;

namespace Tests.Framework.DocumentationTests
{
	public abstract class DocumentationTestBase
	{
		protected virtual IElasticClient Client => TestClient.DefaultInMemoryClient;

		protected static string RandomString() => Guid.NewGuid().ToString("N").Substring(0, 8);
	}

	public abstract class IntegrationDocumentationTestBase : DocumentationTestBase
	{
		protected readonly ClientTestClusterBase Cluster;

		protected IntegrationDocumentationTestBase(ClientTestClusterBase cluster) => Cluster = cluster;

		protected override IElasticClient Client => Cluster.Client;
	}
}
