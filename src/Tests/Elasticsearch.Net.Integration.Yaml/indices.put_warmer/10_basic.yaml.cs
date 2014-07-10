using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.IndicesPutWarmer1
{
	public partial class IndicesPutWarmer1YamlTests
	{	
	
		public class IndicesPutWarmer110BasicYamlBase : YamlTestsBase
		{
			public IndicesPutWarmer110BasicYamlBase() : base()
			{	

				//do indices.create 
				this.Do(()=> _client.IndicesCreate("test_index", null));

				//do indices.create 
				this.Do(()=> _client.IndicesCreate("test_idx", null));

				//do cluster.health 
				this.Do(()=> _client.ClusterHealth(nv=>nv
					.AddQueryString("wait_for_status", @"yellow")
				));

				//do indices.put_warmer 
				_body = new {
					query= new {
						match_all= new {}
					}
				};
				this.Do(()=> _client.IndicesPutWarmer("test_idx", "test_warmer2", _body));

				//do indices.put_warmer 
				_body = new {
					query= new {
						match_all= new {}
					}
				};
				this.Do(()=> _client.IndicesPutWarmer("test_index", "test_warmer", _body));

			}
		}


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class BasicTestForWarmers2Tests : IndicesPutWarmer110BasicYamlBase
		{
			[Test]
			public void BasicTestForWarmers2Test()
			{	

				//do indices.get_warmer 
				this.Do(()=> _client.IndicesGetWarmer("test_index", "test_warmer"));

				//match _response.test_index.warmers.test_warmer.source.query.match_all: 
				this.IsMatch(_response.test_index.warmers.test_warmer.source.query.match_all, new {});

				//do indices.delete_warmer 
				this.Do(()=> _client.IndicesDeleteWarmer("test_index", "test_warmer"));

				//do indices.get_warmer 
				this.Do(()=> _client.IndicesGetWarmer("test_index", "test_warmer"));

				//match this._status: 
				this.IsMatch(this._status, new {});

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GettingAllWarmersViaWarmerShouldWork3Tests : IndicesPutWarmer110BasicYamlBase
		{
			[Test]
			public void GettingAllWarmersViaWarmerShouldWork3Test()
			{	

				//do indices.get_warmer 
				this.Do(()=> _client.IndicesGetWarmerForAll());

				//match _response.test_index.warmers.test_warmer.source.query.match_all: 
				this.IsMatch(_response.test_index.warmers.test_warmer.source.query.match_all, new {});

				//match _response.test_idx.warmers.test_warmer2.source.query.match_all: 
				this.IsMatch(_response.test_idx.warmers.test_warmer2.source.query.match_all, new {});

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GettingWarmersForSeveralIndicesShouldWorkUsing4Tests : IndicesPutWarmer110BasicYamlBase
		{
			[Test]
			public void GettingWarmersForSeveralIndicesShouldWorkUsing4Test()
			{	

				//do indices.get_warmer 
				this.Do(()=> _client.IndicesGetWarmer("*", "*"));

				//match _response.test_index.warmers.test_warmer.source.query.match_all: 
				this.IsMatch(_response.test_index.warmers.test_warmer.source.query.match_all, new {});

				//match _response.test_idx.warmers.test_warmer2.source.query.match_all: 
				this.IsMatch(_response.test_idx.warmers.test_warmer2.source.query.match_all, new {});

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GettingWarmersForSeveralIndicesShouldWorkUsingAll5Tests : IndicesPutWarmer110BasicYamlBase
		{
			[Test]
			public void GettingWarmersForSeveralIndicesShouldWorkUsingAll5Test()
			{	

				//do indices.get_warmer 
				this.Do(()=> _client.IndicesGetWarmer("_all", "_all"));

				//match _response.test_index.warmers.test_warmer.source.query.match_all: 
				this.IsMatch(_response.test_index.warmers.test_warmer.source.query.match_all, new {});

				//match _response.test_idx.warmers.test_warmer2.source.query.match_all: 
				this.IsMatch(_response.test_idx.warmers.test_warmer2.source.query.match_all, new {});

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GettingAllWarmersWithoutSpecifyingIndexShouldWork6Tests : IndicesPutWarmer110BasicYamlBase
		{
			[Test]
			public void GettingAllWarmersWithoutSpecifyingIndexShouldWork6Test()
			{	

				//do indices.get_warmer 
				this.Do(()=> _client.IndicesGetWarmerForAll("_all"));

				//match _response.test_index.warmers.test_warmer.source.query.match_all: 
				this.IsMatch(_response.test_index.warmers.test_warmer.source.query.match_all, new {});

				//match _response.test_idx.warmers.test_warmer2.source.query.match_all: 
				this.IsMatch(_response.test_idx.warmers.test_warmer2.source.query.match_all, new {});

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GettingWarmersForSeveralIndicesShouldWorkUsingPrefix7Tests : IndicesPutWarmer110BasicYamlBase
		{
			[Test]
			public void GettingWarmersForSeveralIndicesShouldWorkUsingPrefix7Test()
			{	

				//do indices.get_warmer 
				this.Do(()=> _client.IndicesGetWarmer("test_i*", "test_w*"));

				//match _response.test_index.warmers.test_warmer.source.query.match_all: 
				this.IsMatch(_response.test_index.warmers.test_warmer.source.query.match_all, new {});

				//match _response.test_idx.warmers.test_warmer2.source.query.match_all: 
				this.IsMatch(_response.test_idx.warmers.test_warmer2.source.query.match_all, new {});

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GettingWarmersForSeveralIndicesShouldWorkUsingCommaSeparatedLists8Tests : IndicesPutWarmer110BasicYamlBase
		{
			[Test]
			public void GettingWarmersForSeveralIndicesShouldWorkUsingCommaSeparatedLists8Test()
			{	

				//do indices.get_warmer 
				this.Do(()=> _client.IndicesGetWarmer("test_index,test_idx", "test_warmer,test_warmer2"));

				//match _response.test_index.warmers.test_warmer.source.query.match_all: 
				this.IsMatch(_response.test_index.warmers.test_warmer.source.query.match_all, new {});

				//match _response.test_idx.warmers.test_warmer2.source.query.match_all: 
				this.IsMatch(_response.test_idx.warmers.test_warmer2.source.query.match_all, new {});

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GettingANonExistentWarmerOnAnExistingIndexShouldReturnAnEmptyBody9Tests : IndicesPutWarmer110BasicYamlBase
		{
			[Test]
			public void GettingANonExistentWarmerOnAnExistingIndexShouldReturnAnEmptyBody9Test()
			{	

				//do indices.get_warmer 
				this.Do(()=> _client.IndicesGetWarmer("test_index", "non-existent"));

				//match this._status: 
				this.IsMatch(this._status, new {});

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GettingAnExistentAndNonExistentWarmerShouldReturnTheExistentAndNoDataAboutTheNonExistentWarmer10Tests : IndicesPutWarmer110BasicYamlBase
		{
			[Test]
			public void GettingAnExistentAndNonExistentWarmerShouldReturnTheExistentAndNoDataAboutTheNonExistentWarmer10Test()
			{	

				//do indices.get_warmer 
				this.Do(()=> _client.IndicesGetWarmer("test_index", "test_warmer,non-existent"));

				//match _response.test_index.warmers.test_warmer.source.query.match_all: 
				this.IsMatch(_response.test_index.warmers.test_warmer.source.query.match_all, new {});

				//is_false _response[@"test_index"][@"warmers"][@"non-existent"]; 
				this.IsFalse(_response[@"test_index"][@"warmers"][@"non-existent"]);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GettingWarmerOnAnNonExistentIndexShouldReturn40411Tests : IndicesPutWarmer110BasicYamlBase
		{
			[Test]
			public void GettingWarmerOnAnNonExistentIndexShouldReturn40411Test()
			{	

				//do indices.get_warmer 
				this.Do(()=> _client.IndicesGetWarmer("non-existent", "foo"), shouldCatch: @"missing");

			}
		}
	}
}

