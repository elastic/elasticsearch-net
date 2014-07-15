using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Integration.Core.Map.Properties
{
	[TestFixture]
	public class PropertiesTests : BaseMappingTests
	{
		[Test]
		public void StringProperty()
		{
			this.Client.DeleteMapping<ElasticsearchProject>();
			var result = this.Client.Map<ElasticsearchProject>(m => m
				.Properties(props => props
					.String(s => s
						.Name(p => p.Name)
						.IndexName("my_crazy_name_i_want_in_lucene")
						.IncludeInAll()
						.Index(FieldIndexOption.Analyzed)
						.IndexAnalyzer("standard")
						.IndexOptions(IndexOptions.Positions)
						.NullValue("my_special_null_value")
						.OmitNorms()
						.PositionOffsetGap(1)
						.SearchAnalyzer("standard")
						.Store()
						.TermVector(TermVectorOption.WithPositionsOffsets)
						.Boost(1.1)
					)
				)
			);
			this.DefaultResponseAssertations(result);
			var mappingResponse = this.Client.GetMapping<ElasticsearchProject>();
			mappingResponse.Should().NotBeNull();
			var mapping = mappingResponse.Mapping;
			mapping.Should().NotBeNull();
			mapping.Properties.Should().NotBeEmpty();
			var nameMapping = mapping.Properties["name"] as StringMapping;
			nameMapping.Should().NotBeNull();
			nameMapping.Name.Name.Should().NotBeNull().And.Equals("name");
			nameMapping.IndexName.Should().NotBeNullOrEmpty().And.Equals("my_crazy_name_i_want_in_lucene");
		}
		[Test]
		public void NumberProperty()
		{
			var result = this.Client.Map<ElasticsearchProject>(m => m
				.Properties(props => props
					.Number(s => s
						.Name(p => p.LOC)
						.IndexName("lines_of_code")
						.Type(NumberType.Integer)
						.NullValue(0)
						.Boost(2.0)
						.IgnoreMalformed()
						.IncludeInAll()
						.Index()
						.Store()
						.PrecisionStep(1)
					)
				)
			);
			this.DefaultResponseAssertations(result);
		}
		[Test]
		public void DateProperty()
		{
			var result = this.Client.Map<ElasticsearchProject>(m => m
				.Properties(props => props
					.Date(s => s
						.Name(p => p.StartedOn)
						.Format("dateOptionalTime")
						.IgnoreMalformed()
						.IncludeInAll()
						.Index()
						.IndexName("started_on_index_name")
						.NullValue(new DateTime(1986, 3, 8))
						.PrecisionStep(4)
						.Store()
						.Boost(1.3)
					)
				)
			);
			this.DefaultResponseAssertations(result);
		}
		[Test]
		public void BooleanProperty()
		{
			var result = this.Client.Map<ElasticsearchProject>(m => m
				.Properties(props => props
					.Boolean(s => s
						.Name(p => p.BoolValue) //reminder .Repository(string) exists too!
						.Boost(1.4)
						//.IncludeInAll() //makes no sense 
						//.Index() //since 0.90.5 this will throw an exception on elasticsearch.
						.IndexName("bool_name_in_lucene_index")
						.NullValue(false)
						.Store()
					)
				)
			);
			this.DefaultResponseAssertations(result);
		}
		[Test]
		public void BinaryProperty()
		{
			this.Client.DeleteMapping<ElasticsearchProject>();
			var result = this.Client.Map<ElasticsearchProject>(m => m
				.Properties(props => props
					.Binary(s => s
						.Name(p => p.MyBinaryField)
						.IndexName("binz")
					)
				)
			);
			this.DefaultResponseAssertations(result);
		}
		[Test, Ignore]
		//needs the attachment plugin
		public void AttachmentProperty()
		{
			var result = this.Client.Map<ElasticsearchProject>(m => m
				.Properties(props => props
					.Attachment(s => s
						.Name(p => p.MyAttachment)
						.FileField(fs => fs.Index(FieldIndexOption.NotAnalyzed).Store())
						.AuthorField(fs => fs.Index(FieldIndexOption.Analyzed).Store(false))
						.DateField(fs => fs.Store(false).IncludeInAll())
					)
				)
			);
			this.DefaultResponseAssertations(result);
		}
		[Test]
		public void ObjectProperty()
		{
			var result = this.Client.Map<ElasticsearchProject>(m => m
				.Properties(props => props
					.Object<Person>(s => s
						.Name(p => p.Followers.First())
						.Dynamic()
						.Enabled()
						.IncludeInAll()
						.MapFromAttributes()
						.Path("full")
						.Properties(pprops => pprops
							.String(ps => ps.Name(p => p.FirstName).Index(FieldIndexOption.NotAnalyzed))
						//etcetera
						)
					)
				)
			);
			this.DefaultResponseAssertations(result);
		}
		[Test]
		public void NestedObjectProperty()
		{
			var result = this.Client.Map<ElasticsearchProject>(m => m
				.Properties(props => props
					.NestedObject<Person>(s => s
						.Name(p => p.NestedFollowers.First())
						.Dynamic()
						.Enabled()
						.IncludeInAll()
						.IncludeInParent()
						.IncludeInRoot()
						.MapFromAttributes()
						.Path("full")
						.Properties(pprops => pprops
							.String(ps => ps.Name(p => p.FirstName).Index(FieldIndexOption.NotAnalyzed))
						//etcetera
						)
					)
				)
			);
			this.DefaultResponseAssertations(result);
		}
		[Test]
		public void MultiFieldProperty()
		{
			var result = this.Client.Map<ElasticsearchProject>(m => m
				.Properties(props => props
					.MultiField(s => s
						.Name(p => p.Name)
						.Fields(pprops => pprops
							.String(ps => ps.Name(p => p.Name).Index(FieldIndexOption.NotAnalyzed))
							.String(ps => ps.Name(p => p.Name.Suffix("searchable")).Index(FieldIndexOption.Analyzed))
						)
					)
				)
			);
			this.DefaultResponseAssertations(result);
		}

        [Test]
        public void MultiFieldPropertyWithFullNamePath()
        {
            var result = this.Client.Map<ElasticsearchProject>(m => m
                .Properties(props => props
                    .MultiField(s => s
                        .Path(MultiFieldMappingPath.Full)
                        .Name(p => p.Name)
                        .Fields(pprops => pprops
                            .String(ps => ps.Name(p => p.Name).Index(FieldIndexOption.NotAnalyzed))
                            .String(ps => ps.Name(p => p.Name.Suffix("searchable")).Index(FieldIndexOption.Analyzed))
                        )
                    )
                )
            );
            this.DefaultResponseAssertations(result);
        }

        [Test]
        public void MultiFieldPropertyWithJustNamePath()
        {
            var result = this.Client.Map<ElasticsearchProject>(m => m
                .Properties(props => props
                    .MultiField(s => s
                        .Path(MultiFieldMappingPath.JustName)
                        .Name(p => p.Name)
                        .Fields(pprops => pprops
                            .String(ps => ps.Name(p => p.Name).Index(FieldIndexOption.NotAnalyzed))
                            .String(ps => ps.Name(p => p.Name.Suffix("searchable")).Index(FieldIndexOption.Analyzed))
                        )
                    )
                )
            );
            this.DefaultResponseAssertations(result);
        }

		[Test]
		public void IPProperty()
		{
			var result = this.Client.Map<ElasticsearchProject>(m => m
				.Properties(props => props
					.IP(s => s
						.Name(p => p.PingIP)
						.Boost(0.7)
						.IncludeInAll()
						.NoIndex()
						.IndexName("ip")
						.NullValue("0.0.0.0")
						.PrecisionStep(4)
						.Store()
					)
				)
			);
			this.DefaultResponseAssertations(result);
		}
		[Test]
		public void GeoPointProperty()
		{
			var result = this.Client.Map<ElasticsearchProject>(m => m
				.Properties(props => props
					.GeoPoint(s => s
						.Name(p => p.Origin)
						.IndexGeoHash()
						.IndexLatLon()
						.GeoHashPrecision(12)
					)
				)
			);
			this.DefaultResponseAssertations(result);
		}
		[Test]
		[Ignore]
		//Need special libs in your elasticsearch folder to enable geoshape
		public void GeoShapeProperty()
		{
			var result = this.Client.Map<ElasticsearchProject>(m => m
				.Properties(props => props
					.GeoShape(s => s
						.Name(p => p.MyGeoShape)
						.Tree(GeoTree.Geohash)
						.TreeLevels(2)
						.DistanceErrorPercentage(0.025)
					)
				)
			);
			this.DefaultResponseAssertations(result);
		}

	}
}
