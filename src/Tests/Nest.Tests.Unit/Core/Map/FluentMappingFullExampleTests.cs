using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using NUnit.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Nest;
using Newtonsoft.Json.Converters;
using Nest.Resolvers.Converters;
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
				.DisableAllField(false)
				.DisableIndexField(false)
				.DisableSizeField(false)
				.Dynamic()
				.Enabled()
				.SourceField(s=>s
					.SetDisabled(false)
					.SetExcludes(new [] {"anyfromthis.prop.*"})
				)
				.IncludeInAll()
				.Path("full")
				.IdField(i => i
					.SetIndex("not_analyzed")
					.SetPath("myOtherId")
					.SetStored(false)
				)
				.SourceField(s => s
					.SetDisabled()
					.SetCompression()
					.SetCompressionTreshold("200b")
					.SetExcludes(new[] { "path1.*" })
					.SetIncludes(new[] { "path2.*" })
				)
				.TypeField(t => t
					.SetIndexed()
					.SetStored()
				)
				.AnalyzerField(a => a
					.SetPath(p => p.Name)
					.SetIndexed()
				)
				.BoostField(b => b
					.SetName(p => p.LOC)
					.SetNullValue(1.0)
				)
				.RoutingField(r => r
					.SetPath(p => p.Country)
					.SetRequired()
				)
				.TimestampField(t => t
					.SetDisabled(false)
					.SetPath(p => p.StartedOn)
				)
				.TtlField(t => t
					.SetDisabled(false)
					.SetDefault("1d")
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
					.String(s=>s
						.Name("_all")
						.IndexAnalyzer("nGram_analyzer")
						.SearchAnalyzer("whitespace_analyzer")
					)
					.String(s => s
						.Name(p => p.Name)
						.IndexName("my_crazy_name_i_want_in_lucene")
						.IncludeInAll()
						.Index(FieldIndexOption.analyzed)
						.IndexAnalyzer("standard")
						.IndexOptions(IndexOptions.positions)
						.NullValue("my_special_null_value")
						.OmitNorms()
						.PositionOffsetGap(1)
						.SearchAnalyzer("standard")
						.Store()
						.TermVector(TermVectorOption.with_positions_offsets)
						.Boost(1.1)
					)
					.Number(s => s
						.Name(p => p.LOC)
						.IndexName("lines_of_code")
						.Type(NumberType.@integer)
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
						.Name(p => p.BoolValue) //reminder .Name(string) exists too!
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
						.FileField(fs => fs.Index(FieldIndexOption.not_analyzed).Store())
						.AuthorField(fs => fs.Index(FieldIndexOption.analyzed).Store(false))
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
							.String(ps => ps.Name(p => p.FirstName).Index(FieldIndexOption.not_analyzed))
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
							.String(ps => ps.Name(p => p.FirstName).Index(FieldIndexOption.not_analyzed))
							//etcetera
						)
					)
					.MultiField(s => s
						.Name(p => p.Name)
						.Fields(pprops => pprops
							.String(ps => ps.Name(p => p.Name).Index(FieldIndexOption.not_analyzed))
							.String(ps => ps.Name(p => p.Name.Suffix("searchable")).Index(FieldIndexOption.analyzed))
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
						.Tree(GeoTree.geohash)
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
					)
				)
			);

            Assert.NotNull(result.ConnectionStatus.Request);
		}


	}
}
