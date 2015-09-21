using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IAnalyzeRequest { }

	public partial class AnalyzeRequest 
	{
		public AnalyzeRequest(string textToAnalyze)
		{
			this.Text = new[] { textToAnalyze };
		}
	}

	[DescriptorFor("IndicesAnalyze")]
	public partial class AnalyzeDescriptor { }
}
