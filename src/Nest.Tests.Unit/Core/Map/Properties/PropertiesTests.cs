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
			var result = this._client.MapFluent<ElasticSearchProject>(m => m
				.Properties(props => props
					.String(s => s
						.Name(p => p.Name)
						.IndexName("my_crazy_name_i_want_in_lucene")
						.IncludeInAll()
						.Index(FieldIndexOption.analyzed)
						.IndexAnalyzer("standard")
						.IndexOptions(IndexOptions.positions)
						.NullValue("my_special_null_value")
						.OmitNorms()
						.OmitTermFrequencyAndPositions()
						.PositionOffsetGap(1)
						.SearchAnalyzer("standard")
						.Store()
						.TermVector(TermVectorOption.with_positions_offsets)
						.Boost(1.1)
					)
				)
			);
			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod());
		}
		[Test]
		public void NumberProperty()
		{
			var result = this._client.MapFluent<ElasticSearchProject>(m => m
				.Properties(props => props
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
				)
			);
			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod());
		}
		[Test]
		public void DateProperty()
		{
			var result = this._client.MapFluent<ElasticSearchProject>(m => m
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
			var result = this._client.MapFluent<ElasticSearchProject>(m => m
				.Properties(props => props
					.Boolean(s => s
						.Name(p => p.BoolValue) //reminder .Name(string) exists too!
						.Boost(1.4)
						.IncludeInAll()
						.Index()
						.IndexName("bool_name_in_lucene_index")
						.NullValue(false)
						.Store()
					)
				)
			);
			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod());
		}
		[Test]
		public void BinaryProperty()
		{
			var result = this._client.MapFluent<ElasticSearchProject>(m => m
				.Properties(props => props
					.Binary(s => s
						.Name(p => p.MyBinaryField)
						.IndexName("binz")
					)
				)
			);
			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod());
		}
		[Test]
		public void AttachmentProperty()
		{
			var result = this._client.MapFluent<ElasticSearchProject>(m => m
				.Properties(props => props
					.Attachment(s => s
						.Name(p => p.MyAttachment)
						.FileField(fs => fs.Index(FieldIndexOption.not_analyzed).Store())
						.AuthorField(fs => fs.Index(FieldIndexOption.analyzed).Store(false))
						.DateField(fs => fs.Store(false).IncludeInAll())
					)
				)
			);
			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod());
		}
		[Test]
		public void ObjectProperty()
		{
			var result = this._client.MapFluent<ElasticSearchProject>(m => m
				.Properties(props => props
					.Object<Person>(s => s
						.Name(p => p.Followers.First())
						.Dynamic()
						.Enabled()
						.IncludeInAll()
						.Path("full")
						.Properties(pprops => pprops
							.String(ps => ps.Name(p => p.FirstName).Index(FieldIndexOption.not_analyzed))
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
			var result = this._client.MapFluent<ElasticSearchProject>(m => m
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
							.String(ps => ps.Name(p => p.FirstName).Index(FieldIndexOption.not_analyzed))
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
			var result = this._client.MapFluent<ElasticSearchProject>(m => m
				.Properties(props => props
					.MultiField(s => s
						.Name(p => p.Name)
						.Fields(pprops => pprops
							.String(ps => ps.Name(p => p.Name).Index(FieldIndexOption.not_analyzed))
							.String(ps => ps.Name(p => p.Name.Suffix("searchable")).Index(FieldIndexOption.analyzed))
						)
					)
				)
			);
			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod());
		}
		[Test]
		public void IPProperty()
		{
			var result = this._client.MapFluent<ElasticSearchProject>(m => m
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
		[Test]
		public void GeoPointProperty()
		{
			var result = this._client.MapFluent<ElasticSearchProject>(m => m
				.Properties(props => props
					.GeoPoint(s => s
						.Name(p => p.Origin)
						.IndexGeoHash()
						.IndexLatLon()
						.GeoHashPrecision(12)
					)
				)
			);
			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod());
		}
		[Test]
		public void GeoShapeProperty()
		{
			var result = this._client.MapFluent<ElasticSearchProject>(m => m
				.Properties(props => props
					.GeoShape(s => s
						.Name(p => p.MyGeoShape)
						.Tree(GeoTree.geohash)
						.TreeLevels(2)
						.DistanceErrorPercentage(0.025)
					)
				)
			);
			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod());
		}

	}
}
