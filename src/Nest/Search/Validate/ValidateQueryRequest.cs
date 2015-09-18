using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IValidateQueryRequest : IRequest<ValidateQueryRequestParameters>
	{
		[JsonProperty("query")]
		IQueryContainer Query { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IValidateQueryRequest<T> : IValidateQueryRequest
		where T : class
	{ }

	internal static class ValidateQueryPathInfo
	{
		public static void Update(RequestPath pathInfo, IValidateQueryRequest request)
		{
			var source = request.RequestParameters.GetQueryStringValue<string>("source");
			var q = request.RequestParameters.GetQueryStringValue<string>("q");
			pathInfo.HttpMethod = (!source.IsNullOrEmpty() || !q.IsNullOrEmpty())
				? HttpMethod.GET
				: HttpMethod.POST;
		}
	}

	public partial class ValidateQueryRequest : RequestBase<ValidateQueryRequestParameters>, IValidateQueryRequest
	{
		public IQueryContainer Query { get; set; }

		protected override void UpdateRequestPath(IConnectionSettingsValues settings, RequestPath pathInfo)
		{
			ValidateQueryPathInfo.Update(pathInfo, this);
		}
	}

	public partial class ValidateQueryRequest<T> : RequestBase<ValidateQueryRequestParameters>, IValidateQueryRequest<T>
		where T : class
	{
		public IQueryContainer Query { get; set; }

		protected override void UpdateRequestPath(IConnectionSettingsValues settings, RequestPath pathInfo)
		{
			ValidateQueryPathInfo.Update(pathInfo, this);
		}
	}

	[DescriptorFor("IndicesValidateQuery")]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public partial class ValidateQueryDescriptor<T> : RequestDescriptorBase<ValidateQueryDescriptor<T>,  ValidateQueryRequestParameters>, IValidateQueryRequest<T>
		where T : class
	{
		private IValidateQueryRequest Self => this;

		IQueryContainer IValidateQueryRequest.Query { get; set; }

		public ValidateQueryDescriptor<T> Query(Func<QueryContainerDescriptor<T>, QueryContainer> querySelector)
		{
			Self.Query = querySelector(new QueryContainerDescriptor<T>());
			return this;
		}

		protected override void UpdateRequestPath(IConnectionSettingsValues settings, RequestPath pathInfo)
		{
			ValidateQueryPathInfo.Update(pathInfo, this);
		}
	}
}
