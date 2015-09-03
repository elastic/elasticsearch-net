using Elasticsearch.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public interface IFieldStatsRequest : IIndicesOptionalExplicitAllPath<FieldStatsRequestParameters> { }

	internal static class FieldStatsPathInfo
	{
		public static void Update(IConnectionSettingsValues settings, ElasticsearchPathInfo<FieldStatsRequestParameters> pathInfo) =>
			pathInfo.HttpMethod = HttpMethod.GET;
	}

	public partial class FieldStatsRequest : IndicesOptionalExplicitAllPathBase<FieldStatsRequestParameters>, IFieldStatsRequest
	{
		public FieldStatsRequest(Indices indices) : base(indices) { }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<FieldStatsRequestParameters> pathInfo) =>
			FieldStatsPathInfo.Update(settings, pathInfo);
	}

	public partial class FieldStatsDescriptor 
		: IndicesOptionalExplicitAllPathDescriptor<FieldStatsDescriptor, FieldStatsRequestParameters>
		, IFieldStatsRequest
	{
		public FieldStatsDescriptor(Indices indices) : base(indices) { }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<FieldStatsRequestParameters> pathInfo) =>
			FieldStatsPathInfo.Update(settings, pathInfo);	
	}
}
