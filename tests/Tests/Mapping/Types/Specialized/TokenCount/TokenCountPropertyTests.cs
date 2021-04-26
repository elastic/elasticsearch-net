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

namespace Tests.Mapping.Types.Specialized.TokenCount
{
	public class TokenCountPropertyTests : PropertyTestsBase
	{
		public TokenCountPropertyTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			properties = new
			{
				name = new
				{
					type = "token_count",
					analyzer = "standard",
					enable_position_increments = false,
					index = false,
					null_value = 0.0
				}
			}
		};

		protected override Func<PropertiesDescriptor<Project>, IPromise<IProperties>> FluentProperties => f => f
			.TokenCount(s => s
				.Name(p => p.Name)
				.Analyzer("standard")
				.EnablePositionIncrements(false)
				.Index(false)
				.NullValue(0.0)
			);


		protected override IProperties InitializerProperties => new Properties
		{
			{
				"name", new TokenCountProperty
				{
					Index = false,
					Analyzer = "standard",
					EnablePositionIncrements = false,
					NullValue = 0.0
				}
			}
		};
	}
}
