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
	
		public class IndicesPutWarmer3AllPathOptionsYamlBase : YamlTestsBase
		{
			public IndicesPutWarmer3AllPathOptionsYamlBase() : base()
			{	

				//do indices.create 
				this.Do(()=> this._client.IndicesCreatePut("test_index1", null));

				//do indices.create 
				this.Do(()=> this._client.IndicesCreatePut("test_index2", null));

				//do indices.create 
				this.Do(()=> this._client.IndicesCreatePut("foo", null));

			}
		}


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class PutWarmerPerIndex2Tests : IndicesPutWarmer3AllPathOptionsYamlBase
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
				this.Do(()=> this._client.IndicesPutWarmer("test_index1", "warmer", _body));

				//do indices.put_warmer 
				_body = new {
					query= new {
						match_all= new {}
					}
				};
				this.Do(()=> this._client.IndicesPutWarmer("test_index2", "warmer", _body));

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
		public class PutWarmerInAllIndex3Tests : IndicesPutWarmer3AllPathOptionsYamlBase
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
				this.Do(()=> this._client.IndicesPutWarmer("_all", "warmer", _body));

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
		public class PutWarmerInIndex4Tests : IndicesPutWarmer3AllPathOptionsYamlBase
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
				this.Do(()=> this._client.IndicesPutWarmer("*", "warmer", _body));

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
		public class PutWarmerPrefixIndex5Tests : IndicesPutWarmer3AllPathOptionsYamlBase
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
				this.Do(()=> this._client.IndicesPutWarmer("test_index*", "warmer", _body));

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
		public class PutWarmerInListOfIndices6Tests : IndicesPutWarmer3AllPathOptionsYamlBase
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
				this.Do(()=> this._client.IndicesPutWarmer("test_index1,test_index2", "warmer", _body));

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
		public class PutWarmerWithBlankIndex7Tests : IndicesPutWarmer3AllPathOptionsYamlBase
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
				this.Do(()=> this._client.IndicesPutWarmerForAll("warmer", _body));

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
		public class PutWarmerWithMissingName8Tests : IndicesPutWarmer3AllPathOptionsYamlBase
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
				this.Do(()=> this._client.IndicesPutWarmerForAll("", _body), shouldCatch: @"param");

			}
		}
	}
}

