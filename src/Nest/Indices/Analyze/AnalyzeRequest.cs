using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IAnalyzeRequest : IIndicesOptionalPath<AnalyzeRequestParameters> { }

	public partial class AnalyzeRequest : IndicesOptionalPathBase<AnalyzeRequestParameters>, IAnalyzeRequest
	{
		public AnalyzeRequest(string textToAnalyze)
		{
			this.Text = new[] { textToAnalyze };
		}
	}

	[DescriptorFor("IndicesAnalyze")]
	public partial class AnalyzeDescriptor : IndicesOptionalPathDescriptor<AnalyzeDescriptor, AnalyzeRequestParameters>, IAnalyzeRequest
	{
	}
}
