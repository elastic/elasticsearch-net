using Elasticsearch.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ICatFielddataRequest : IRequest<CatFielddataRequestParameters>
	{
	}

	public partial class CatFielddataRequest : BasePathRequest<CatFielddataRequestParameters>, ICatFielddataRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<CatFielddataRequestParameters> pathInfo)
		{
			CatRequestPathInfo.Update(pathInfo);
		}
	}

	public partial class CatFielddataDescriptor : BasePathDescriptor<CatFielddataDescriptor, CatFielddataRequestParameters>, ICatFielddataRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<CatFielddataRequestParameters> pathInfo)
		{
			CatRequestPathInfo.Update(pathInfo);
		}
	}
}
