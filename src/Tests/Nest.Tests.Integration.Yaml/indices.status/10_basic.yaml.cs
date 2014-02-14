using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesStatus1
{
	public partial class IndicesStatus1YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class IndicesStatusTest1Tests : YamlTestsBase
		{
			[Test]
			public void IndicesStatusTest1Test()
			{	

				//do indices.status 
				this.Do(()=> this._client.IndicesStatusGetForAll());

				//do indices.status 
				this.Do(()=> this._client.IndicesStatusGet("not_here"), shouldCatch: @"missing");

			}
		}
	}
}

