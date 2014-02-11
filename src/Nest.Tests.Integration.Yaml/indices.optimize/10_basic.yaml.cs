using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesOptimize
{
	public partial class IndicesOptimizeTests
	{	


		public class OptimizeIndexTestsTests : YamlTestsBase
		{
			[Test]
			public void OptimizeIndexTestsTest()
			{	

				//do indices.create 
				this.Do(()=> this._client.IndicesCreatePost("testing", null));

				//do indices.optimize 
				this.Do(()=> this._client.IndicesOptimizeGet("testing", nv=>nv
					.Add("max_num_segments", 1)
				));

				//is_true _response.ok; 
				this.IsTrue(_response.ok);

			}
		}
	}
}

