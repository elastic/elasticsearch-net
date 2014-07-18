using System.Reflection;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;

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
					    mode: ""avg""
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
	}
}