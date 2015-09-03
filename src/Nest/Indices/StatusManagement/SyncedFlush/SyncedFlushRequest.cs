using Elasticsearch.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public interface ISyncedFlushRequest : IIndicesOptionalExplicitAllPath<SyncedFlushRequestParameters>
	{
	}

	internal static class SyncedFlushPathInfo
	{
		public static void Update(IConnectionSettingsValues settings, ElasticsearchPathInfo<SyncedFlushRequestParameters> pathInfo)
		{
			pathInfo.HttpMethod = HttpMethod.POST;
		}
	}

	public partial class SyncedFlushRequest : IndicesOptionalExplicitAllPathBase<SyncedFlushRequestParameters>, ISyncedFlushRequest
	{
		public SyncedFlushRequest(Indices indices) : base(indices) { }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<SyncedFlushRequestParameters> pathInfo)
		{
			SyncedFlushPathInfo.Update(settings, pathInfo);
		}
	}
	
	[DescriptorFor("IndicesFlushSynced")]
	public partial class SyncedFlushDescriptor 
		: IndicesOptionalExplicitAllPathDescriptor<SyncedFlushDescriptor, SyncedFlushRequestParameters>
		, ISyncedFlushRequest
	{
		public SyncedFlushDescriptor(Indices indices) : base(indices) { }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<SyncedFlushRequestParameters> pathInfo)
		{
			SyncedFlushPathInfo.Update(settings, pathInfo);
		}
	}
}
