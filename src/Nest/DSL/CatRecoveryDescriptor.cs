using Elasticsearch.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ICatRecoveryRequest : IRequest<CatRecoveryRequestParameters>
	{
	}

	public partial class CatRecoveryRequest : BasePathRequest<CatRecoveryRequestParameters>, ICatRecoveryRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<CatRecoveryRequestParameters> pathInfo)
		{
			CatRequestPathInfo.Update(pathInfo);
		}
	}

	public partial class CatRecoveryDescriptor : BasePathDescriptor<CatRecoveryDescriptor, CatRecoveryRequestParameters>, ICatRecoveryRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<CatRecoveryRequestParameters> pathInfo)
		{
			CatRequestPathInfo.Update(pathInfo);
		}
	}
}
