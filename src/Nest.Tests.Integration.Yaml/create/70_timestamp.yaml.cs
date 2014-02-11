using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Create
{
	public partial class CreateTests
	{	


		public class TimestampTests : YamlTestsBase
		{
			[Test]
			public void TimestampTest()
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
				this.Do(()=> this._client.IndicesCreatePost("test_1", _body));

				//do cluster.health 
				this.Do(()=> this._client.ClusterHealthGet(nv=>nv
					.Add("wait_for_status","yellow")
				));

				//do create 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> this._client.IndexPost("test_1", "test", "1", _body, nv=>nv
					.Add("op_type","create")
				));

				//do get 
				this.Do(()=> this._client.Get("test_1", "test", "1", nv=>nv
					.Add("fields","_timestamp")
				));

				//is_true _response.fields._timestamp; 
				this.IsTrue(_response.fields._timestamp);

				//do delete 
				this.Do(()=> this._client.Delete("test_1", "test", "1"));

				//do create 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> this._client.IndexPost("test_1", "test", "1", _body, nv=>nv
					.Add("timestamp","1372011280000")
					.Add("op_type","create")
				));

				//do get 
				this.Do(()=> this._client.Get("test_1", "test", "1", nv=>nv
					.Add("fields","_timestamp")
				));

				//match _response.fields._timestamp: 
				this.IsMatch(_response.fields._timestamp, @"1372011280000");

				//do delete 
				this.Do(()=> this._client.Delete("test_1", "test", "1"));

				//do create 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> this._client.IndexPost("test_1", "test", "1", _body, nv=>nv
					.Add("timestamp","2013-06-23T18:14:40")
					.Add("op_type","create")
				));

				//do get 
				this.Do(()=> this._client.Get("test_1", "test", "1", nv=>nv
					.Add("fields","_timestamp")
				));

				//match _response.fields._timestamp: 
				this.IsMatch(_response.fields._timestamp, @"1372011280000");

			}
		}
	}
}

