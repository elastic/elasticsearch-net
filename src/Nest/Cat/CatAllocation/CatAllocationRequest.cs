using Elasticsearch.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ICatAllocationRequest : IRequest<CatAllocationRequestParameters> { }

	public partial class CatAllocationRequest : BasePathRequest<CatAllocationRequestParameters>, ICatAllocationRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<CatAllocationRequestParameters> pathInfo) =>
			CatRequestPathInfo.Update(pathInfo);
	}

	public partial class CatAllocationDescriptor : BasePathDescriptor<CatAllocationDescriptor, CatAllocationRequestParameters>, ICatAllocationRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<CatAllocationRequestParameters> pathInfo) =>
			CatRequestPathInfo.Update(pathInfo);
	}
}
