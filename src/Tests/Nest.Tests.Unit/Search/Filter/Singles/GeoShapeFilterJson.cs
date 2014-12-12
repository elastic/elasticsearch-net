using NUnit.Framework;
using Nest.Tests.MockData.Domain;
using System.Collections.Generic;

namespace Nest.Tests.Unit.Search.Filter.Singles
{
	[TestFixture]
	public class GeoShapeFilterJson
	{
		[Test]
		public void GeoShapeEnvelopeFilter()
		{
			//[13.0, 53.0], [14.0, 52.0]]
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Filter(filter => filter
					.Cache(true)
					.Name("my_geo_filter")
					.GeoShapeEnvelope(f => f.Origin, d => d
						.Coordinates(new[] { new[] { 13.0, 53.0 }, new[] { 14.0, 52.0 } })
						.Relation(GeoShapeRelation.Intersects)
				)
			);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
					geo_shape: {
						origin: {
							shape: {
								coordinates: [[13.0, 53.0], [14.0, 52.0]],
								type: ""envelope""
							},
							relation: ""intersects""
						},
						_cache: true,
						_name: ""my_geo_filter""
					}
				}
			}";
			Assert.True(json.JsonEquals(expected), json);
		}
		
		[Test]
		public void GeoShapeCircleFilter()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Filter(f => f
					.Cache(true)
					.Name("my_geo_filter")
					.GeoShapeCircle(p => p.Origin, d => d
						.Coordinates(new[] { -45.0, 45.0 })
						.Radius("100m")
						.Relation(GeoShapeRelation.Within)
					)
				);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
					geo_shape: {
						origin: {
							shape: {
								coordinates: [ -45.0, 45.0 ],
								radius: ""100m"",
								type: ""circle""
							},
							relation: ""within""
						},
						_cache: true,
						_name: ""my_geo_filter""
					}
				}
			}";
			Assert.IsTrue(json.JsonEquals(expected), json);
		}

		[Test]
		public void GeoShapeLineStringFilter()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Filter(f => f
					.Cache(true)
					.Name("my_geo_filter")
					.GeoShapeLineString(p => p.Origin, d => d
						.Coordinates(new[] { new[] { 13.0, 53.0 }, new[] { 14.0, 52.0 } })
						.Relation(GeoShapeRelation.Disjoint)
					)
				);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
					geo_shape: {
						origin: {
							shape: {
								coordinates: [
									[ 13.0, 53.0 ], [ 14.0, 52.0 ]
								],
								type: ""linestring""
							},
							relation: ""disjoint""
						},
						_cache: true,
						_name: ""my_geo_filter""
					}
				}
			}";
			Assert.IsTrue(json.JsonEquals(expected), json);
		}

		[Test]
		public void GeoShapeMultiLineStringFilter()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Filter(f => f
					.Cache(true)
					.Name("my_geo_filter")
					.GeoShapeMultiLineString(p => p.Origin, d => d
						.Coordinates(new[] { 
								new[] { new[] { 102.0, 2.0 }, new[] { 103.0, 2.0 }, new[] { 103.0, 3.0 }, new[] { 102.0, 3.0 } },
								new[] { new[] { 100.0, 0.0 }, new[] { 101.0, 0.0 }, new[] { 101.0, 1.0 }, new[] { 100.0, 1.0 } },
								new[] { new[] { 100.2, 0.2 }, new[] { 100.8, 0.2 }, new[] { 100.8, 0.8 }, new[] { 100.2, 0.8 } } 
							}
						)
						.Relation(GeoShapeRelation.Intersects)
					)
				);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
					geo_shape: {
						origin: {
							shape: {
								coordinates: [
									[ [ 102.0, 2.0 ], [ 103.0, 2.0 ], [ 103.0, 3.0 ], [ 102.0, 3.0 ] ],
									[ [ 100.0, 0.0 ], [ 101.0, 0.0 ], [ 101.0, 1.0 ], [ 100.0, 1.0 ] ],
									[ [ 100.2, 0.2 ], [ 100.8, 0.2 ], [ 100.8, 0.8 ], [ 100.2, 0.8 ] ]
								],
								type: ""multilinestring""
							},
							relation: ""intersects""
						},
						_cache: true,
						_name: ""my_geo_filter""
					}
				}
			}";
			Assert.IsTrue(json.JsonEquals(expected), json);
		}

		[Test]
		public void GeoShapePointFilter()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Filter(f => f
					.Cache(true)
					.Name("my_geo_filter")
					.GeoShapePoint(p => p.Origin, d => d
						.Coordinates(new [] { 1.0, 2.0 })
						.Relation(GeoShapeRelation.Disjoint)
					)
				);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
					geo_shape: {
						origin: {
							shape: {
								coordinates:[ 1.0, 2.0 ],
								type: ""point""
							},
							relation: ""disjoint""
						},
						_cache: true,
						_name: ""my_geo_filter""
					}
				}
			}";
			Assert.IsTrue(json.JsonEquals(expected), json);
		}

		public void GeoShapeMultiPointFilter()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Filter(f => f
					.Cache(true)
					.Name("my_geo_filter")
					.GeoShapeMultiPoint(p => p.Origin, d => d
						.Coordinates(new[] { new[] { 13.0, 53.0 }, new[] { 14.0, 52.0 } })
						.Relation(GeoShapeRelation.Within)
					)
				);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
					geo_shape: {
						origin: {
							shape: {
								coordinates:[ [ 13.0, 53.0 ], [ 14.0, 52.0 ] ],
								type: ""multipoint""
							},
							relation: ""within""
						},
						_cache: true,
						_name: ""my_geo_filter""
					}
				}
			}";
			Assert.IsTrue(json.JsonEquals(expected), json);
		}

		[Test]
		public void GeoShapePolygonFilter()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Filter(f => f
					.Cache(true)
					.Name("my_geo_filter")
					.GeoShapePolygon(p => p.Origin, d => d
						.Coordinates(new[] { 
							new[] { new[] { 100.0, 0.0 }, new[] { 101.0, 0.0 }, new[] { 101.0, 1.0 }, new[] { 100.0, 1.0 }, new [] { 100.0, 0.0 } },
							new[] { new[] { 100.2, 0.2 }, new[] { 100.8, 0.2 }, new[] { 100.8, 0.8 }, new[] { 100.2, 0.8 }, new [] { 100.2, 0.2 } }
						})
						.Relation(GeoShapeRelation.Disjoint)
					)
				);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
					geo_shape: {
						origin: {
							shape: {
								coordinates: [
									[ [ 100.0, 0.0 ], [ 101.0, 0.0 ], [ 101.0, 1.0 ], [ 100.0, 1.0 ], [ 100.0, 0.0 ] ],
									[ [ 100.2, 0.2 ], [ 100.8, 0.2 ], [ 100.8, 0.8 ], [ 100.2, 0.8 ], [ 100.2, 0.2 ] ]
								],
								type: ""polygon""
							},
							relation: ""disjoint""
						},
						_cache: true,
						_name: ""my_geo_filter""
					}
				}
			}";
			Assert.IsTrue(json.JsonEquals(expected), json);
		}

		[Test]
		public void GeoShapeMultiPolygonFilter()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Filter(f => f
					.Cache(true)
					.Name("my_geo_filter")
					.GeoShapeMultiPolygon(p => p.Origin, d => d
						.Coordinates(new[] {
							new [] {
								new [] {
									new [] { 102.0, 2.0 }, new [] { 103.0, 2.0 }, new [] { 103.0, 3.0 }, new [] { 102.0, 3.0 }, new [] { 102.0, 2.0 }
								}
							},
							new [] {
								new [] {
									new [] { 100.0, 0.0 }, new [] { 101.0, 0.0 }, new [] { 101.0, 1.0 }, new [] {100.0, 1.0 }, new [] { 100.0, 0.0 }
								},
								new [] {
									new [] { 100.2, 0.2 }, new [] { 100.8, 0.2 }, new [] { 100.8, 0.8 }, new [] { 100.2, 0.8 }, new [] { 100.2, 0.2 }
								}
							}
						})
						.Relation(GeoShapeRelation.Within)
					)
				);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
					geo_shape: {
						origin: {
							shape: {
								coordinates:[
									[ [ [ 102.0, 2.0 ], [ 103.0, 2.0 ], [ 103.0, 3.0 ], [ 102.0, 3.0 ], [ 102.0, 2.0 ] ] ],
									[
									  [ [ 100.0, 0.0 ], [ 101.0, 0.0 ], [ 101.0, 1.0 ], [ 100.0, 1.0 ], [ 100.0, 0.0 ] ],
									  [ [ 100.2, 0.2 ], [ 100.8, 0.2 ], [ 100.8, 0.8 ], [ 100.2, 0.8 ], [ 100.2, 0.2 ] ]
									]
								  ],
								type: ""multipolygon""
							},
							relation: ""within""
						},
						_cache: true,
						_name: ""my_geo_filter""
					}
				}
			}";
			Assert.IsTrue(json.JsonEquals(expected), json);
		}
	}
}
