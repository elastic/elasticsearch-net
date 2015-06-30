using Elasticsearch.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ICatSegmentsRequest : IRequest<CatSegmentsRequestParameters> { }

	public partial class CatSegmentsRequest : BasePathRequest<CatSegmentsRequestParameters>, ICatSegmentsRequest 
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<CatSegmentsRequestParameters> pathInfo) =>
			CatRequestPathInfo.Update(pathInfo);
	}

	public partial class CatSegmentsDescriptor : BasePathDescriptor<CatSegmentsDescriptor, CatSegmentsRequestParameters>, ICatSegmentsRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<CatSegmentsRequestParameters> pathInfo) =>
			CatRequestPathInfo.Update(pathInfo);
	}
}
