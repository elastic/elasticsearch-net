using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.Exists1
{
	public partial class Exists1YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class Basic1Tests : YamlTestsBase
		{
			[Test]
			public void Basic1Test()
			{	

				//do exists 
				this.Do(()=> _client.Exists("test_1", "test", "1"));

				//is_false this._status; 
				this.IsFalse(this._status);

				//do index 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> _client.Index("test_1", "test", "1", _body));

				//is_true this._status; 
				this.IsTrue(this._status);

				//do exists 
				this.Do(()=> _client.Exists("test_1", "test", "1"));

				//is_true this._status; 
				this.IsTrue(this._status);

			}
		}
	}
}

