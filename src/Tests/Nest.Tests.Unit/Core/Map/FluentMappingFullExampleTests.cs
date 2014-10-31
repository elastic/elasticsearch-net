using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Core.Map
{
	[TestFixture]
	public class FluentMappingFullExampleTests : BaseJsonTests
	{
		[Test]
		public void MapFluentFull()
		{
			//most of these merely specify the defaults and are superfluous
			//No asserts just a global overview of what the fluent mapping is capable off.
			
			var result = this._client.Map<ElasticsearchProject>(m => m
				.Type("elasticsearchprojects2")
				.Indices("nest_test_data", "nest_test_data_clone")
				.IgnoreConflicts()
				.IndexAnalyzer("standard")
				.SearchAnalyzer("standard")
				.DynamicDateFormats(new[] { "dateOptionalTime", "yyyy/MM/dd HH:mm:ss Z||yyyy/MM/dd Z" })
				.DateDetection(true)
				.NumericDetection(true)
				//MapFromAttributes() is shortcut to fill property mapping using the types' attributes and properties
				//Allows us to map the exceptions to the rule and be less verbose.
				.MapFromAttributes()
				.SetParent<Person>() //makes no sense but i needed a type :)
				.AllField(a=>a
					.Enabled() 
					.IndexAnalyzer("nGram_analyzer")
					.SearchAnalyzer("whitespace_analyzer")
					.TermVector(TermVectorOption.WithPositionsOffsets)
				)
				.DisableIndexField(false)
				.DisableSizeField(false)
				.Dynamic()
				.Enabled()
				.SourceField(s=>s
					.Enabled()
					.Excludes(new [] {"anyfromthis.prop.*"})
				)
				.IncludeInAll()
				.Path("full")
				.IdField(i => i
					.Index("not_analyzed")
					.Path("myOtherId")
					.Store(false)
				)
				.SourceField(s => s
					.Enabled(false)
					.Compress()
					.CompressionThreshold("200b")
					.Excludes(new[] { "path1.*" })
					.Includes(new[] { "path2.*" })
				)
				.TypeField(t => t
					.Index()
					.Store()
				)
				.AnalyzerField(a => a
					.Path(p => p.Name)
					.Index()
				)
				.BoostField(b => b
					.Name(p => p.LOC)
					.NullValue(1.0)
				)
				.RoutingField(r => r
					.Path(p => p.Country)
					.Required()
				)
				.TimestampField(t => t
					.Enabled()
					.Path(p => p.StartedOn)
				)
				.TtlField(t => t
					.Enable(false)
					.Default("1d")
				)
				.Meta(d=>d
					.Add("attr1", "value1")
					.Add("attr2", new { attr3 = "value3" })
				)
				
				.DynamicTemplates(d=>d
					.Add(t=>t
						.Name("template_1")
						.Match("multi*")
						.Mapping(tm=>tm
							.MultiField(mf=>mf
								.Fields(mff=>mff
									.Generic(g => g
										.Name("{name}")
										.Type("{dynamic_type}")
										.Index("analyzed")
										.Store(false)
									)
									.Generic(g => g
										.Name("org")
										.Type("{dynamic_type}")
										.Index("not_analyzed")
										.Store()
									)
									.Generic(g => g
										.Name("do_no_render_name_property", noNameProperty: true)
										.Type("{dynamic_type}")
										.Index("not_analyzed")
										.Store()
									)
								)							
							)
						)
					)
				)
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
						.PositionOffsetGap(1)
						.SearchAnalyzer("standard")
						.Store()
						.TermVector(TermVectorOption.WithPositionsOffsets)
						.Boost(1.1)
						.CopyTo(p => p.Content)
					)
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
					.Boolean(s => s
						.Name(p => p.BoolValue) //reminder .Repository(string) exists too!
						.Boost(1.4)
						.IncludeInAll()
						.Index()
						.IndexName("bool_name_in_lucene_index")
						.NullValue(false)
						.Store()
					)
					.Binary(s => s
						.Name(p => p.MyBinaryField)
						.IndexName("binz")
					)
					.Attachment(s => s
						.Name(p => p.MyAttachment)
						.FileField(fs => fs.Index(FieldIndexOption.NotAnalyzed).Store())
						.AuthorField(fs => fs.Index(FieldIndexOption.Analyzed).Store(false))
						.DateField(fs => fs.Store(false).IncludeInAll())
					)
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
					.MultiField(s => s
						.Name(p => p.Name)
						.Fields(pprops => pprops
							.String(ps => ps.Name(p => p.Name).Index(FieldIndexOption.NotAnalyzed))
							.String(ps => ps.Name(p => p.Name.Suffix("searchable")).Index(FieldIndexOption.Analyzed))
						)
					)
					.IP(s=>s
						.Name(p=>p.PingIP)
						.Boost(0.7)
						.IncludeInAll()
						.NoIndex()
						.IndexName("ip")
						.NullValue("0.0.0.0")
						.PrecisionStep(4)
						.Store()
					)
					.GeoPoint(s=>s
						.Name(p=>p.Origin)
						.IndexGeoHash()
						.IndexLatLon()
						.GeoHashPrecision(12)
					)
					.GeoShape(s => s
						.Name(p => p.MyGeoShape)
						.Tree(GeoTree.Geohash)
						.TreeLevels(2)
						.DistanceErrorPercentage(0.025)
					)
					.Completion(s=>s
						.Name(p=>p.Name.Suffix("completion"))
						.IndexAnalyzer("standard")
						.SearchAnalyzer("standard")
						.MaxInputLength(20)
						.Payloads()
						.PreservePositionIncrements()
						.PreserveSeparators()
						.Context(c => c
							.Category("color", cc => cc
								.Path(p => p.Country)
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

            Assert.NotNull(result.ConnectionStatus.Request);
		}


	}
}
