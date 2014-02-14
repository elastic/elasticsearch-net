using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesPutWarmer3
{
	public partial class IndicesPutWarmer3YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class Setup1Tests : YamlTestsBase
		{
			[Test]
			public void Setup1Test()
			{	

				//do indices.create 
				this.Do(()=> this._client.IndicesCreatePost("test_index1", null));

				//do indices.create 
				this.Do(()=> this._client.IndicesCreatePost("test_index2", null));

				//do indices.create 
				this.Do(()=> this._client.IndicesCreatePost("foo", null));

				//do cluster.health 
				this.Do(()=> this._client.ClusterHealthGet(nv=>nv
					.Add("wait_for_status", @"yellow")
				));

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class PutWarmerPerIndex2Tests : YamlTestsBase
		{
			[Test]
			public void PutWarmerPerIndex2Test()
			{	

				//do indices.put_warmer 
				_body = new {
					query= new {
						match_all= new {}
					}
				};
				this.Do(()=> this._client.IndicesPutWarmerPost("test_index1", "warmer", _body));

				//do indices.put_warmer 
				_body = new {
					query= new {
						match_all= new {}
					}
				};
				this.Do(()=> this._client.IndicesPutWarmerPost("test_index2", "warmer", _body));

				//do indices.get_warmer 
				this.Do(()=> this._client.IndicesGetWarmer("_all", "*"));

				//match _response.test_index1.warmers.warmer.source.query.match_all: 
				this.IsMatch(_response.test_index1.warmers.warmer.source.query.match_all, new {});

				//match _response.test_index2.warmers.warmer.source.query.match_all: 
				this.IsMatch(_response.test_index2.warmers.warmer.source.query.match_all, new {});

				//is_false _response.foo; 
				this.IsFalse(_response.foo);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class PutWarmerInAllIndex3Tests : YamlTestsBase
		{
			[Test]
			public void PutWarmerInAllIndex3Test()
			{	

				//do indices.put_warmer 
				_body = new {
					query= new {
						match_all= new {}
					}
				};
				this.Do(()=> this._client.IndicesPutWarmerPost("_all", "warmer", _body));

				//do indices.get_warmer 
				this.Do(()=> this._client.IndicesGetWarmer("_all", "*"));

				//match _response.test_index1.warmers.warmer.source.query.match_all: 
				this.IsMatch(_response.test_index1.warmers.warmer.source.query.match_all, new {});

				//match _response.test_index2.warmers.warmer.source.query.match_all: 
				this.IsMatch(_response.test_index2.warmers.warmer.source.query.match_all, new {});

				//match _response.foo.warmers.warmer.source.query.match_all: 
				this.IsMatch(_response.foo.warmers.warmer.source.query.match_all, new {});

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class PutWarmerInIndex4Tests : YamlTestsBase
		{
			[Test]
			public void PutWarmerInIndex4Test()
			{	

				//do indices.put_warmer 
				_body = new {
					query= new {
						match_all= new {}
					}
				};
				this.Do(()=> this._client.IndicesPutWarmerPost("*", "warmer", _body));

				//do indices.get_warmer 
				this.Do(()=> this._client.IndicesGetWarmer("_all", "*"));

				//match _response.test_index1.warmers.warmer.source.query.match_all: 
				this.IsMatch(_response.test_index1.warmers.warmer.source.query.match_all, new {});

				//match _response.test_index2.warmers.warmer.source.query.match_all: 
				this.IsMatch(_response.test_index2.warmers.warmer.source.query.match_all, new {});

				//match _response.foo.warmers.warmer.source.query.match_all: 
				this.IsMatch(_response.foo.warmers.warmer.source.query.match_all, new {});

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class PutWarmerPrefixIndex5Tests : YamlTestsBase
		{
			[Test]
			public void PutWarmerPrefixIndex5Test()
			{	

				//do indices.put_warmer 
				_body = new {
					query= new {
						match_all= new {}
					}
				};
				this.Do(()=> this._client.IndicesPutWarmerPost("test_index*", "warmer", _body));

				//do indices.get_warmer 
				this.Do(()=> this._client.IndicesGetWarmer("_all", "*"));

				//match _response.test_index1.warmers.warmer.source.query.match_all: 
				this.IsMatch(_response.test_index1.warmers.warmer.source.query.match_all, new {});

				//match _response.test_index2.warmers.warmer.source.query.match_all: 
				this.IsMatch(_response.test_index2.warmers.warmer.source.query.match_all, new {});

				//is_false _response.foo; 
				this.IsFalse(_response.foo);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class PutWarmerInListOfIndices6Tests : YamlTestsBase
		{
			[Test]
			public void PutWarmerInListOfIndices6Test()
			{	

				//do indices.put_warmer 
				_body = new {
					query= new {
						match_all= new {}
					}
				};
				this.Do(()=> this._client.IndicesPutWarmerPost("test_index1,test_index2", "warmer", _body));

				//do indices.get_warmer 
				this.Do(()=> this._client.IndicesGetWarmer("_all", "*"));

				//match _response.test_index1.warmers.warmer.source.query.match_all: 
				this.IsMatch(_response.test_index1.warmers.warmer.source.query.match_all, new {});

				//match _response.test_index2.warmers.warmer.source.query.match_all: 
				this.IsMatch(_response.test_index2.warmers.warmer.source.query.match_all, new {});

				//is_false _response.foo; 
				this.IsFalse(_response.foo);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class PutWarmerWithBlankIndex7Tests : YamlTestsBase
		{
			[Test]
			public void PutWarmerWithBlankIndex7Test()
			{	

				//do indices.put_warmer 
				_body = new {
					query= new {
						match_all= new {}
					}
				};
				this.Do(()=> this._client.IndicesPutWarmerPost("warmer", _body));

				//do indices.get_warmer 
				this.Do(()=> this._client.IndicesGetWarmer("_all", "*"));

				//match _response.test_index1.warmers.warmer.source.query.match_all: 
				this.IsMatch(_response.test_index1.warmers.warmer.source.query.match_all, new {});

				//match _response.test_index2.warmers.warmer.source.query.match_all: 
				this.IsMatch(_response.test_index2.warmers.warmer.source.query.match_all, new {});

				//match _response.foo.warmers.warmer.source.query.match_all: 
				this.IsMatch(_response.foo.warmers.warmer.source.query.match_all, new {});

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class PutWarmerWithMissingName8Tests : YamlTestsBase
		{
			[Test]
			public void PutWarmerWithMissingName8Test()
			{	

				//do indices.put_warmer 
				_body = new {
					query= new {
						match_all= new {}
					}
				};
				this.Do(()=> this._client.IndicesPutWarmerPost(_body), shouldCatch: @"param");

			}
		}
	}
}

