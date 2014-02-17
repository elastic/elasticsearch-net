using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Termvector1
{
	public partial class Termvector1YamlTests
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
				this.Do(()=> this._client.IndicesCreatePut("testidx", _body));

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
		public class BasicTestsForTermvectorGet2Tests : YamlTestsBase
		{
			[Test]
			public void BasicTestsForTermvectorGet2Test()
			{	

				//do termvector 
				this.Do(()=> this._client.TermvectorGet("testidx", "testtype", "testing_document", nv=>nv
					.Add("term_statistics", @"true")
				));

				//match _response.term_vectors.text.field_statistics.sum_doc_freq: 
				this.IsMatch(_response.term_vectors.text.field_statistics.sum_doc_freq, 5);

				//match _response.term_vectors.text.terms.brown.doc_freq: 
				this.IsMatch(_response.term_vectors.text.terms.brown.doc_freq, 1);

				//match _response.term_vectors.text.terms.brown.tokens[0].start_offset: 
				this.IsMatch(_response.term_vectors.text.terms.brown.tokens[0].start_offset, 10);

			}
		}
	}
}

