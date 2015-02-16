using FluentAssertions;
using System.Reflection;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;
using System.Text;
using System.IO;

namespace Nest.Tests.Unit.Search.Sorting
{
	[TestFixture]
	public class SortTests : BaseJsonTests
	{
		[Test]
		public void TestSort()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Sort(sort => sort
					.OnField(e => e.Country)
					.MissingLast()
					.Descending()
					.IgnoreUnmappedFields(true)
				);
			var json = TestElasticClient.Serialize(s);
			var expected = @"
                {
                  from: 0,
                  size: 10,
                  sort: [
                    {
					  country: {
					    ignore_unmapped: true,
                        missing: ""_last"",
                        order: ""desc""
					  }
                    }
                  ]
                }";
			Assert.True(json.JsonEquals(expected), json);
		}

		[Test]
		public void TestSortWithUnmappedType()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Sort(sort => sort
					.OnField(e => e.DoubleValue)
					.UnmappedType(FieldType.Long)
				);
			var expected = @"
                {
                  from: 0,
                  size: 10,
                  sort: [
                    {
                        doubleValue : { unmapped_type : ""long"" } 
                    }
                  ]
                }";
			var json = TestElasticClient.Serialize(s);
			Assert.True(json.JsonEquals(expected), json);
		}

		[Test]
		public void TestSortOnSortField()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Sort(sort => sort
					.OnField(e => e.Name.Suffix("sort"))
					.MissingLast()
					.Descending()
					.Mode(SortMode.Min)
				);
			var json = TestElasticClient.Serialize(s);
			var expected = @"
                {
                  from: 0,
                  size: 10,
                  sort: [
					 {
                      ""name.sort"": {
                          missing: ""_last"",
                          order: ""desc"",
					      mode: ""min""
                        }
					 }
                   ]
                }";
			Assert.True(json.JsonEquals(expected), json);
		}

		[Test]
		public void TestSortOnNestedField()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Sort(sort => sort
					.OnField(e => e.Contributors.Suffix("age")) // Sort projects by oldest contributor
					.NestedMax()
					.Descending()
				);
			var json = TestElasticClient.Serialize(s);
			var expected = @"
                {
                  from: 0,
                  size: 10,
                  sort: [
                    {
					  ""contributors.age"": {
						  ""order"": ""desc"",
                          ""mode"": ""max"" 
				      }
					}
                  ]
                }";
			Assert.True(json.JsonEquals(expected), json);
		}

		[Test]
		public void TestSortAscending()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.SortAscending(f => f.Country);
			var json = TestElasticClient.Serialize(s);
			var expected = @"
                {
                  from: 0,
                  size: 10,
                  sort: [
					  { country : { order: ""asc"" } }
                    ]      
                }";
			Assert.True(json.JsonEquals(expected), json);
		}

		[Test]
		public void TestSortDescending()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.SortDescending(f => f.Country);
			var json = TestElasticClient.Serialize(s);
			var expected = @"
                {
                  from: 0,
                  size: 10,
                  sort: [
                      { country : { order: ""desc"" } }
                    ]      
                }";
			Assert.True(json.JsonEquals(expected), json);
		}

		[Test]
		public void TestSortAscendingOnSortField()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.SortAscending(f => f.Name.Suffix("sort"));
			var json = TestElasticClient.Serialize(s);
			var expected = @"
                {
                  from: 0,
                  size: 10,
                  sort: [
                      {""name.sort"" : { order: ""asc"" } }
                    ]      
                }";
			Assert.True(json.JsonEquals(expected), json);
		}

		[Test]
		public void TestSortDescendingOnSortField()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.SortDescending(f => f.Name.Suffix("sort"));
			var json = TestElasticClient.Serialize(s);
			var expected = @"
                {
                  from: 0,
                  size: 10,
                  sort: [
                      { ""name.sort"" : { order: ""desc"" } }
                    ]      
                }";
			Assert.True(json.JsonEquals(expected), json);
		}

		[Test]
		public void TestSortGeo()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.SortGeoDistance(sort => sort
					.OnField(e => e.Origin)
					.MissingLast()
					.Descending()
					.PinTo(40, -70)
					.Unit(GeoUnit.Kilometers)
					.Mode(SortMode.Max)
				);
			var json = TestElasticClient.Serialize(s);
			var expected = @"
                {
                  from: 0,
                  size: 10,
                  sort: [
					{
					  _geo_distance: {
					   ""origin"": ""40, -70"",
						 missing: ""_last"",
						 mode: ""max"",
						 order: ""desc"",
						 unit: ""km""
					  }
					}
                  ]
                }";
			Assert.True(json.JsonEquals(expected), json);
		}

		[Test]
		public void TestSortGeoMultiple()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.SortGeoDistance(sort => sort
					.OnField(e => e.Origin)
					.MissingLast()
					.Descending()
					.PinTo(GeoLocation.TryCreate(40, -70),GeoLocation.TryCreate(30.21, 1.21))
					.Unit(GeoUnit.Miles)
					.Mode(SortMode.Average)
				);
			var json = TestElasticClient.Serialize(s);
			var expected = @"
                {
                  from: 0,
                  size: 10,
                  sort: [
					{
					  _geo_distance: {
					   ""origin"": [""40,-70"",""30.21,1.21""],
						 missing: ""_last"",
						 mode: ""avg"",
						 order: ""desc"",
						 unit: ""mi""
					  }
					}
                  ]
                }";
			Assert.True(json.JsonEquals(expected), json);
		}

		[Test]
		public void TestSortScript()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.SortScript(sort => sort
					.MissingLast()
					.Descending()
					.Mode(SortMode.Average)
					.Script("doc['field_name'].value * factor")
					.Params(p => p
						.Add("factor", 1.1)
					)
					.Language("native")
					.Type("number")
				);
			var json = TestElasticClient.Serialize(s);
			var expected = @"
                {
                  from: 0,
                  size: 10,
                  sort: [
                   {
                      _script: {
                        type: ""number"",
                        script: ""doc['field_name'].value * factor"",
                        params: {
                          factor: 1.1
                        },
                        missing: ""_last"",
                        order: ""desc"",
                        mode: ""avg"",
                        lang: ""native""
                      }
                    }
                  ]
                }";
			Assert.True(json.JsonEquals(expected), json);
		}

		[Test]
		public void TestSortScriptFile()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.SortScript(sort => sort
					.MissingLast()
					.Descending()
					.Mode(SortMode.Average)
					.File("SortScript")
					.Params(p => p
						.Add("factor", 1.1)
					)
					.Language("native")
					.Type("number")
				);
			var json = TestElasticClient.Serialize(s);
			var expected = @"
                {
                  from: 0,
                  size: 10,
                  sort: [
                   {
                      _script: {
                        type: ""number"",
                        params: {
                          factor: 1.1
                        },
                        missing: ""_last"",
                        order: ""desc"",
                        mode: ""avg"",
                        lang: ""native"",
						file: ""SortScript"",
                      }
                    }
                  ]
                }";
			Assert.True(json.JsonEquals(expected), json);
		}

		[Test]
		public void TestNestedFilter()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.Sort(sort => sort
					.OnField(e => e.Id)
					.NestedFilter(f => f.Term("name", "value"))
				);
			var json = TestElasticClient.Serialize(s);
			var expected = @"
                {
                ""sort"": [
                    {
					""id"": {
                      ""nested_filter"": {
                        ""term"": {
                          ""name"": ""value""
                          }
                        }
                      }
				    }
                   ]
                 }";
			Assert.True(json.JsonEquals(expected), json);
		}

		[Test]
		public void TestNestedPath()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.Sort(sort => sort
					.OnField(e => e.Id)
					.NestedPath("name")
				);
			var json = TestElasticClient.Serialize(s);
			var expected = @"
                {
                  ""sort"": [
                    {
					  ""id"": {
					    ""nested_path"": ""name""
					  }
					}
                  ]
                }";
			Assert.True(json.JsonEquals(expected), json);
		}

		[Test]
		public void TestScriptNestedFilter()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.SortScript(sort => sort
					.Script("script")
					.NestedFilter(f => f.Term("name", "value"))
				);
			var json = TestElasticClient.Serialize(s);
			var expected = @"
				{
				  ""sort"": [
					{
					  ""_script"": {
						""script"": ""script"",
						""nested_filter"": {
						  ""term"": {
							""name"": ""value""
						  }
						}
					  }
					}
				  ]
				}";
			Assert.True(json.JsonEquals(expected), json);
		}

		[Test]
		public void TestScriptNestedPath()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.SortScript(sort => sort
					.Script("script")
					.NestedPath("name")
				);
			var json = TestElasticClient.Serialize(s);
			var expected = @"
				{
				  ""sort"": [
					{
					  ""_script"": {
						""script"": ""script"",
						""nested_path"": ""name""
					  }
					}
				  ]
				}";
			Assert.True(json.JsonEquals(expected), json);
		}

		[Test]
		public void TestNestedPathObject()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.Sort(sort => sort
					.OnField(e => e.Id)
					.NestedPath(f => f.Name)
				);
			var json = TestElasticClient.Serialize(s);
			var expected = @"
                {
                  ""sort"": [
                    {
					  ""id"": {
						""nested_path"": ""name""
					  }
					}
                  ]
                }";
			Assert.True(json.JsonEquals(expected), json);
		}
		[Test]
		public void RandomScriptSort()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
			  .From(0)
			  .Size(10)
			  .SortScript(sort => sort
				.Ascending()
				.Script("(doc['_id'].stringValue + salt).hashCode()")
				.Params(p => p
				  .Add("salt", "some_random_string")
				)
				.Type("number")
			);
			this.JsonEquals(s, MethodInfo.GetCurrentMethod());
		}

		[Test]
		public void SortSerializeThenDeserializeTest()
		{
			var d1 = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(20)
				.MatchAll()
				.Sort(s => s
					.OnField(p => p.Id)
					.Order(SortOrder.Ascending)
				)
				.SortGeoDistance(s => s
					.OnField(p => p.Origin)
					.PinTo(40, 70)
				)
				.SortScript(s => s
					.Ascending()
					.Script("(doc['_id'].stringValue + salt).hashCode()")
					.Params(p => p
						.Add("salt", "some_random_string")
					)
					.Type("number")
				);

			var s1 = this._client.Serializer.Serialize(d1);
			var d2 = this._client.Serializer.Deserialize<SearchDescriptor<ElasticsearchProject>>(new MemoryStream(s1));
			var s2 = this._client.Serializer.Serialize(d2);

			var s1Json = Encoding.UTF8.GetString(s1);
			var s2Json = Encoding.UTF8.GetString(s2);

			s1Json.JsonEquals(s2Json).Should().BeTrue();
		}
	}
}