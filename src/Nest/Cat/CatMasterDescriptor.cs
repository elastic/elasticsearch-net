using Elasticsearch.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ICatMasterRequest : IRequest<CatMasterRequestParameters>
	{
	}

	public partial class CatMasterRequest : BasePathRequest<CatMasterRequestParameters>, ICatMasterRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<CatMasterRequestParameters> pathInfo)
		{
			CatRequestPathInfo.Update(pathInfo);
		}
	}

	public partial class CatMasterDescriptor : BasePathDescriptor<CatMasterDescriptor, CatMasterRequestParameters>, ICatMasterRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<CatMasterRequestParameters> pathInfo)
		{
			CatRequestPathInfo.Update(pathInfo);
		}
	}
}
