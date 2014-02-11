using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesOpen
{
	public partial class IndicesOpenTests
	{	


		public class BasicTestForIndexOpenCloseTests : YamlTestsBase
		{
			[Test]
			public void BasicTestForIndexOpenCloseTest()
			{	

				//do indices.create 
				this.Do(()=> this._client.IndicesCreatePost("test_index", null));

				//do cluster.health 
				this.Do(()=> this._client.ClusterHealthGet(nv=>nv
					.Add("wait_for_status", @"yellow")
				));

				//do indices.close 
				this.Do(()=> this._client.IndicesClosePost("test_index"));

				//do search 
				this.Do(()=> this._client.SearchGet("test_index"));

				//do indices.open 
				this.Do(()=> this._client.IndicesOpenPost("test_index"));

				//do cluster.health 
				this.Do(()=> this._client.ClusterHealthGet(nv=>nv
					.Add("wait_for_status", @"yellow")
				));

				//do search 
				this.Do(()=> this._client.SearchGet("test_index"));

			}
		}
	}
}

