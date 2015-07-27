using Elasticsearch.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	internal static class UpgradeStatusPathInfo
	{
		public static void Update(ElasticsearchPathInfo<UpgradeStatusRequestParameters> pathInfo)
		{
			pathInfo.HttpMethod = HttpMethod.GET;
		}
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IUpgradeStatusRequest : IIndicesOptionalPath<UpgradeStatusRequestParameters>
	{
	}

	public partial class UpgradeStatusRequest : IndicesOptionalPathBase<UpgradeStatusRequestParameters>, IUpgradeStatusRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<UpgradeStatusRequestParameters> pathInfo)
		{
			UpgradeStatusPathInfo.Update(pathInfo);
		}
	}

	[DescriptorFor("IndicesGetUpgrade")]
	public partial class UpgradeStatusDescriptor
		: IndicesOptionalPathDescriptor<UpgradeStatusDescriptor, UpgradeStatusRequestParameters>, IUpgradeStatusRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<UpgradeStatusRequestParameters> pathInfo)
		{
			UpgradeStatusPathInfo.Update(pathInfo);
		}
	}
}
