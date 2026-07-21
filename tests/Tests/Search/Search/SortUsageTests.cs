// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Search.Search;

public class SortUsageTests : SearchUsageTestBase
{
	public SortUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

	protected override bool VerifyJson => true;

	// TODO: Port other tests from NEST

	// NOTE: The descriptor syntax is usable but more complex than NEST which provides a collection-based promise.
	// TODO: Consider updating code-gen to produce simpler descriptor syntax for this case.
	protected override Action<SearchRequestDescriptor<Project>> Fluent => s => s
		.Sort(
			s => s.Field(f => f.StartedOn, fs => fs.Order(SortOrder.Asc)),
			s => s.Field(f => f.Name, fs => fs.Order(SortOrder.Desc)),
			s => s.GeoDistance(f => f
				.Field(fld => fld.LocationPoint)
				.DistanceType(GeoDistanceType.Arc)
				.Order(SortOrder.Asc)
				.Unit(DistanceUnit.Centimeters)
				.Mode(SortMode.Min)
				.Location(new []
				{
					GeoLocation.LatitudeLongitude(new () { Lat = 70, Lon = -70 }),
					GeoLocation.LatitudeLongitude(new () { Lat = -12, Lon = 12 })
				})));

	protected override SearchRequest<Project> Initializer =>
		new()
		{
			Sort = new[]
			{
				SortOptions.Field(Infer.Field<Project>(f => f.StartedOn), new FieldSort { Order = SortOrder.Asc }),
				SortOptions.Field(Infer.Field<Project>(f => f.Name), new FieldSort { Order = SortOrder.Desc }),
				SortOptions.GeoDistance(new GeoDistanceSort
				{
					Field = Infer.Field<Project>(f => f.LocationPoint),
					DistanceType = GeoDistanceType.Arc,
					Order = SortOrder.Asc,
					Unit = DistanceUnit.Centimeters,
					Mode = SortMode.Min,
					Location = new []
					{
						GeoLocation.LatitudeLongitude(new () { Lat = 70, Lon = -70 }),
						GeoLocation.LatitudeLongitude(new () { Lat = -12, Lon = 12 })
					}
				})
			}
		};
}
