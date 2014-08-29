using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.IndicesUpdateAliases2
{
	public partial class IndicesUpdateAliases2YamlTests
	{	
	
		public class IndicesUpdateAliases220RoutingYamlBase : YamlTestsBase
		{
			public IndicesUpdateAliases220RoutingYamlBase() : base()
			{	

				//do indices.create 
				this.Do(()=> _client.IndicesCreate("test_index", null));

			}
		}


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class Routing2Tests : IndicesUpdateAliases220RoutingYamlBase
		{
			[Test]
			public void Routing2Test()
			{	

				//do indices.update_aliases 
				_body = new {
					actions= new [] {
						new {
							add= new {
								index= "test_index",
								alias= "test_alias",
								routing= "routing"
							}
						}
					}
				};
				this.Do(()=> _client.IndicesUpdateAliasesForAll(_body));

				//do indices.get_aliases 
				this.Do(()=> _client.IndicesGetAliases("test_index"));

				//match _response.test_index.aliases.test_alias: 
				this.IsMatch(_response.test_index.aliases.test_alias, new {
					index_routing= "routing",
					search_routing= "routing"
				});

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class IndexRouting3Tests : IndicesUpdateAliases220RoutingYamlBase
		{
			[Test]
			public void IndexRouting3Test()
			{	

				//do indices.update_aliases 
				_body = new {
					actions= new [] {
						new {
							add= new {
								index= "test_index",
								alias= "test_alias",
								index_routing= "index_routing"
							}
						}
					}
				};
				this.Do(()=> _client.IndicesUpdateAliasesForAll(_body));

				//do indices.get_aliases 
				this.Do(()=> _client.IndicesGetAliases("test_index"));

				//match _response.test_index.aliases.test_alias: 
				this.IsMatch(_response.test_index.aliases.test_alias, new {
					index_routing= "index_routing"
				});

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class SearchRouting4Tests : IndicesUpdateAliases220RoutingYamlBase
		{
			[Test]
			public void SearchRouting4Test()
			{	

				//do indices.update_aliases 
				_body = new {
					actions= new [] {
						new {
							add= new {
								index= "test_index",
								alias= "test_alias",
								search_routing= "search_routing"
							}
						}
					}
				};
				this.Do(()=> _client.IndicesUpdateAliasesForAll(_body));

				//do indices.get_aliases 
				this.Do(()=> _client.IndicesGetAliases("test_index"));

				//match _response.test_index.aliases.test_alias: 
				this.IsMatch(_response.test_index.aliases.test_alias, new {
					search_routing= "search_routing"
				});

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class IndexDefaultRouting5Tests : IndicesUpdateAliases220RoutingYamlBase
		{
			[Test]
			public void IndexDefaultRouting5Test()
			{	

				//do indices.update_aliases 
				_body = new {
					actions= new [] {
						new {
							add= new {
								index= "test_index",
								alias= "test_alias",
								index_routing= "index_routing",
								routing= "routing"
							}
						}
					}
				};
				this.Do(()=> _client.IndicesUpdateAliasesForAll(_body));

				//do indices.get_aliases 
				this.Do(()=> _client.IndicesGetAliases("test_index"));

				//match _response.test_index.aliases.test_alias: 
				this.IsMatch(_response.test_index.aliases.test_alias, new {
					index_routing= "index_routing",
					search_routing= "routing"
				});

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class SearchDefaultRouting6Tests : IndicesUpdateAliases220RoutingYamlBase
		{
			[Test]
			public void SearchDefaultRouting6Test()
			{	

				//do indices.update_aliases 
				_body = new {
					actions= new [] {
						new {
							add= new {
								index= "test_index",
								alias= "test_alias",
								search_routing= "search_routing",
								routing= "routing"
							}
						}
					}
				};
				this.Do(()=> _client.IndicesUpdateAliasesForAll(_body));

				//do indices.get_aliases 
				this.Do(()=> _client.IndicesGetAliases("test_index"));

				//match _response.test_index.aliases.test_alias: 
				this.IsMatch(_response.test_index.aliases.test_alias, new {
					index_routing= "routing",
					search_routing= "search_routing"
				});

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class IndexSearchDefaultRouting7Tests : IndicesUpdateAliases220RoutingYamlBase
		{
			[Test]
			public void IndexSearchDefaultRouting7Test()
			{	

				//do indices.update_aliases 
				_body = new {
					actions= new [] {
						new {
							add= new {
								index= "test_index",
								alias= "test_alias",
								index_routing= "index_routing",
								search_routing= "search_routing",
								routing= "routing"
							}
						}
					}
				};
				this.Do(()=> _client.IndicesUpdateAliasesForAll(_body));

				//do indices.get_aliases 
				this.Do(()=> _client.IndicesGetAliases("test_index"));

				//match _response.test_index.aliases.test_alias: 
				this.IsMatch(_response.test_index.aliases.test_alias, new {
					index_routing= "index_routing",
					search_routing= "search_routing"
				});

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class NumericRouting8Tests : IndicesUpdateAliases220RoutingYamlBase
		{
			[Test]
			public void NumericRouting8Test()
			{	

				//do indices.update_aliases 
				_body = new {
					actions= new [] {
						new {
							add= new {
								index= "test_index",
								alias= "test_alias",
								routing= "5"
							}
						}
					}
				};
				this.Do(()=> _client.IndicesUpdateAliasesForAll(_body));

				//do indices.get_aliases 
				this.Do(()=> _client.IndicesGetAliases("test_index"));

				//match _response.test_index.aliases.test_alias: 
				this.IsMatch(_response.test_index.aliases.test_alias, new {
					index_routing= "5",
					search_routing= "5"
				});

			}
		}
	}
}

