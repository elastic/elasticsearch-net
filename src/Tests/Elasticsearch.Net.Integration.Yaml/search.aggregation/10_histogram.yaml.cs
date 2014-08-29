using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.SearchAggregation1
{
	public partial class SearchAggregation1YamlTests
	{	
	
		public class SearchAggregation110HistogramYamlBase : YamlTestsBase
		{
			public SearchAggregation110HistogramYamlBase() : base()
			{	

				//do indices.create 
				_body = new {
					settings= new {
						number_of_replicas= "0"
					},
					mappings= new {
						test= new {
							properties= new {
								number= new {
									type= "integer"
								}
							}
						}
					}
				};
				this.Do(()=> _client.IndicesCreate("test_1", _body));

				//do cluster.health 
				this.Do(()=> _client.ClusterHealth(nv=>nv
					.AddQueryString("wait_for_status", @"green")
				));

			}
		}


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class BasicTest2Tests : SearchAggregation110HistogramYamlBase
		{
			[Test]
			public void BasicTest2Test()
			{	

				//do index 
				_body = new {
					number= "1"
				};
				this.Do(()=> _client.Index("test_1", "test", "1", _body));

				//do index 
				_body = new {
					number= "51"
				};
				this.Do(()=> _client.Index("test_1", "test", "2", _body));

				//do index 
				_body = new {
					number= "101"
				};
				this.Do(()=> _client.Index("test_1", "test", "3", _body));

				//do index 
				_body = new {
					number= "151"
				};
				this.Do(()=> _client.Index("test_1", "test", "4", _body));

				//do indices.refresh 
				this.Do(()=> _client.IndicesRefreshForAll());

				//do search 
				_body = new {
					aggs= new {
						histo= new {
							histogram= new {
								field= "number",
								interval= "50"
							}
						}
					}
				};
				this.Do(()=> _client.Search(_body));

				//match _response.hits.total: 
				this.IsMatch(_response.hits.total, 4);

				//length _response.aggregations.histo.buckets: 4; 
				this.IsLength(_response.aggregations.histo.buckets, 4);

				//match _response.aggregations.histo.buckets[0].key: 
				this.IsMatch(_response.aggregations.histo.buckets[0].key, 0);

				//is_false _response.aggregations.histo.buckets[0].key_as_string; 
				this.IsFalse(_response.aggregations.histo.buckets[0].key_as_string);

				//match _response.aggregations.histo.buckets[0].doc_count: 
				this.IsMatch(_response.aggregations.histo.buckets[0].doc_count, 1);

				//match _response.aggregations.histo.buckets[1].key: 
				this.IsMatch(_response.aggregations.histo.buckets[1].key, 50);

				//is_false _response.aggregations.histo.buckets[1].key_as_string; 
				this.IsFalse(_response.aggregations.histo.buckets[1].key_as_string);

				//match _response.aggregations.histo.buckets[1].doc_count: 
				this.IsMatch(_response.aggregations.histo.buckets[1].doc_count, 1);

				//match _response.aggregations.histo.buckets[2].key: 
				this.IsMatch(_response.aggregations.histo.buckets[2].key, 100);

				//is_false _response.aggregations.histo.buckets[2].key_as_string; 
				this.IsFalse(_response.aggregations.histo.buckets[2].key_as_string);

				//match _response.aggregations.histo.buckets[2].doc_count: 
				this.IsMatch(_response.aggregations.histo.buckets[2].doc_count, 1);

				//match _response.aggregations.histo.buckets[3].key: 
				this.IsMatch(_response.aggregations.histo.buckets[3].key, 150);

				//is_false _response.aggregations.histo.buckets[3].key_as_string; 
				this.IsFalse(_response.aggregations.histo.buckets[3].key_as_string);

				//match _response.aggregations.histo.buckets[3].doc_count: 
				this.IsMatch(_response.aggregations.histo.buckets[3].doc_count, 1);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class FormatTest3Tests : SearchAggregation110HistogramYamlBase
		{
			[Test]
			public void FormatTest3Test()
			{	

				//do index 
				_body = new {
					number= "1"
				};
				this.Do(()=> _client.Index("test_1", "test", "1", _body));

				//do index 
				_body = new {
					number= "51"
				};
				this.Do(()=> _client.Index("test_1", "test", "2", _body));

				//do index 
				_body = new {
					number= "101"
				};
				this.Do(()=> _client.Index("test_1", "test", "3", _body));

				//do index 
				_body = new {
					number= "151"
				};
				this.Do(()=> _client.Index("test_1", "test", "4", _body));

				//do indices.refresh 
				this.Do(()=> _client.IndicesRefreshForAll());

				//do search 
				_body = new {
					aggs= new {
						histo= new {
							histogram= new {
								field= "number",
								interval= "50",
								format= "Value is ##0.0"
							}
						}
					}
				};
				this.Do(()=> _client.Search(_body));

				//match _response.hits.total: 
				this.IsMatch(_response.hits.total, 4);

				//length _response.aggregations.histo.buckets: 4; 
				this.IsLength(_response.aggregations.histo.buckets, 4);

				//match _response.aggregations.histo.buckets[0].key: 
				this.IsMatch(_response.aggregations.histo.buckets[0].key, 0);

				//match _response.aggregations.histo.buckets[0].key_as_string: 
				this.IsMatch(_response.aggregations.histo.buckets[0].key_as_string, @"Value is 0.0");

				//match _response.aggregations.histo.buckets[0].doc_count: 
				this.IsMatch(_response.aggregations.histo.buckets[0].doc_count, 1);

				//match _response.aggregations.histo.buckets[1].key: 
				this.IsMatch(_response.aggregations.histo.buckets[1].key, 50);

				//match _response.aggregations.histo.buckets[1].key_as_string: 
				this.IsMatch(_response.aggregations.histo.buckets[1].key_as_string, @"Value is 50.0");

				//match _response.aggregations.histo.buckets[1].doc_count: 
				this.IsMatch(_response.aggregations.histo.buckets[1].doc_count, 1);

				//match _response.aggregations.histo.buckets[2].key: 
				this.IsMatch(_response.aggregations.histo.buckets[2].key, 100);

				//match _response.aggregations.histo.buckets[2].key_as_string: 
				this.IsMatch(_response.aggregations.histo.buckets[2].key_as_string, @"Value is 100.0");

				//match _response.aggregations.histo.buckets[2].doc_count: 
				this.IsMatch(_response.aggregations.histo.buckets[2].doc_count, 1);

				//match _response.aggregations.histo.buckets[3].key: 
				this.IsMatch(_response.aggregations.histo.buckets[3].key, 150);

				//match _response.aggregations.histo.buckets[3].key_as_string: 
				this.IsMatch(_response.aggregations.histo.buckets[3].key_as_string, @"Value is 150.0");

				//match _response.aggregations.histo.buckets[3].doc_count: 
				this.IsMatch(_response.aggregations.histo.buckets[3].doc_count, 1);

			}
		}
	}
}

