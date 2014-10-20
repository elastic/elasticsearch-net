using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.Suggest2
{
	public partial class Suggest2YamlTests
	{	
	
		public class Suggest220ContextYamlBase : YamlTestsBase
		{
			public Suggest220ContextYamlBase() : base()
			{	

				//do indices.create 
				_body = new {
					mappings= new {
						test= new {
							properties= new {
								suggest_context= new {
									type= "completion",
									context= new {
										color= new {
											type= "category"
										}
									}
								},
								suggest_context_default_hardcoded= new {
									type= "completion",
									context= new {
										color= new {
											type= "category",
											@default= "red"
										}
									}
								},
								suggest_context_default_path= new {
									type= "completion",
									context= new {
										color= new {
											type= "category",
											@default= "red",
											path= "color"
										}
									}
								},
								suggest_geo= new {
									type= "completion",
									context= new {
										location= new {
											type= "geo",
											precision= "5km"
										}
									}
								}
							}
						}
					}
				};
				this.Do(()=> _client.IndicesCreate("test", _body));

			}
		}


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class SimpleContextSuggestionShouldWork2Tests : Suggest220ContextYamlBase
		{
			[Test]
			public void SimpleContextSuggestionShouldWork2Test()
			{	

				//do index 
				_body = new {
					suggest_context= new {
						input= "Hoodie red",
						context= new {
							color= "red"
						}
					}
				};
				this.Do(()=> _client.Index("test", "test", "1", _body));

				//do index 
				_body = new {
					suggest_context= new {
						input= "Hoodie blue",
						context= new {
							color= "blue"
						}
					}
				};
				this.Do(()=> _client.Index("test", "test", "2", _body));

				//do indices.refresh 
				this.Do(()=> _client.IndicesRefreshForAll());

				//do suggest 
				_body = new {
					result= new {
						text= "hoo",
						completion= new {
							field= "suggest_context",
							context= new {
								color= "red"
							}
						}
					}
				};
				this.Do(()=> _client.Suggest(_body));

				//match _response.result[0].options[0].text: 
				this.IsMatch(_response.result[0].options[0].text, @"Hoodie red");

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class HardcodedCategoryValueShouldWork3Tests : Suggest220ContextYamlBase
		{
			[Test]
			public void HardcodedCategoryValueShouldWork3Test()
			{	

				//do index 
				_body = new {
					suggest_context_default_hardcoded= new {
						input= "Hoodie red"
					}
				};
				this.Do(()=> _client.Index("test", "test", "1", _body));

				//do index 
				_body = new {
					suggest_context_default_hardcoded= new {
						input= "Hoodie blue",
						context= new {
							color= "blue"
						}
					}
				};
				this.Do(()=> _client.Index("test", "test", "2", _body));

				//do indices.refresh 
				this.Do(()=> _client.IndicesRefreshForAll());

				//do suggest 
				_body = new {
					result= new {
						text= "hoo",
						completion= new {
							field= "suggest_context_default_hardcoded",
							context= new {
								color= "red"
							}
						}
					}
				};
				this.Do(()=> _client.Suggest(_body));

				//length _response.result: 1; 
				this.IsLength(_response.result, 1);

				//length _response.result[0].options: 1; 
				this.IsLength(_response.result[0].options, 1);

				//match _response.result[0].options[0].text: 
				this.IsMatch(_response.result[0].options[0].text, @"Hoodie red");

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class CategorySuggestContextDefaultPathShouldWork4Tests : Suggest220ContextYamlBase
		{
			[Test]
			public void CategorySuggestContextDefaultPathShouldWork4Test()
			{	

				//do index 
				_body = new {
					suggest_context_default_path= new {
						input= "Hoodie red"
					}
				};
				this.Do(()=> _client.Index("test", "test", "1", _body));

				//do index 
				_body = new {
					suggest_context_default_path= new {
						input= "Hoodie blue"
					},
					color= "blue"
				};
				this.Do(()=> _client.Index("test", "test", "2", _body));

				//do indices.refresh 
				this.Do(()=> _client.IndicesRefreshForAll());

				//do suggest 
				_body = new {
					result= new {
						text= "hoo",
						completion= new {
							field= "suggest_context_default_path",
							context= new {
								color= "red"
							}
						}
					}
				};
				this.Do(()=> _client.Suggest(_body));

				//length _response.result: 1; 
				this.IsLength(_response.result, 1);

				//length _response.result[0].options: 1; 
				this.IsLength(_response.result[0].options, 1);

				//match _response.result[0].options[0].text: 
				this.IsMatch(_response.result[0].options[0].text, @"Hoodie red");

				//do suggest 
				_body = new {
					result= new {
						text= "hoo",
						completion= new {
							field= "suggest_context_default_path",
							context= new {
								color= "blue"
							}
						}
					}
				};
				this.Do(()=> _client.Suggest(_body));

				//length _response.result: 1; 
				this.IsLength(_response.result, 1);

				//length _response.result[0].options: 1; 
				this.IsLength(_response.result[0].options, 1);

				//match _response.result[0].options[0].text: 
				this.IsMatch(_response.result[0].options[0].text, @"Hoodie blue");

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GeoSuggestShouldWork5Tests : Suggest220ContextYamlBase
		{
			[Test]
			public void GeoSuggestShouldWork5Test()
			{	

				//do index 
				_body = new {
					suggest_geo= new {
						input= "Hotel Marriot in Amsterdam",
						context= new {
							location= new {
								lat= 52.22,
								lon= 4.53
							}
						}
					}
				};
				this.Do(()=> _client.Index("test", "test", "1", _body));

				//do index 
				_body = new {
					suggest_geo= new {
						input= "Hotel Marriot in Berlin",
						context= new {
							location= new {
								lat= 53.31,
								lon= 13.24
							}
						}
					}
				};
				this.Do(()=> _client.Index("test", "test", "2", _body));

				//do indices.refresh 
				this.Do(()=> _client.IndicesRefreshForAll());

				//do indices.get_mapping 
				this.Do(()=> _client.IndicesGetMappingForAll());

				//do suggest 
				_body = new {
					result= new {
						text= "hote",
						completion= new {
							field= "suggest_geo",
							context= new {
								location= new {
									lat= 52.22,
									lon= 4.53
								}
							}
						}
					}
				};
				this.Do(()=> _client.Suggest("test", _body));

				//length _response.result: 1; 
				this.IsLength(_response.result, 1);

				//length _response.result[0].options: 1; 
				this.IsLength(_response.result[0].options, 1);

				//match _response.result[0].options[0].text: 
				this.IsMatch(_response.result[0].options[0].text, @"Hotel Marriot in Amsterdam");

			}
		}
	}
}

