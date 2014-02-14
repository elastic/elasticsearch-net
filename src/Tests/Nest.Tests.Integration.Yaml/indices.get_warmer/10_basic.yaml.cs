using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesGetWarmer1
{
	public partial class IndicesGetWarmer1YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class Setup1Tests : YamlTestsBase
		{
			[Test]
			public void Setup1Test()
			{	

				//do indices.create 
				_body = new {
					warmers= new {
						warmer_1= new {
							source= new {
								query= new {
									match_all= new {}
								}
							}
						},
						warmer_2= new {
							source= new {
								query= new {
									match_all= new {}
								}
							}
						}
					}
				};
				this.Do(()=> this._client.IndicesCreatePost("test_1", _body));

				//do indices.create 
				_body = new {
					warmers= new {
						warmer_2= new {
							source= new {
								query= new {
									match_all= new {}
								}
							}
						},
						warmer_3= new {
							source= new {
								query= new {
									match_all= new {}
								}
							}
						}
					}
				};
				this.Do(()=> this._client.IndicesCreatePost("test_2", _body));

				//do cluster.health 
				this.Do(()=> this._client.ClusterHealthGet(nv=>nv
					.Add("wait_for_status", @"yellow")
				));

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetWarmer2Tests : YamlTestsBase
		{
			[Test]
			public void GetWarmer2Test()
			{	

				//do indices.get_warmer 
				this.Do(()=> this._client.IndicesGetWarmerForAll());

				//match _response.test_1.warmers.warmer_1.source.query.match_all: 
				this.IsMatch(_response.test_1.warmers.warmer_1.source.query.match_all, new {});

				//match _response.test_1.warmers.warmer_2.source.query.match_all: 
				this.IsMatch(_response.test_1.warmers.warmer_2.source.query.match_all, new {});

				//match _response.test_2.warmers.warmer_2.source.query.match_all: 
				this.IsMatch(_response.test_2.warmers.warmer_2.source.query.match_all, new {});

				//match _response.test_2.warmers.warmer_3.source.query.match_all: 
				this.IsMatch(_response.test_2.warmers.warmer_3.source.query.match_all, new {});

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetIndexWarmer3Tests : YamlTestsBase
		{
			[Test]
			public void GetIndexWarmer3Test()
			{	

				//do indices.get_warmer 
				this.Do(()=> this._client.IndicesGetWarmer("test_1"));

				//match _response.test_1.warmers.warmer_1.source.query.match_all: 
				this.IsMatch(_response.test_1.warmers.warmer_1.source.query.match_all, new {});

				//match _response.test_1.warmers.warmer_2.source.query.match_all: 
				this.IsMatch(_response.test_1.warmers.warmer_2.source.query.match_all, new {});

				//is_false _response.test_2; 
				this.IsFalse(_response.test_2);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetIndexWarmerAll4Tests : YamlTestsBase
		{
			[Test]
			public void GetIndexWarmerAll4Test()
			{	

				//do indices.get_warmer 
				this.Do(()=> this._client.IndicesGetWarmer("test_1", "_all"));

				//match _response.test_1.warmers.warmer_1.source.query.match_all: 
				this.IsMatch(_response.test_1.warmers.warmer_1.source.query.match_all, new {});

				//match _response.test_1.warmers.warmer_2.source.query.match_all: 
				this.IsMatch(_response.test_1.warmers.warmer_2.source.query.match_all, new {});

				//is_false _response.test_2; 
				this.IsFalse(_response.test_2);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetIndexWarmer5Tests : YamlTestsBase
		{
			[Test]
			public void GetIndexWarmer5Test()
			{	

				//do indices.get_warmer 
				this.Do(()=> this._client.IndicesGetWarmer("test_1", "*"));

				//match _response.test_1.warmers.warmer_1.source.query.match_all: 
				this.IsMatch(_response.test_1.warmers.warmer_1.source.query.match_all, new {});

				//match _response.test_1.warmers.warmer_2.source.query.match_all: 
				this.IsMatch(_response.test_1.warmers.warmer_2.source.query.match_all, new {});

				//is_false _response.test_2; 
				this.IsFalse(_response.test_2);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetIndexWarmerName6Tests : YamlTestsBase
		{
			[Test]
			public void GetIndexWarmerName6Test()
			{	

				//do indices.get_warmer 
				this.Do(()=> this._client.IndicesGetWarmer("test_1", "warmer_1"));

				//match _response.test_1.warmers.warmer_1.source.query.match_all: 
				this.IsMatch(_response.test_1.warmers.warmer_1.source.query.match_all, new {});

				//is_false _response.test_1.warmers.warmer_2; 
				this.IsFalse(_response.test_1.warmers.warmer_2);

				//is_false _response.test_2; 
				this.IsFalse(_response.test_2);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetIndexWarmerNameName7Tests : YamlTestsBase
		{
			[Test]
			public void GetIndexWarmerNameName7Test()
			{	

				//do indices.get_warmer 
				this.Do(()=> this._client.IndicesGetWarmer("test_1", "warmer_1,warmer_2"));

				//match _response.test_1.warmers.warmer_1.source.query.match_all: 
				this.IsMatch(_response.test_1.warmers.warmer_1.source.query.match_all, new {});

				//match _response.test_1.warmers.warmer_2.source.query.match_all: 
				this.IsMatch(_response.test_1.warmers.warmer_2.source.query.match_all, new {});

				//is_false _response.test_2; 
				this.IsFalse(_response.test_2);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetIndexWarmerName8Tests : YamlTestsBase
		{
			[Test]
			public void GetIndexWarmerName8Test()
			{	

				//do indices.get_warmer 
				this.Do(()=> this._client.IndicesGetWarmer("test_1", "*2"));

				//match _response.test_1.warmers.warmer_2.source.query.match_all: 
				this.IsMatch(_response.test_1.warmers.warmer_2.source.query.match_all, new {});

				//is_false _response.test_1.warmers.warmer_1; 
				this.IsFalse(_response.test_1.warmers.warmer_1);

				//is_false _response.test_2; 
				this.IsFalse(_response.test_2);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetWarmerName9Tests : YamlTestsBase
		{
			[Test]
			public void GetWarmerName9Test()
			{	

				//do indices.get_warmer 
				this.Do(()=> this._client.IndicesGetWarmerForAll("warmer_2"));

				//match _response.test_1.warmers.warmer_2.source.query.match_all: 
				this.IsMatch(_response.test_1.warmers.warmer_2.source.query.match_all, new {});

				//match _response.test_2.warmers.warmer_2.source.query.match_all: 
				this.IsMatch(_response.test_2.warmers.warmer_2.source.query.match_all, new {});

				//is_false _response.test_1.warmers.warmer_1; 
				this.IsFalse(_response.test_1.warmers.warmer_1);

				//is_false _response.test_2.warmers.warmer_3; 
				this.IsFalse(_response.test_2.warmers.warmer_3);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetAllWarmerName10Tests : YamlTestsBase
		{
			[Test]
			public void GetAllWarmerName10Test()
			{	

				//do indices.get_warmer 
				this.Do(()=> this._client.IndicesGetWarmer("_all", "warmer_2"));

				//match _response.test_1.warmers.warmer_2.source.query.match_all: 
				this.IsMatch(_response.test_1.warmers.warmer_2.source.query.match_all, new {});

				//match _response.test_2.warmers.warmer_2.source.query.match_all: 
				this.IsMatch(_response.test_2.warmers.warmer_2.source.query.match_all, new {});

				//is_false _response.test_1.warmers.warmer_1; 
				this.IsFalse(_response.test_1.warmers.warmer_1);

				//is_false _response.test_2.warmers.warmer_3; 
				this.IsFalse(_response.test_2.warmers.warmer_3);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetWarmerName11Tests : YamlTestsBase
		{
			[Test]
			public void GetWarmerName11Test()
			{	

				//do indices.get_warmer 
				this.Do(()=> this._client.IndicesGetWarmer("*", "warmer_2"));

				//match _response.test_1.warmers.warmer_2.source.query.match_all: 
				this.IsMatch(_response.test_1.warmers.warmer_2.source.query.match_all, new {});

				//match _response.test_2.warmers.warmer_2.source.query.match_all: 
				this.IsMatch(_response.test_2.warmers.warmer_2.source.query.match_all, new {});

				//is_false _response.test_1.warmers.warmer_1; 
				this.IsFalse(_response.test_1.warmers.warmer_1);

				//is_false _response.test_2.warmers.warmer_3; 
				this.IsFalse(_response.test_2.warmers.warmer_3);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetIndexIndexWarmerName12Tests : YamlTestsBase
		{
			[Test]
			public void GetIndexIndexWarmerName12Test()
			{	

				//do indices.get_warmer 
				this.Do(()=> this._client.IndicesGetWarmer("test_1,test_2", "warmer_2"));

				//match _response.test_1.warmers.warmer_2.source.query.match_all: 
				this.IsMatch(_response.test_1.warmers.warmer_2.source.query.match_all, new {});

				//match _response.test_2.warmers.warmer_2.source.query.match_all: 
				this.IsMatch(_response.test_2.warmers.warmer_2.source.query.match_all, new {});

				//is_false _response.test_2.warmers.warmer_3; 
				this.IsFalse(_response.test_2.warmers.warmer_3);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetIndexWarmerName13Tests : YamlTestsBase
		{
			[Test]
			public void GetIndexWarmerName13Test()
			{	

				//do indices.get_warmer 
				this.Do(()=> this._client.IndicesGetWarmer("*2", "warmer_2"));

				//match _response.test_2.warmers.warmer_2.source.query.match_all: 
				this.IsMatch(_response.test_2.warmers.warmer_2.source.query.match_all, new {});

				//is_false _response.test_1; 
				this.IsFalse(_response.test_1);

				//is_false _response.test_2.warmers.warmer_3; 
				this.IsFalse(_response.test_2.warmers.warmer_3);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class EmptyResponseWhenNoMatchingWarmer14Tests : YamlTestsBase
		{
			[Test]
			public void EmptyResponseWhenNoMatchingWarmer14Test()
			{	

				//do indices.get_warmer 
				this.Do(()=> this._client.IndicesGetWarmer("*", "non_existent"));

				//match this._status: 
				this.IsMatch(this._status, new {});

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class Throw404OnMissingIndex15Tests : YamlTestsBase
		{
			[Test]
			public void Throw404OnMissingIndex15Test()
			{	

				//do indices.get_warmer 
				this.Do(()=> this._client.IndicesGetWarmer("non_existent", "*"), shouldCatch: @"missing");

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetWarmerWithLocalFlag16Tests : YamlTestsBase
		{
			[Test]
			public void GetWarmerWithLocalFlag16Test()
			{	

				//do indices.get_warmer 
				this.Do(()=> this._client.IndicesGetWarmerForAll(nv=>nv
					.Add("local", @"true")
				));

				//is_true _response.test_1; 
				this.IsTrue(_response.test_1);

				//is_true _response.test_2; 
				this.IsTrue(_response.test_2);

			}
		}
	}
}

