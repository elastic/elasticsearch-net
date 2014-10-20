using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.Search6
{
	public partial class Search6YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class DefaultIndex1Tests : YamlTestsBase
		{
			[Test]
			public void DefaultIndex1Test()
			{	

				//do indices.create 
				_body = new {
					settings= new {
						number_of_shards= "1"
					}
				};
				this.Do(()=> _client.IndicesCreate("goodbad", _body));

				//do index 
				_body = new {
					text= "good",
					@class= "good"
				};
				this.Do(()=> _client.Index("goodbad", "doc", "1", _body));

				//do index 
				_body = new {
					text= "good",
					@class= "good"
				};
				this.Do(()=> _client.Index("goodbad", "doc", "2", _body));

				//do index 
				_body = new {
					text= "bad",
					@class= "bad"
				};
				this.Do(()=> _client.Index("goodbad", "doc", "3", _body));

				//do index 
				_body = new {
					text= "bad",
					@class= "bad"
				};
				this.Do(()=> _client.Index("goodbad", "doc", "4", _body));

				//do index 
				_body = new {
					text= "good bad",
					@class= "good"
				};
				this.Do(()=> _client.Index("goodbad", "doc", "5", _body));

				//do index 
				_body = new {
					text= "good bad",
					@class= "bad"
				};
				this.Do(()=> _client.Index("goodbad", "doc", "6", _body));

				//do index 
				_body = new {
					text= "bad",
					@class= "bad"
				};
				this.Do(()=> _client.Index("goodbad", "doc", "7", _body));

				//do indices.refresh 
				this.Do(()=> _client.IndicesRefresh("goodbad"));

				//do search 
				this.Do(()=> _client.SearchGet("goodbad", "doc"));

				//match _response.hits.total: 
				this.IsMatch(_response.hits.total, 7);

				//do search 
				_body = new {
					aggs= new {
						@class= new {
							terms= new {
								field= "class"
							},
							aggs= new {
								sig_terms= new {
									significant_terms= new {
										field= "text"
									}
								}
							}
						}
					}
				};
				this.Do(()=> _client.Search("goodbad", "doc", _body));

				//match _response.aggregations.@class.buckets[0].sig_terms.buckets[0].key: 
				this.IsMatch(_response.aggregations.@class.buckets[0].sig_terms.buckets[0].key, @"bad");

				//match _response.aggregations.@class.buckets[1].sig_terms.buckets[0].key: 
				this.IsMatch(_response.aggregations.@class.buckets[1].sig_terms.buckets[0].key, @"good");

			}
		}
	}
}

