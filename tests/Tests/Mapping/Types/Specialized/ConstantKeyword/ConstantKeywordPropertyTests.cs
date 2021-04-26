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
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Mapping.Types.Specialized.ConstantKeyword
{
	[SkipVersion("<7.7.0", "introduced in 7.7.0")]
	public class ConstantKeywordPropertyTests : PropertyTestsBase
	{
		public ConstantKeywordPropertyTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			properties = new
			{
				versionControl = new
				{
					type = "constant_keyword",
					value = Project.VersionControlConstant,
				}
			}
		};

		protected override Func<PropertiesDescriptor<Project>, IPromise<IProperties>> FluentProperties => f => f
			.ConstantKeyword(s => s
				.Name(n => n.VersionControl)
				.Value(Project.VersionControlConstant)
			);

		protected override IProperties InitializerProperties => new Properties
		{
			{
				"versionControl", new ConstantKeywordProperty
				{
					Value = Project.VersionControlConstant
				}
			}
		};
	}
}
