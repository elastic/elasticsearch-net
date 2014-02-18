using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesOptimize1
{
	public partial class IndicesOptimize1YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class OptimizeIndexTests1Tests : YamlTestsBase
		{
			[Test]
			public void OptimizeIndexTests1Test()
			{	

				//do indices.create 
				this.Do(()=> this._client.IndicesCreatePut("testing", null));

				//do indices.optimize 
				this.Do(()=> this._client.IndicesOptimizePost("testing", nv=>nv
					.Add("max_num_segments", 1)
				));

			}
		}
	}
}

