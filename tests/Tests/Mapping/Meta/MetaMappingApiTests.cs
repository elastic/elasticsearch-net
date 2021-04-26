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
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;
using Tests.Mapping.Types;

namespace Tests.Mapping.Meta
{
	[SkipVersion("<7.6.0", "Meta added in Elasticsearch 7.6.0")]
	public class MetaMappingApiTests
		: PropertyTestsBase
	{
		public MetaMappingApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override Func<PropertiesDescriptor<Project>, IPromise<IProperties>> FluentProperties => p => p
			.Number(n => n
				.Name(nn => nn.Rank)
				.Type(NumberType.Integer)
				.Meta(m => m
					.Add("unit", "popularity")
				)
			);
		protected override IProperties InitializerProperties => new Properties<Project>
		{
			{ n => n.Rank, new NumberProperty(NumberType.Integer)
				{
					Meta = new Dictionary<string, string>
					{
						{ "unit", "popularity" }
					}
				}
			}
		};

		protected override void ExpectResponse(PutMappingResponse response)
		{
			base.ExpectResponse(response);

			// check the meta shows up in get mapping API
			var getMappingResponse = Client.Indices.GetMapping<Project>(m => m.Index(CallIsolatedValue));
			getMappingResponse.IsValid.Should().BeTrue();
			var mappingMeta = getMappingResponse.Indices[CallIsolatedValue].Mappings.Properties["rank"].Meta;
			mappingMeta.Should().NotBeNull().And.ContainKey("unit");
			mappingMeta["unit"].Should().Be("popularity");

			// check the meta shows up in field capabilities API
			var fieldCapsResponse = Client.FieldCapabilities(CallIsolatedValue, f => f
				.Fields<Project>(ff => ff.Rank)
			);
			fieldCapsResponse.IsValid.Should().BeTrue();
			var meta = fieldCapsResponse.Fields["rank"].Integer.Meta;
			meta.Should().NotBeNull().And.ContainKey("unit");
			meta["unit"].Should().BeEquivalentTo("popularity");
		}
	}
}
