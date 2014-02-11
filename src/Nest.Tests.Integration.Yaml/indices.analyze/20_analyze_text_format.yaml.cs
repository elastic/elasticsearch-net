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


		public class TextFormatTests : YamlTestsBase
		{
			[Test]
			public void TextFormatTest()
			{	

				//do indices.analyze 
				_status = this._client.IndicesAnalyzeGet(nv=>nv
					.Add("format","text")
					.Add("text","tHE BLACK and white! AND red")
				);
				_response = _status.Deserialize<dynamic>();

			}
		}
	}
}

