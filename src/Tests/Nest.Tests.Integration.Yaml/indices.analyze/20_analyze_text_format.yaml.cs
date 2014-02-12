using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesAnalyze
{
	public partial class IndicesAnalyzeTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class TextFormatTests : YamlTestsBase
		{
			[Test]
			public void TextFormatTest()
			{	

				//do indices.analyze 
				this.Do(()=> this._client.IndicesAnalyzeGet(nv=>nv
					.Add("format", @"text")
					.Add("text", @"tHE BLACK and white! AND red")
				));

				//match _response.tokens: 
				this.IsMatch(_response.tokens, @"[black:4->9:<ALPHANUM>]

4: 
[white:14->19:<ALPHANUM>]

6: 
[red:25->28:<ALPHANUM>]
");

			}
		}
	}
}

