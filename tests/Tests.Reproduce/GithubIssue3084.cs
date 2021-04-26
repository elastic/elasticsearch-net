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
using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;

namespace Tests.Reproduce
{
	public class GithubIssue3084 : IClusterFixture<WritableCluster>
	{
		private readonly WritableCluster _cluster;

		public GithubIssue3084(WritableCluster cluster) => _cluster = cluster;

		protected static string RandomString() => Guid.NewGuid().ToString("N").Substring(0, 8);

		[I]
		public void DeserializeErrorIsTheSameForAsync()
		{
			var client = _cluster.Client;
			var index = $"gh3084-{RandomString()}";
			var document = new ObjectVersion1 { Id = 1, Numeric = 0.1 };

			var indexResult = client.Index(document, i => i.Index(index));
			indexResult.ShouldBeValid();

			Action getDoc = () => client.Get<ObjectVersion2>(new GetRequest(index, 1));
			Func<Task<IGetResponse<ObjectVersion2>>> getDocAsync = async () => await client.GetAsync<ObjectVersion2>(new GetRequest(index, 1));

			getDoc.Should().Throw<Exception>("synchonous code path should throw");
			getDocAsync.Should().Throw<Exception>("async code path should throw");
		}

		public class ObjectVersion1
		{
			public int Id { get; set; }
			public double Numeric { get; set; }
		}

		public class ObjectVersion2
		{
			public int Id { get; set; }
			public int Numeric { get; set; }
		}
	}
}
