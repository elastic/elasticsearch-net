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
using System.Text;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using FluentAssertions;
using Nest;

namespace Tests.Reproduce
{
	public class GitHubIssue4958
	{
		[U]
		public void SearchUsesConfiguredDefaultMappingFor()
		{
			var defaultIndex = "documents";
			var pool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));

			var settings = new ConnectionSettings(pool, new InMemoryConnection())
				.DefaultIndex(defaultIndex)
				.DisableDirectStreaming()
				.DefaultMappingFor<AccountAddressElasticInfo>(i => i
					.IndexName(defaultIndex)
					.PropertyName(p => p.AccountId, "accountid")
					.IdProperty(p => p.AccountAddressVersionId)
				);

			var client = new ElasticClient(settings);

			var response = client.DeleteByQuery<AccountAddressElasticInfo>(d => d
				.Query(q => q
					.Term(f => f.AccountId, "value")
				)
				.MaximumDocuments(100000)
			);

			var json = Encoding.UTF8.GetString(response.ApiCall.RequestBodyInBytes);
			json.Should().Contain("\"accountid\"");
		}

		public class AccountAddressInfo
		{
			public string AccountAddressVersionId { get; set; }
			public string AccountId { get; set; }
		}

		public class AccountAddressElasticInfo : AccountAddressInfo
		{

			public DateTime Timestamp => DateTime.Now;
		}
	}
}
