using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Mget11
{
	public partial class Mget11YamlTests
	{	
	
		public class Mget1170SourceFilteringYamlBase : YamlTestsBase
		{
			public Mget1170SourceFilteringYamlBase() : base()
			{	

				//do index 
				_body = new {
					include= new {
						field1= "v1",
						field2= "v2"
					},
					count= "1"
				};
				this.Do(()=> _client.Index("test_1", "test", "1", _body));

				//do index 
				_body = new {
					include= new {
						field1= "v1",
						field2= "v2"
					},
					count= "1"
				};
				this.Do(()=> _client.Index("test_1", "test", "2", _body));

			}
		}


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class SourceFilteringTrueFalse2Tests : Mget1170SourceFilteringYamlBase
		{
			[Test]
			public void SourceFilteringTrueFalse2Test()
			{	

				//do mget 
				_body = new {
					docs= new dynamic[] {
						new {
							_index= "test_1",
							_type= "test",
							_id= "1",
							_source= "false"
						},
						new {
							_index= "test_1",
							_type= "test",
							_id= "2",
							_source= "true"
						}
					}
				};
				this.Do(()=> _client.Mget(_body));

				//match _response.docs[0]._id: 
				this.IsMatch(_response.docs[0]._id, 1);

				//is_false _response.docs[0]._source; 
				this.IsFalse(_response.docs[0]._source);

				//match _response.docs[1]._id: 
				this.IsMatch(_response.docs[1]._id, 2);

				//is_true _response.docs[1]._source; 
				this.IsTrue(_response.docs[1]._source);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class SourceFilteringIncludeField3Tests : Mget1170SourceFilteringYamlBase
		{
			[Test]
			public void SourceFilteringIncludeField3Test()
			{	

				//do mget 
				_body = new {
					docs= new dynamic[] {
						new {
							_index= "test_1",
							_type= "test",
							_id= "1",
							_source= "include.field1"
						},
						new {
							_index= "test_1",
							_type= "test",
							_id= "2",
							_source= new [] {
								"include.field1"
							}
						}
					}
				};
				this.Do(()=> _client.Mget(_body));

				//match _response.docs[0]._source: 
				this.IsMatch(_response.docs[0]._source, new {
					include= new {
						field1= "v1"
					}
				});

				//match _response.docs[1]._source: 
				this.IsMatch(_response.docs[1]._source, new {
					include= new {
						field1= "v1"
					}
				});

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class SourceFilteringIncludeNestedField4Tests : Mget1170SourceFilteringYamlBase
		{
			[Test]
			public void SourceFilteringIncludeNestedField4Test()
			{	

				//do mget 
				_body = new {
					docs= new dynamic[] {
						new {
							_index= "test_1",
							_type= "test",
							_id= "1",
							_source= new {
								include= "include.field1"
							}
						},
						new {
							_index= "test_1",
							_type= "test",
							_id= "2",
							_source= new {
								include= new [] {
									"include.field1"
								}
							}
						}
					}
				};
				this.Do(()=> _client.Mget(_body));

				//match _response.docs[0]._source: 
				this.IsMatch(_response.docs[0]._source, new {
					include= new {
						field1= "v1"
					}
				});

				//match _response.docs[1]._source: 
				this.IsMatch(_response.docs[1]._source, new {
					include= new {
						field1= "v1"
					}
				});

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class SourceFilteringExcludeField5Tests : Mget1170SourceFilteringYamlBase
		{
			[Test]
			public void SourceFilteringExcludeField5Test()
			{	

				//do mget 
				_body = new {
					docs= new dynamic[] {
						new {
							_index= "test_1",
							_type= "test",
							_id= "1",
							_source= new {
								include= new [] {
									"include"
								},
								exclude= new [] {
									"*.field2"
								}
							}
						}
					}
				};
				this.Do(()=> _client.Mget(_body));

				//match _response.docs[0]._source: 
				this.IsMatch(_response.docs[0]._source, new {
					include= new {
						field1= "v1"
					}
				});

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class SourceFilteringIdsAndTrueFalse6Tests : Mget1170SourceFilteringYamlBase
		{
			[Test]
			public void SourceFilteringIdsAndTrueFalse6Test()
			{	

				//do mget 
				_body = new {
					ids= new [] {
						"1",
						"2"
					}
				};
				this.Do(()=> _client.Mget("test_1", _body, nv=>nv
					.Add("_source", @"false")
				));

				//is_false _response.docs[0]._source; 
				this.IsFalse(_response.docs[0]._source);

				//is_false _response.docs[1]._source; 
				this.IsFalse(_response.docs[1]._source);

				//do mget 
				_body = new {
					ids= new [] {
						"1",
						"2"
					}
				};
				this.Do(()=> _client.Mget("test_1", _body, nv=>nv
					.Add("_source", @"true")
				));

				//is_true _response.docs[0]._source; 
				this.IsTrue(_response.docs[0]._source);

				//is_true _response.docs[1]._source; 
				this.IsTrue(_response.docs[1]._source);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class SourceFilteringIdsAndIncludeField7Tests : Mget1170SourceFilteringYamlBase
		{
			[Test]
			public void SourceFilteringIdsAndIncludeField7Test()
			{	

				//do mget 
				_body = new {
					ids= new [] {
						"1",
						"2"
					}
				};
				this.Do(()=> _client.Mget("test_1", _body, nv=>nv
					.Add("_source", @"include.field1")
				));

				//match _response.docs[0]._source: 
				this.IsMatch(_response.docs[0]._source, new {
					include= new {
						field1= "v1"
					}
				});

				//match _response.docs[1]._source: 
				this.IsMatch(_response.docs[1]._source, new {
					include= new {
						field1= "v1"
					}
				});

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class SourceFilteringIdsAndIncludeNestedField8Tests : Mget1170SourceFilteringYamlBase
		{
			[Test]
			public void SourceFilteringIdsAndIncludeNestedField8Test()
			{	

				//do mget 
				_body = new {
					ids= new [] {
						"1",
						"2"
					}
				};
				this.Do(()=> _client.Mget("test_1", _body, nv=>nv
					.Add("_source_include", @"include.field1,count")
				));

				//match _response.docs[0]._source: 
				this.IsMatch(_response.docs[0]._source, new {
					include= new {
						field1= "v1"
					},
					count= "1"
				});

				//match _response.docs[1]._source: 
				this.IsMatch(_response.docs[1]._source, new {
					include= new {
						field1= "v1"
					},
					count= "1"
				});

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class SourceFilteringIdsAndExcludeField9Tests : Mget1170SourceFilteringYamlBase
		{
			[Test]
			public void SourceFilteringIdsAndExcludeField9Test()
			{	

				//do mget 
				_body = new {
					ids= new [] {
						"1",
						"2"
					}
				};
				this.Do(()=> _client.Mget("test_1", _body, nv=>nv
					.Add("_source_include", @"include")
					.Add("_source_exclude", @"*.field2")
				));

				//match _response.docs[0]._source: 
				this.IsMatch(_response.docs[0]._source, new {
					include= new {
						field1= "v1"
					}
				});

				//match _response.docs[1]._source: 
				this.IsMatch(_response.docs[1]._source, new {
					include= new {
						field1= "v1"
					}
				});

			}
		}
	}
}

