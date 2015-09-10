using Elasticsearch.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IDeleteSearchTemplateRequest : INamePath<DeleteTemplateRequestParameters> { }

	public partial class DeleteSearchTemplateRequest 
		: NamePathBase<DeleteTemplateRequestParameters>, IDeleteSearchTemplateRequest
	{
		public DeleteSearchTemplateRequest(string templateName) : base(templateName) { }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<DeleteTemplateRequestParameters> pathInfo)
		{
			DeleteSearchTemplatePathInfo.Update(pathInfo, this);
		}
	}

	internal static class DeleteSearchTemplatePathInfo
	{
		public static void Update(ElasticsearchPathInfo<DeleteTemplateRequestParameters> pathInfo, IDeleteSearchTemplateRequest request)
		{
			pathInfo.Id = request.Name;
			pathInfo.HttpMethod = HttpMethod.DELETE;
		}
	}

	public partial class DeleteSearchTemplateDescriptor 
		: NamePathDescriptor<DeleteSearchTemplateDescriptor, DeleteTemplateRequestParameters>, IDeleteSearchTemplateRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<DeleteTemplateRequestParameters> pathInfo)
		{
			DeleteSearchTemplatePathInfo.Update(pathInfo, this);
		}
	}
}
