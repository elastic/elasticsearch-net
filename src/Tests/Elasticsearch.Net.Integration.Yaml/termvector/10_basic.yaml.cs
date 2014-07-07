using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.Termvector1
{
	public partial class Termvector1YamlTests
	{	
	
		public class Termvector110BasicYamlBase : YamlTestsBase
		{
			public Termvector110BasicYamlBase() : base()
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
		public class BasicTestsForTermvectorGet2Tests : Termvector110BasicYamlBase
		{
			[Test]
			public void BasicTestsForTermvectorGet2Test()
			{	

				//do termvector 
				this.Do(()=> _client.TermvectorGet("testidx", "testtype", "testing_document", nv=>nv
					.AddQueryString("term_statistics", @"true")
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

