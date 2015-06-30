using Elasticsearch.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ICatNodesRequest : IRequest<CatNodesRequestParameters> { }

	public partial class CatNodesRequest : BasePathRequest<CatNodesRequestParameters>, ICatNodesRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<CatNodesRequestParameters> pathInfo) =>
			CatRequestPathInfo.Update(pathInfo);
	}

	public partial class CatNodesDescriptor : BasePathDescriptor<CatNodesDescriptor, CatNodesRequestParameters>, ICatNodesRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<CatNodesRequestParameters> pathInfo) =>
			CatRequestPathInfo.Update(pathInfo);
	}
}
