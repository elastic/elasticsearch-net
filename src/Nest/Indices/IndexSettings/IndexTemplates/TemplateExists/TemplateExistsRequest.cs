using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ITemplateExistsRequest : INamePath<TemplateExistsRequestParameters> { }

	internal static class TemplateExistsPathInfo
	{
		public static void Update(ElasticsearchPathInfo<TemplateExistsRequestParameters> pathInfo, ITemplateExistsRequest request)
		{
			pathInfo.HttpMethod = HttpMethod.HEAD;
		}
	}
	
	public partial class TemplateExistsRequest : NamePathBase<TemplateExistsRequestParameters>, ITemplateExistsRequest
	{
		public TemplateExistsRequest(string name) : base(name) { }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<TemplateExistsRequestParameters> pathInfo)
		{
			TemplateExistsPathInfo.Update(pathInfo, this);
		}
	}
	[DescriptorFor("IndicesExistsTemplate")]
	public partial class TemplateExistsDescriptor : NamePathDescriptor<TemplateExistsDescriptor, TemplateExistsRequestParameters>, ITemplateExistsRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<TemplateExistsRequestParameters> pathInfo)
		{
			TemplateExistsPathInfo.Update(pathInfo, this);
		}
	}
}
