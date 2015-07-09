using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IRecoveryStatusRequest : IIndicesOptionalPath<RecoveryStatusRequestParameters> { }

	internal static class RecoveryStatusPathInfo
	{
		public static void Update(ElasticsearchPathInfo<RecoveryStatusRequestParameters> pathInfo, IRecoveryStatusRequest request)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;
		}
	}
	
	public partial class RecoveryStatusRequest : IndicesOptionalPathBase<RecoveryStatusRequestParameters>, IRecoveryStatusRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<RecoveryStatusRequestParameters> pathInfo)
		{
			RecoveryStatusPathInfo.Update(pathInfo, this);
		}
	}

	[DescriptorFor("IndicesRecovery")]
	public partial class RecoveryStatusDescriptor : IndicesOptionalPathDescriptor<RecoveryStatusDescriptor, RecoveryStatusRequestParameters>, IRecoveryStatusRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<RecoveryStatusRequestParameters> pathInfo)
		{
			RecoveryStatusPathInfo.Update(pathInfo, this);
		}
	}
}
