using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesPutWarmer2
{
	public partial class IndicesPutWarmer2YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GettingWarmerForAliasesShouldReturnTheRealIndexAsKey1Tests : YamlTestsBase
		{
			[Test]
			public void GettingWarmerForAliasesShouldReturnTheRealIndexAsKey1Test()
			{	

				//do indices.create 
				this.Do(()=> this._client.IndicesCreatePost("test_index", null));

				//do cluster.health 
				this.Do(()=> this._client.ClusterHealthGet(nv=>nv
					.Add("wait_for_status", @"yellow")
				));

				//do indices.put_warmer 
				_body = new {
					query= new {
						match_all= new {}
					}
				};
				this.Do(()=> this._client.IndicesPutWarmerPost("test_index", "test_warmer", _body));

				//do indices.put_alias 
				this.Do(()=> this._client.IndicesPutAliasPost("test_index", "test_alias", null));

				//do indices.get_warmer 
				this.Do(()=> this._client.IndicesGetWarmer("test_alias"));

				//match _response.test_index.warmers.test_warmer.source.query.match_all: 
				this.IsMatch(_response.test_index.warmers.test_warmer.source.query.match_all, new {});

			}
		}
	}
}

