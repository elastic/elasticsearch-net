// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
