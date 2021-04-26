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

using System.Text;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Tests.Core.Client;
using Tests.Domain;

namespace Tests.Reproduce
{
	public class GithubIssue4078
	{
		[U]
		public void BulkIndexOperationIssue() {

			var indexResponse = TestClient.InMemoryWithJsonNetSerializer.Bulk(d =>
				d.Index<Project>(i => i.IfSequenceNumber(12345).IfPrimaryTerm(67890).Document(Project.Instance))
			);
			Encoding.UTF8.GetString(indexResponse.ApiCall.RequestBodyInBytes).Should().Contain("\"if_seq_no\"").And.Contain("12345")
				.And.Contain("\"if_primary_term\"").And.Contain("67890");
		}

		[U]
		public void BulkUpdateOperationIssue()
		{

			var indexResponse = TestClient.InMemoryWithJsonNetSerializer.Bulk(d =>
				d.Update<Project, object>(i => i.IfSequenceNumber(12345).IfPrimaryTerm(67890).Doc(Project.Instance))
			);
			Encoding.UTF8.GetString(indexResponse.ApiCall.RequestBodyInBytes).Should().Contain("\"if_seq_no\"").And.Contain("12345")
				.And.Contain("\"if_primary_term\"").And.Contain("67890");
		}
	}
}
