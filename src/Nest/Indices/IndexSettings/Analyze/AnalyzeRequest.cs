using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IAnalyzeRequest : IIndicesOptionalPath<AnalyzeRequestParameters>
	{
	}

	internal static class AnalyzePathInfo
	{
		public static void Update(ElasticsearchPathInfo<AnalyzeRequestParameters> pathInfo, IAnalyzeRequest request)
		{
			pathInfo.HttpMethod = HttpMethod.POST;
		}
	}
	
	public partial class AnalyzeRequest : IndicesOptionalPathBase<AnalyzeRequestParameters>, IAnalyzeRequest
	{
		public AnalyzeRequest(string textToAnalyze)
		{
			this.Text = textToAnalyze;
		}


		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<AnalyzeRequestParameters> pathInfo)
		{
			AnalyzePathInfo.Update(pathInfo, this);
		}
	}

	[DescriptorFor("IndicesAnalyze")]
	public partial class AnalyzeDescriptor : IndicesOptionalPathDescriptor<AnalyzeDescriptor, AnalyzeRequestParameters>, IAnalyzeRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<AnalyzeRequestParameters> pathInfo)
		{
			AnalyzePathInfo.Update(pathInfo, this);
		}
	}
}
