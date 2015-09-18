using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGetAliasRequest : IRequest<GetAliasRequestParameters>
	{
		[JsonIgnore]
		string Alias { get; set; }
	}

	internal static class GetAliasPathInfo
	{
		public static void Update(RequestPath pathInfo, IGetAliasRequest request)
		{
			pathInfo.HttpMethod = HttpMethod.GET;
			pathInfo.Name = request.Alias ?? "*";
		}
	}
	
	public partial class GetAliasRequest : RequestBase<GetAliasRequestParameters>, IGetAliasRequest
	{
		public string Alias { get; set; }
		
		protected override void UpdateRequestPath(IConnectionSettingsValues settings, RequestPath pathInfo)
		{
			GetAliasPathInfo.Update(pathInfo, this);
		}
	}

	[DescriptorFor("IndicesGetAlias")]
	public partial class GetAliasDescriptor 
		: RequestDescriptorBase<GetAliasDescriptor, GetAliasRequestParameters>, IGetAliasRequest
	{

		private IGetAliasRequest Self => this;

		string IGetAliasRequest.Alias { get; set; }

		public GetAliasDescriptor Alias(string alias)
		{
			Self.Alias = alias;
			return this;
		}

		protected override void UpdateRequestPath(IConnectionSettingsValues settings, RequestPath pathInfo)
		{
			GetAliasPathInfo.Update(pathInfo, this);
		}
	}
}
