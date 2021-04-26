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

using System.Collections.Generic;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.QueryDsl.Specialized.MoreLikeThis
{
	public class MoreLikeThisFullDocumentQueryUsageTests : QueryDslUsageTestsBase
	{
		public MoreLikeThisFullDocumentQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override QueryContainer QueryInitializer => new MoreLikeThisQuery
		{
			Fields = Infer.Fields<Project>(
				f => f.Name,
				f => f.Description),
			Like = new List<Like>
			{
				new LikeDocument<Project>(Project.Instance) { Routing = Project.Instance.Name },
				"some long text"
			}
		};

		protected override object QueryJson => new
		{
			more_like_this = new
			{
				fields = new []
				{
					"name",
					"description"
				},
				like = new object[]
				{
					new
					{
						_index = "project",
						doc = Project.InstanceAnonymous,
						routing = Project.Instance.Name
					},
					"some long text"
				}
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.MoreLikeThis(sn => sn
				.Fields(ff => ff
					.Field(f => f.Name)
					.Field(f => f.Description)
				)
				.Like(l => l
					.Document(d => d
						.Document(Project.Instance)
						.Routing(Project.Instance.Name)
					)
					.Text("some long text")
				)
			);
	}
}
