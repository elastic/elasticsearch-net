using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Nest.Tests.Integration.Reproduce
{
	[TestFixture]
	public class Reproduce1908Tests : IntegrationTests
	{
		private const string DefaultIndex = "reproduce1908";

		public class Polygon
		{
			public PolygonGeoShape VertexValues { get; set; }
		}

		protected override IElasticClient Client
		{
			get
			{
				return new ElasticClient(Settings);
			}
		}

		protected override IConnectionSettingsValues Settings
		{
			get
			{
				return new ConnectionSettings(ElasticsearchConfiguration.CreateBaseUri(), DefaultIndex)
					.SetMaximumAsyncConnections(ElasticsearchConfiguration.MaxConnections)
					.DisableAutomaticProxyDetection(false)
					.PrettyJson()
					.ExposeRawResponse()
					.SetDefaultPropertyNameInferrer(p => p)
					.SetConnectionStatusHandler(r =>
					{
						// log out the requests
						if (r.Request != null)
						{
							Console.WriteLine("{0} {1} \n{2}\n", r.RequestMethod.ToUpperInvariant(), r.RequestUrl,
								Encoding.UTF8.GetString(r.Request));
						}
						else
						{
							Console.WriteLine("{0} {1}\n", r.RequestMethod.ToUpperInvariant(), r.RequestUrl);
						}

						if (r.ResponseRaw != null)
						{
							Console.WriteLine("Status: {0}\n{1}\n\n{2}\n", 
								r.HttpStatusCode, 
								Encoding.UTF8.GetString(r.ResponseRaw), 
								new string('-', 30));
						}
						else
						{
							Console.WriteLine("Status: {0}\n\n{1}\n", 
								r.HttpStatusCode, 
								new string('-', 30));
						}
					});
			}
		}

		[Test]
		public void TypePropertySerializesLowercaseWhenUsingPascalPropertyNameInferrer()
		{
			if (Client.IndexExists(DefaultIndex).Exists)
				Client.DeleteIndex(DefaultIndex);

			Client.CreateIndex(DefaultIndex, c => c
				.AddMapping<Polygon>(m => m
					.MapFromAttributes()
					.Properties(p => p
						.GeoShape(g => g
							.Name(f => f.VertexValues)
						)
					)
				)
				);

			Client.Index(new Polygon
			{
				VertexValues = new PolygonGeoShape
				{
					Coordinates = new List<List<List<double>>>
					{
						new List<List<double>>
						{
							new List<double> { 45.50537109375, 1.186438639445215 },
							new List<double> { 49.41650390625, 5.922044619883305 },
							new List<double> { 54.5141601562, 3.9957805129630253 },
							new List<double> { 52.7124023437, -1.208406497271858 },
							new List<double> { 48.3837890625, -0.615222552406841 },
							new List<double> { 45.50537109375, 1.186438639445215 },
						}
					},
				}
			});

			Client.Refresh(r => r.Index(DefaultIndex));

			var coordinates = new[] { 48.864686, 2.351616 };

			var response1 = Client.Search<Polygon>(x => x
				.Query(q => q
					.Filtered(f => f
						.Filter(g => g
							.GeoShapePoint("VertexValues", d => d
								.Coordinates(coordinates)
								.Relation(GeoShapeRelation.Intersects)
							)
						)
					)
				)
				);

			Assert.IsTrue(response1.IsValid);
			Assert.IsTrue(response1.Documents.Count() == 1);

			Client.DeleteIndex(DefaultIndex);
		}
	}
}