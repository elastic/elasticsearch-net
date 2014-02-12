using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesStatus
{
	public partial class IndicesStatusTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class IndicesStatusTestTests : YamlTestsBase
		{
			[Test]
			public void IndicesStatusTestTest()
			{	

				//do indices.status 
				this.Do(()=> this._client.IndicesStatusGet());

				//is_true _response.ok; 
				this.IsTrue(_response.ok);

				//do indices.status 
				this.Do(()=> this._client.IndicesStatusGet("not_here"), shouldCatch: @"missing");

			}
		}
	}
}

