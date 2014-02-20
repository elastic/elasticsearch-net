using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Index10
{
	public partial class Index10YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class Timestamp1Tests : YamlTestsBase
		{
			[Test]
			public void Timestamp1Test()
			{	

				//do indices.create 
				_body = new {
					mappings= new {
						test= new {
							_timestamp= new {
								enabled= "1",
								store= "yes"
							}
						}
					}
				};
				this.Do(()=> _client.IndicesCreate("test_1", _body));

				//do cluster.health 
				this.Do(()=> _client.ClusterHealth(nv=>nv
					.Add("wait_for_status", @"yellow")
				));

				//do index 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> _client.Index("test_1", "test", "1", _body));

				//do get 
				this.Do(()=> _client.Get("test_1", "test", "1", nv=>nv
					.Add("fields", @"_timestamp")
				));

				//is_true _response.fields._timestamp; 
				this.IsTrue(_response.fields._timestamp);

				//do index 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> _client.Index("test_1", "test", "1", _body, nv=>nv
					.Add("timestamp", @"1372011280000")
				));

				//do get 
				this.Do(()=> _client.Get("test_1", "test", "1", nv=>nv
					.Add("fields", @"_timestamp")
				));

				//match _response.fields._timestamp: 
				this.IsMatch(_response.fields._timestamp, @"1372011280000");

				//do index 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> _client.Index("test_1", "test", "1", _body, nv=>nv
					.Add("timestamp", @"2013-06-23T18:14:40")
				));

				//do get 
				this.Do(()=> _client.Get("test_1", "test", "1", nv=>nv
					.Add("fields", @"_timestamp")
				));

				//match _response.fields._timestamp: 
				this.IsMatch(_response.fields._timestamp, @"1372011280000");

			}
		}
	}
}

