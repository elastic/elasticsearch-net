using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.Script1
{
	public partial class Script1YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class IndexedScript1Tests : YamlTestsBase
		{
			[Test]
			public void IndexedScript1Test()
			{	

				//do put_script 
				_body = new {
					script= "_score * docnew [] {\"myParent.weight\"}.value"
				};
				

				//match _response._id: 
				this.IsMatch(_response._id, 1);

				//do get_script 
				

				//match _response.script: 
				this.IsMatch(_response.script, @"_score * doc[""myParent.weight""].value");

				//do delete_script 
				

				//match _response.found: 
				this.IsMatch(_response.found, @"true");

				//match _response._index: 
				this.IsMatch(_response._index, @".scripts");

				//match _response._id: 
				this.IsMatch(_response._id, 1);

				//do put_script 
				_body = new {
					script= "_score * foo bar + docnew [] {\"myParent.weight\"}.value"
				};
				

				//do put_script 
				_body = new {
					script= "_score * foo bar + docnew [] {\"myParent.weight\"}.value"
				};
				

				//do put_script 
				_body = new {
					script= "_score * docnew [] {\"myParent.weight\"}.value"
				};
				

				//do put_script 
				_body = new {
					script= "_score * docnew [] {\"myParent.weight\"}.value"
				};
				

			}
		}
	}
}

