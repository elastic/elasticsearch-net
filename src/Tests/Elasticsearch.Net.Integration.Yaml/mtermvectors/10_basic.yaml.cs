using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.Mtermvectors1
{
	public partial class Mtermvectors1YamlTests
	{	
	
		public class Mtermvectors110BasicYamlBase : YamlTestsBase
		{
			public Mtermvectors110BasicYamlBase() : base()
			{	

				//do indices.create 
				_body = new {
					mappings= new {
						testtype= new {
							properties= new {
								text= new {
									type= "string",
									term_vector= "with_positions_offsets"
								}
							}
						}
					}
				};
				this.Do(()=> _client.IndicesCreate("testidx", _body));

				//do index 
				_body = new {
					text= "The quick brown fox is brown."
				};
				this.Do(()=> _client.Index("testidx", "testtype", "testing_document", _body));

				//do indices.refresh 
				this.Do(()=> _client.IndicesRefreshForAll());

			}
		}


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class BasicTestsForMultiTermvectorGet2Tests : Mtermvectors110BasicYamlBase
		{
			[Test]
			public void BasicTestsForMultiTermvectorGet2Test()
			{	

				//do mtermvectors 
				_body = new {
					docs= new dynamic[] {
						new {
							_index= "testidx",
							_type= "testtype",
							_id= "testing_document"
						}
					}
				};
				this.Do(()=> _client.Mtermvectors(_body, nv=>nv
					.AddQueryString("term_statistics", @"true")
				));

				//match _response.docs[0].term_vectors.text.terms.brown.term_freq: 
				this.IsMatch(_response.docs[0].term_vectors.text.terms.brown.term_freq, 2);

				//match _response.docs[0].term_vectors.text.terms.brown.ttf: 
				this.IsMatch(_response.docs[0].term_vectors.text.terms.brown.ttf, 2);

				//do mtermvectors 
				_body = new {
					docs= new dynamic[] {
						new {
							_index= "testidx",
							_type= "testtype",
							_id= "testing_document"
						}
					}
				};
				this.Do(()=> _client.Mtermvectors(_body, nv=>nv
					.AddQueryString("term_statistics", @"true")
				));

				//match _response.docs[0].term_vectors.text.terms.brown.term_freq: 
				this.IsMatch(_response.docs[0].term_vectors.text.terms.brown.term_freq, 2);

				//match _response.docs[0].term_vectors.text.terms.brown.ttf: 
				this.IsMatch(_response.docs[0].term_vectors.text.terms.brown.ttf, 2);

				//do mtermvectors 
				_body = new {
					docs= new dynamic[] {
						new {
							_type= "testtype",
							_id= "testing_document"
						}
					}
				};
				this.Do(()=> _client.Mtermvectors("testidx", _body, nv=>nv
					.AddQueryString("term_statistics", @"true")
				));

				//match _response.docs[0].term_vectors.text.terms.brown.term_freq: 
				this.IsMatch(_response.docs[0].term_vectors.text.terms.brown.term_freq, 2);

				//match _response.docs[0].term_vectors.text.terms.brown.ttf: 
				this.IsMatch(_response.docs[0].term_vectors.text.terms.brown.ttf, 2);

				//do mtermvectors 
				_body = new {
					docs= new dynamic[] {
						new {
							_id= "testing_document"
						}
					}
				};
				this.Do(()=> _client.Mtermvectors("testidx", "testtype", _body, nv=>nv
					.AddQueryString("term_statistics", @"true")
				));

				//match _response.docs[0].term_vectors.text.terms.brown.term_freq: 
				this.IsMatch(_response.docs[0].term_vectors.text.terms.brown.term_freq, 2);

				//match _response.docs[0].term_vectors.text.terms.brown.ttf: 
				this.IsMatch(_response.docs[0].term_vectors.text.terms.brown.ttf, 2);

				//do mtermvectors 
				this.Do(()=> _client.MtermvectorsGet("testidx", "testtype", nv=>nv
					.AddQueryString("term_statistics", @"true")
					.AddQueryString("ids", new [] {
						@"testing_document"
					})
				));

				//match _response.docs[0].term_vectors.text.terms.brown.term_freq: 
				this.IsMatch(_response.docs[0].term_vectors.text.terms.brown.term_freq, 2);

				//match _response.docs[0].term_vectors.text.terms.brown.ttf: 
				this.IsMatch(_response.docs[0].term_vectors.text.terms.brown.ttf, 2);

			}
		}
	}
}

