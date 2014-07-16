using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.CatFielddata1
{
	public partial class CatFielddata1YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class Help1Tests : YamlTestsBase
		{
			[Test]
			public void Help1Test()
			{	

				//do cat.fielddata 
				this.Do(()=> _client.CatFielddata(nv=>nv
					.AddQueryString("help", @"true")
				));

				//match this._status: 
				this.IsMatch(this._status, @"/^  id .+ \n
    host .+ \n
    ip .+ \n
    node .+ \n
    total .+ \n
$/
");

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class TestCatFielddataOutput2Tests : YamlTestsBase
		{
			[Test]
			public void TestCatFielddataOutput2Test()
			{	

				//do cat.fielddata 
				this.Do(()=> _client.CatFielddata());

				//do index 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> _client.Index("index", "type", _body, nv=>nv
					.AddQueryString("refresh", @"true")
				));

				//do search 
				_body = new {
					query= new {
						match_all= new {}
					},
					sort= "foo"
				};
				this.Do(()=> _client.Search("index", _body));

				//do cat.fielddata 
				this.Do(()=> _client.CatFielddata(nv=>nv
					.AddQueryString("h", @"total")
					.AddQueryString("v", @"true")
				));

				//match this._status: 
				this.IsMatch(this._status, @"/^   total               \s \n
     (\s*\d+(\.\d+)?[gmk]?b  \s \n)+ $/
");

				//do cat.fielddata 
				this.Do(()=> _client.CatFielddata(nv=>nv
					.AddQueryString("h", @"total,foo")
					.AddQueryString("v", @"true")
				));

				//match this._status: 
				this.IsMatch(this._status, @"/^   total \s+              foo \s+ \n
     (\s*\d+(\.\d+)?[gmk]?b \s+ \d+(\.\d+)?[gmk]?b \s \n)+ \s*$/
");

				//do cat.fielddata 
				this.Do(()=> _client.CatFielddata(nv=>nv
					.AddQueryString("h", @"total,foo")
					.AddQueryString("fields", @"notfoo,foo")
					.AddQueryString("v", @"true")
				));

				//match this._status: 
				this.IsMatch(this._status, @"/^   total \s+              foo \s+ \n
     (\s*\d+(\.\d+)?[gmk]?b \s+ \d+(\.\d+)?[gmk]?b \s \n)+ \s*$/
");

			}
		}
	}
}

