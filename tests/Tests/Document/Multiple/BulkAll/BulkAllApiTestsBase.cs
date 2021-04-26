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
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;

namespace Tests.Document.Multiple.BulkAll
{
	public abstract class BulkAllApiTestsBase : IClusterFixture<IntrusiveOperationCluster>
	{
		protected BulkAllApiTestsBase(IntrusiveOperationCluster cluster) => Client = cluster.Client;

		protected IElasticClient Client { get; }

		protected static string CreateIndexName() => $"project-copy-{Guid.NewGuid().ToString("N").Substring(8)}";

		protected IEnumerable<SmallObject> CreateLazyStreamOfDocuments(int count)
		{
			for (var i = 0; i < count; i++)
				yield return new SmallObject() { Id = i };
		}

		protected async Task CreateIndexAsync(string indexName, int numberOfShards, Func<TypeMappingDescriptor<SmallObject>, ITypeMapping> mappings = null)
		{
			var result = await Client.Indices.CreateAsync(indexName, s => s
				.Settings(settings => settings
					.NumberOfShards(numberOfShards)
					.NumberOfReplicas(0)
				)
				.Map(mappings)
			);
			result.Should().NotBeNull();
			result.ShouldBeValid();
		}

		protected static void OnError(ref Exception ex, Exception e, EventWaitHandle handle)
		{
			ex = e;
			handle.Set();
			throw e;
		}

		protected class SmallObject
		{
			public int Id { get; set; }
			public string Number { get; set; }
		}
	}
}
