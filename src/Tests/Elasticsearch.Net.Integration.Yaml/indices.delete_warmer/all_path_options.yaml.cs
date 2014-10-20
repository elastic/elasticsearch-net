using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.IndicesDeleteWarmer1
{
	public partial class IndicesDeleteWarmer1YamlTests
	{	
	
		public class IndicesDeleteWarmer1AllPathOptionsYamlBase : YamlTestsBase
		{
			public IndicesDeleteWarmer1AllPathOptionsYamlBase() : base()
			{	

				//do indices.create 
				_body = new {
					warmers= new {
						test_warmer1= new {
							source= new {
								query= new {
									match_all= new {}
								}
							}
						},
						test_warmer2= new {
							source= new {
								query= new {
									match_all= new {}
								}
							}
						}
					}
				};
				this.Do(()=> _client.IndicesCreate("test_index1", _body));

				//do indices.create 
				_body = new {
					warmers= new {
						test_warmer1= new {
							source= new {
								query= new {
									match_all= new {}
								}
							}
						},
						test_warmer2= new {
							source= new {
								query= new {
									match_all= new {}
								}
							}
						}
					}
				};
				this.Do(()=> _client.IndicesCreate("test_index2", _body));

				//do indices.create 
				_body = new {
					warmers= new {
						test_warmer1= new {
							source= new {
								query= new {
									match_all= new {}
								}
							}
						},
						test_warmer2= new {
							source= new {
								query= new {
									match_all= new {}
								}
							}
						}
					}
				};
				this.Do(()=> _client.IndicesCreate("foo", _body));

			}
		}


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class CheckDeleteWithAllIndex3Tests : IndicesDeleteWarmer1AllPathOptionsYamlBase
		{
			[Test]
			public void CheckDeleteWithAllIndex3Test()
			{	

				//do indices.delete_warmer 
				this.Do(()=> _client.IndicesDeleteWarmer("_all", "test_warmer1"));

				//do indices.get_warmer 
				this.Do(()=> _client.IndicesGetWarmerForAll());

				//match _response.test_index1.warmers.test_warmer2.source.query.match_all: 
				this.IsMatch(_response.test_index1.warmers.test_warmer2.source.query.match_all, new {});

				//match _response.test_index2.warmers.test_warmer2.source.query.match_all: 
				this.IsMatch(_response.test_index2.warmers.test_warmer2.source.query.match_all, new {});

				//match _response.foo.warmers.test_warmer2.source.query.match_all: 
				this.IsMatch(_response.foo.warmers.test_warmer2.source.query.match_all, new {});

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class CheckDeleteWithIndex4Tests : IndicesDeleteWarmer1AllPathOptionsYamlBase
		{
			[Test]
			public void CheckDeleteWithIndex4Test()
			{	

				//do indices.delete_warmer 
				this.Do(()=> _client.IndicesDeleteWarmer("*", "test_warmer1"));

				//do indices.get_warmer 
				this.Do(()=> _client.IndicesGetWarmerForAll());

				//match _response.test_index1.warmers.test_warmer2.source.query.match_all: 
				this.IsMatch(_response.test_index1.warmers.test_warmer2.source.query.match_all, new {});

				//match _response.test_index2.warmers.test_warmer2.source.query.match_all: 
				this.IsMatch(_response.test_index2.warmers.test_warmer2.source.query.match_all, new {});

				//match _response.foo.warmers.test_warmer2.source.query.match_all: 
				this.IsMatch(_response.foo.warmers.test_warmer2.source.query.match_all, new {});

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class CheckDeleteWithIndexList5Tests : IndicesDeleteWarmer1AllPathOptionsYamlBase
		{
			[Test]
			public void CheckDeleteWithIndexList5Test()
			{	

				//do indices.delete_warmer 
				this.Do(()=> _client.IndicesDeleteWarmer("test_index1,test_index2", "test_warmer1"));

				//do indices.get_warmer 
				this.Do(()=> _client.IndicesGetWarmer("_all", "test_warmer1"));

				//match _response.foo.warmers.test_warmer1.source.query.match_all: 
				this.IsMatch(_response.foo.warmers.test_warmer1.source.query.match_all, new {});

				//is_false _response.test_index1; 
				this.IsFalse(_response.test_index1);

				//is_false _response.test_index2; 
				this.IsFalse(_response.test_index2);

				//do indices.get_warmer 
				this.Do(()=> _client.IndicesGetWarmer("_all", "test_warmer2"));

				//match _response.test_index1.warmers.test_warmer2.source.query.match_all: 
				this.IsMatch(_response.test_index1.warmers.test_warmer2.source.query.match_all, new {});

				//match _response.test_index2.warmers.test_warmer2.source.query.match_all: 
				this.IsMatch(_response.test_index2.warmers.test_warmer2.source.query.match_all, new {});

				//match _response.foo.warmers.test_warmer2.source.query.match_all: 
				this.IsMatch(_response.foo.warmers.test_warmer2.source.query.match_all, new {});

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class CheckDeleteWithPrefixIndex6Tests : IndicesDeleteWarmer1AllPathOptionsYamlBase
		{
			[Test]
			public void CheckDeleteWithPrefixIndex6Test()
			{	

				//do indices.delete_warmer 
				this.Do(()=> _client.IndicesDeleteWarmer("test_*", "test_warmer1"));

				//do indices.get_warmer 
				this.Do(()=> _client.IndicesGetWarmer("_all", "test_warmer1"));

				//match _response.foo.warmers.test_warmer1.source.query.match_all: 
				this.IsMatch(_response.foo.warmers.test_warmer1.source.query.match_all, new {});

				//is_false _response.test_index1; 
				this.IsFalse(_response.test_index1);

				//is_false _response.test_index2; 
				this.IsFalse(_response.test_index2);

				//do indices.get_warmer 
				this.Do(()=> _client.IndicesGetWarmer("_all", "test_warmer2"));

				//match _response.test_index1.warmers.test_warmer2.source.query.match_all: 
				this.IsMatch(_response.test_index1.warmers.test_warmer2.source.query.match_all, new {});

				//match _response.test_index2.warmers.test_warmer2.source.query.match_all: 
				this.IsMatch(_response.test_index2.warmers.test_warmer2.source.query.match_all, new {});

				//match _response.foo.warmers.test_warmer2.source.query.match_all: 
				this.IsMatch(_response.foo.warmers.test_warmer2.source.query.match_all, new {});

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class CheckDeleteWithIndexListAndWarmers7Tests : IndicesDeleteWarmer1AllPathOptionsYamlBase
		{
			[Test]
			public void CheckDeleteWithIndexListAndWarmers7Test()
			{	

				//do indices.delete_warmer 
				this.Do(()=> _client.IndicesDeleteWarmer("test_index1,test_index2", "*"));

				//do indices.get_warmer 
				this.Do(()=> _client.IndicesGetWarmer("_all", "test_warmer1"));

				//match _response.foo.warmers.test_warmer1.source.query.match_all: 
				this.IsMatch(_response.foo.warmers.test_warmer1.source.query.match_all, new {});

				//is_false _response.test_index1; 
				this.IsFalse(_response.test_index1);

				//is_false _response.test_index2; 
				this.IsFalse(_response.test_index2);

				//do indices.get_warmer 
				this.Do(()=> _client.IndicesGetWarmer("_all", "test_warmer2"));

				//match _response.foo.warmers.test_warmer2.source.query.match_all: 
				this.IsMatch(_response.foo.warmers.test_warmer2.source.query.match_all, new {});

				//is_false _response.test_index1; 
				this.IsFalse(_response.test_index1);

				//is_false _response.test_index2; 
				this.IsFalse(_response.test_index2);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class CheckDeleteWithIndexListAndAllWarmers8Tests : IndicesDeleteWarmer1AllPathOptionsYamlBase
		{
			[Test]
			public void CheckDeleteWithIndexListAndAllWarmers8Test()
			{	

				//do indices.delete_warmer 
				this.Do(()=> _client.IndicesDeleteWarmer("test_index1,test_index2", "_all"));

				//do indices.get_warmer 
				this.Do(()=> _client.IndicesGetWarmer("_all", "test_warmer1"));

				//match _response.foo.warmers.test_warmer1.source.query.match_all: 
				this.IsMatch(_response.foo.warmers.test_warmer1.source.query.match_all, new {});

				//is_false _response.test_index1; 
				this.IsFalse(_response.test_index1);

				//is_false _response.test_index2; 
				this.IsFalse(_response.test_index2);

				//do indices.get_warmer 
				this.Do(()=> _client.IndicesGetWarmer("_all", "test_warmer2"));

				//match _response.foo.warmers.test_warmer2.source.query.match_all: 
				this.IsMatch(_response.foo.warmers.test_warmer2.source.query.match_all, new {});

				//is_false _response.test_index1; 
				this.IsFalse(_response.test_index1);

				//is_false _response.test_index2; 
				this.IsFalse(_response.test_index2);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class CheckDeleteWithIndexListAndWildcardWarmers9Tests : IndicesDeleteWarmer1AllPathOptionsYamlBase
		{
			[Test]
			public void CheckDeleteWithIndexListAndWildcardWarmers9Test()
			{	

				//do indices.delete_warmer 
				this.Do(()=> _client.IndicesDeleteWarmer("test_index1,test_index2", "*1"));

				//do indices.get_warmer 
				this.Do(()=> _client.IndicesGetWarmer("_all", "test_warmer1"));

				//match _response.foo.warmers.test_warmer1.source.query.match_all: 
				this.IsMatch(_response.foo.warmers.test_warmer1.source.query.match_all, new {});

				//is_false _response.test_index1; 
				this.IsFalse(_response.test_index1);

				//is_false _response.test_index2; 
				this.IsFalse(_response.test_index2);

				//do indices.get_warmer 
				this.Do(()=> _client.IndicesGetWarmer("_all", "test_warmer2"));

				//match _response.test_index1.warmers.test_warmer2.source.query.match_all: 
				this.IsMatch(_response.test_index1.warmers.test_warmer2.source.query.match_all, new {});

				//match _response.test_index2.warmers.test_warmer2.source.query.match_all: 
				this.IsMatch(_response.test_index2.warmers.test_warmer2.source.query.match_all, new {});

				//match _response.foo.warmers.test_warmer2.source.query.match_all: 
				this.IsMatch(_response.foo.warmers.test_warmer2.source.query.match_all, new {});

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class Check404OnNoMatchingTestWarmer10Tests : IndicesDeleteWarmer1AllPathOptionsYamlBase
		{
			[Test]
			public void Check404OnNoMatchingTestWarmer10Test()
			{	

				//do indices.delete_warmer 
				this.Do(()=> _client.IndicesDeleteWarmer("*", "non_existent"), shouldCatch: @"missing");

				//do indices.delete_warmer 
				this.Do(()=> _client.IndicesDeleteWarmer("non_existent", "test_warmer1"), shouldCatch: @"missing");

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class CheckDeleteWithBlankIndexAndBlankTestWarmer11Tests : IndicesDeleteWarmer1AllPathOptionsYamlBase
		{
			[Test]
			public void CheckDeleteWithBlankIndexAndBlankTestWarmer11Test()
			{	

				//do indices.delete_warmer 
				this.Do(()=> _client.IndicesDeleteWarmer("", "test_warmer1"), shouldCatch: @"param");

				//do indices.delete_warmer 
				this.Do(()=> _client.IndicesDeleteWarmer("test_index1", ""), shouldCatch: @"param");

			}
		}
	}
}

