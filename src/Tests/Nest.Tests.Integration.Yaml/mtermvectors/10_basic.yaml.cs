using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Mtermvectors1
{
	public partial class Mtermvectors1YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class Setup1Tests : YamlTestsBase
		{
			[Test]
			public void Setup1Test()
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
				this.Do(()=> this._client.IndicesCreatePost("testidx", _body));

				//do index 
				_body = new {
					text= "The quick brown fox is brown."
				};
				this.Do(()=> this._client.IndexPost("testidx", "testtype", "testing_document", _body));

				//do indices.refresh 
				this.Do(()=> this._client.IndicesRefreshPostForAll());

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class BasicTestsForMultiTermvectorGet2Tests : YamlTestsBase
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
				this.Do(()=> this._client.MtermvectorsPost(_body, nv=>nv
					.Add("term_statistics", @"true")
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
				this.Do(()=> this._client.MtermvectorsPost(_body, nv=>nv
					.Add("term_statistics", @"true")
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
				this.Do(()=> this._client.MtermvectorsPost("testidx", _body, nv=>nv
					.Add("term_statistics", @"true")
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
				this.Do(()=> this._client.MtermvectorsPost("testidx", "testtype", _body, nv=>nv
					.Add("term_statistics", @"true")
				));

				//match _response.docs[0].term_vectors.text.terms.brown.term_freq: 
				this.IsMatch(_response.docs[0].term_vectors.text.terms.brown.term_freq, 2);

				//match _response.docs[0].term_vectors.text.terms.brown.ttf: 
				this.IsMatch(_response.docs[0].term_vectors.text.terms.brown.ttf, 2);

				//do mtermvectors 
				this.Do(()=> this._client.MtermvectorsGet("testidx", "testtype", nv=>nv
					.Add("term_statistics", @"true")
					.Add("ids", new [] {
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

