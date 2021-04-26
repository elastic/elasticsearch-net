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
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;
using static Nest.Infer;

namespace Tests.Mapping.Types.Core.Join
{
	public class JoinPropertyTests : PropertyTestsBase
	{
		public JoinPropertyTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			properties = new
			{
				name = new
				{
					type = "join",
					relations = new
					{
						project = "commits"
					}
				}
			}
		};

		protected override Func<PropertiesDescriptor<Project>, IPromise<IProperties>> FluentProperties => f => f
			.Join(pr => pr
				.Name(p => p.Name)
				.Relations(r => r.Join<Project, CommitActivity>())
			);


		protected override IProperties InitializerProperties => new Properties
		{
			{
				"name", new JoinProperty
				{
					Relations = new Relations
					{
						{ Relation<Project>(), Relation<CommitActivity>() }
					}
				}
			}
		};
	}

	public class JoinPropertyComplexTests : PropertyTestsBase
	{
		public JoinPropertyComplexTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			properties = new
			{
				name = new
				{
					type = "join",
					relations = new
					{
						project = "commits",
						parent2 = new[] { "child2", "child3" }
					}
				}
			}
		};

		protected override Func<PropertiesDescriptor<Project>, IPromise<IProperties>> FluentProperties => f => f
			.Join(pr => pr
				.Name(p => p.Name)
				.Relations(r => r
					.Join<Project, CommitActivity>()
					.Join("parent2", "child2", "child3")
				)
			);


		protected override IProperties InitializerProperties => new Properties
		{
			{
				"name", new JoinProperty
				{
					Relations = new Relations
					{
						{ Relation<Project>(), typeof(CommitActivity) },
						{ "parent2", "child2", "child3" }
					}
				}
			}
		};
	}
}
