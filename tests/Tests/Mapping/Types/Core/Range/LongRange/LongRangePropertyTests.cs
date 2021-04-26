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

namespace Tests.Mapping.Types.Core.Range.LongRange
{
	[SkipVersion("<5.2.0", "dedicated range types is a new 5.2.0 feature")]
	public class LongRangePropertyTests : PropertyTestsBase
	{
		public LongRangePropertyTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			properties = new
			{
				ranges = new
				{
					type = "object",
					properties = new
					{
						longs = new
						{
							type = "long_range",
							store = true,
							index = false,
							coerce = true
						}
					}
				}
			}
		};

		protected override Func<PropertiesDescriptor<Project>, IPromise<IProperties>> FluentProperties => f => f
			.Object<Ranges>(m => m
				.Name(p => p.Ranges)
				.Properties(props => props
					.LongRange(n => n
						.Name(p => p.Longs)
						.Store()
						.Index(false)
						.Coerce()
					)
				)
			);


		protected override IProperties InitializerProperties => new Properties
		{
			{
				"ranges", new ObjectProperty
				{
					Properties = new Properties
					{
						{
							"longs", new LongRangeProperty
							{
								Store = true,
								Index = false,
								Coerce = true
							}
						}
					}
				}
			}
		};
	}
}
