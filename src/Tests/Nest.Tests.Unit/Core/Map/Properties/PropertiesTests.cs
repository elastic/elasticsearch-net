using System;
using System.Linq;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;
using System.Reflection;

namespace Nest.Tests.Unit.Core.Map.Properties
{
	[TestFixture]
	public class PropertiesTests : BaseJsonTests
	{
		[Test]
		public void StringProperty()
		{
			var result = this._client.Map<ElasticsearchProject>(m => m
				.Properties(props => props
					.String(s => s
						.Name(p => p.Name)
						.Similarity("mysimilarity")
						.IndexName("my_crazy_name_i_want_in_lucene")
						.IncludeInAll()
						.Index(FieldIndexOption.Analyzed)
						.IndexAnalyzer("standard")
						.IndexOptions(IndexOptions.Positions)
						.NullValue("my_special_null_value")
						.OmitNorms()
						.Norms(new NormsMapping { Enabled = true, Loading = NormsLoading.Eager})
						.PositionOffsetGap(1)
						.SearchAnalyzer("standard")
						.Store()
						.TermVector(TermVectorOption.WithPositionsOffsets)
						.Boost(1.1)
						.CopyTo(p => p.Content, p => p.Country)
						.IgnoreAbove(20)
						.FieldData(fd => fd
							.Format(FieldDataStringFormat.Fst)
							.Loading(FieldDataLoading.Eager)
							.Filter(fdf => fdf
								.Frequency(freq => freq
									.Min(0.001)
									.Max(0.1)
									.MinSegmentSize(500)
								)
								.Regex(rx => rx
									.Pattern("^#.*")
								)
							)
						)
					)
				)
			);
			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod());
		}
		[Test]
		public void NumberProperty()
		{
			var result = this._client.Map<ElasticsearchProject>(m => m
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
						.FieldData(fd => fd
							.Format(FieldDataNonStringFormat.DocValues)
							.Loading(FieldDataLoading.Eager)
						)
					)
				)
			);
			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod());
		}
		[Test]
		public void DateProperty()
		{
			var result = this._client.Map<ElasticsearchProject>(m => m
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
			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod());
		}
		[Test]
		public void BooleanProperty()
		{
			var result = this._client.Map<ElasticsearchProject>(m => m
				.Properties(props => props
					.Boolean(s => s
						.Name(p => p.BoolValue) //reminder .Repository(string) exists too!
						.Boost(1.4)
						.IncludeInAll()
						.Index()
						.IndexName("bool_name_in_lucene_index")
						.NullValue(false)
						.Store()
						.CopyTo(p => p.Content)
					)
				)
			);
			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod());
		}
		[Test]
		public void BinaryProperty()
		{
			var result = this._client.Map<ElasticsearchProject>(m => m
				.Properties(props => props
					.Binary(s => s
						.Name(p => p.MyBinaryField)
						.IndexName("binz")
						.CopyTo("another_field")
					)
				)
			);
			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod());
		}
		[Test]
		public void AttachmentProperty()
		{
			var result = this._client.Map<ElasticsearchProject>(m => m
				.Properties(props => props
					.Attachment(s => s
						.Name(p => p.MyAttachment)
						.FileField(fs => fs.Index(FieldIndexOption.NotAnalyzed).Store())
						.TitleField(fs => fs.Index(FieldIndexOption.NotAnalyzed).Store(false))
						.MetadataField("contents", fs => fs.Index(FieldIndexOption.NotAnalyzed).Store(false))
						.AuthorField(fs => fs.Index(FieldIndexOption.Analyzed).Store(false))
						.DateField(fs => fs.Store(false).IncludeInAll())
					)
				)
			);
			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod());
		}
		[Test]
		public void ObjectProperty()
		{
			var result = this._client.Map<ElasticsearchProject>(m => m
				.Properties(props => props
					.Object<Person>(s => s
						.Name(p => p.Followers.First())
						.Dynamic()
						.Enabled()
						.IncludeInAll()
						.Path("full")
						.Properties(pprops => pprops
							.String(ps => ps.Name(p => p.FirstName).Index(FieldIndexOption.NotAnalyzed))
						//etcetera
						)
					)
				)
			);
			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod());
		}
		[Test]
		public void NestedObjectProperty()
		{
			var result = this._client.Map<ElasticsearchProject>(m => m
				.Properties(props => props
					.NestedObject<Person>(s => s
						.Name(p => p.NestedFollowers.First())
						.Dynamic()
						.Enabled()
						.IncludeInAll()
						.IncludeInParent()
						.IncludeInRoot()
						.Path("full")
						.Properties(pprops => pprops
							.String(ps => ps.Name(p => p.FirstName).Index(FieldIndexOption.NotAnalyzed))
						//etcetera
						)
					)
				)
			);
			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod());
		}
		[Test]
		public void MultiFieldProperty()
		{
			var result = this._client.Map<ElasticsearchProject>(m => m
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
			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod());
		}

        [Test]
        public void MultiFieldPropertyWithFullPath()
        {
            var result = this._client.Map<ElasticsearchProject>(m => m
                .Properties(props => props
                    .MultiField(s => s
                        .Name(p => p.Name)
                        .Path(MultiFieldMappingPath.Full)
                        .Fields(pprops => pprops
                            .String(ps => ps.Name(p => p.Name).Index(FieldIndexOption.NotAnalyzed))
                            .String(ps => ps.Name(p => p.Name.Suffix("searchable")).Index(FieldIndexOption.Analyzed))
                        )
                    )
                )
            );
            this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod());
        }

        [Test]
        public void MultiFieldPropertyWithJustNamePath()
        {
            var result = this._client.Map<ElasticsearchProject>(m => m
                .Properties(props => props
                    .MultiField(s => s
                        .Name(p => p.Name)
                        .Path(MultiFieldMappingPath.JustName)
                        .Fields(pprops => pprops
                            .String(ps => ps.Name(p => p.Name).Index(FieldIndexOption.NotAnalyzed))
                            .String(ps => ps.Name(p => p.Name.Suffix("searchable")).Index(FieldIndexOption.Analyzed))
                        )
                    )
                )
            );
            this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod());
        }

		[Test]
		public void IPProperty()
		{
			var result = this._client.Map<ElasticsearchProject>(m => m
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
			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod());
		}

		public class Foo
		{
			public int Id { get; set; }
			[ElasticProperty(AddSortField = true, SortAnalyzer = "simple")]
			public string Name { get; set; }
		}

		[Test]
		public void SortAnalyzeryReadFromAttribute()
		{
			var result = _client.Map<Foo>(m => m.MapFromAttributes());
			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod());

		}

		[Test]
		public void GeoPointProperty()
		{
			var result = this._client.Map<ElasticsearchProject>(m => m
				.Properties(props => props
					.GeoPoint(s => s
						.Name(p => p.Origin)
						.IndexGeoHash()
						.IndexLatLon()
						.GeoHashPrecision(12)
						.FieldData(fd => fd
							.Format(FieldDataNonStringFormat.Array)
							.Loading(FieldDataLoading.EagerGlobalOrdinals)
							.Filter(fdf => fdf
								.Frequency(freq => freq
									.Min(0.001)
									.Max(0.1)
								)
							)
						)
					)
				)
			);
			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod());
		}

		[Test]
		public void GeoShapeProperty()
		{
			var result = this._client.Map<ElasticsearchProject>(m => m
				.Properties(props => props
					.GeoShape(s => s
						.Name(p => p.MyGeoShape)
						.Tree(GeoTree.Geohash)
						.TreeLevels(2)
						.DistanceErrorPercentage(0.025)
					)
				)
			);
			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod());
		}

		public class DocValuesFoo
		{
			public int Id { get; set; }
			[ElasticProperty(DocValues=true)]
			public int Value { get; set; }
		}

		[Test]
		public void DocValuesProperty()
		{
			var result = this._client.Map<DocValuesFoo>(m => m.MapFromAttributes());
			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod());

			var result2 = this._client.Map<DocValuesFoo>(m => m
				.Properties(props => props
					.Number(nmd => nmd
						.Name("id")
						.Type(NumberType.Integer))
					.Number(nmd => nmd
						.Name("value")
						.Type(NumberType.Integer)
						.DocValues())));
			this.JsonEquals(result2.ConnectionStatus.Request, MethodInfo.GetCurrentMethod());
		}

		[Test]
		public void CompletionProperty()
		{
			var result = this._client.Map<ElasticsearchProject>(m => m
				.Properties(props => props
					.Completion(c => c
						.Name(p => p.Suggest)
						.IndexAnalyzer("simple")
						.SearchAnalyzer("simple")
						.Payloads()
						.MaxInputLength(25)
						.Context(cxt => cxt
							.Category("color", cc => cc
								.Path("color_field")
								.Default("red", "green", "blue")
							)
							.GeoLocation("location", lc => lc
								.Precision("5m")
								.Neighbors()
								.Default("u33")
							)
						)
					)
				)
			);
			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod());
		}
	}
}
