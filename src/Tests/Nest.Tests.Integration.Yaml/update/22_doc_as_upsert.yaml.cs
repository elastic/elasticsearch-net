using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Update4
{
	public partial class Update4YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class DocAsUpsert1Tests : YamlTestsBase
		{
			[Test]
			public void DocAsUpsert1Test()
			{	

				//do update 
				_body = new {
					doc= new {
						foo= "bar",
						count= "1"
					},
					doc_as_upsert= "1"
				};
				this.Do(()=> _client.Update("test_1", "test", "1", _body));

				//do get 
				this.Do(()=> _client.Get("test_1", "test", "1"));

				//match _response._source.foo: 
				this.IsMatch(_response._source.foo, @"bar");

				//match _response._source.count: 
				this.IsMatch(_response._source.count, 1);

				//do update 
				_body = new {
					doc= new {
						count= "2"
					},
					doc_as_upsert= "1"
				};
				this.Do(()=> _client.Update("test_1", "test", "1", _body));

				//do get 
				this.Do(()=> _client.Get("test_1", "test", "1"));

				//match _response._source.foo: 
				this.IsMatch(_response._source.foo, @"bar");

				//match _response._source.count: 
				this.IsMatch(_response._source.count, 2);

			}
		}
	}
}

