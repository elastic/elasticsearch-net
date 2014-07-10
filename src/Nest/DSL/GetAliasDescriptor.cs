using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGetAliasRequest : IIndicesOptionalPath<GetAliasRequestParameters>
	{
		[JsonIgnore]
		string Alias { get; set; }
	}

	internal static class GetAliasPathInfo
	{
		public static void Update(ElasticsearchPathInfo<GetAliasRequestParameters> pathInfo, IGetAliasRequest request)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;
			pathInfo.Name = request.Alias ?? "*";
		}
	}
	
	public partial class GetAliasRequest : IndicesOptionalPathBase<GetAliasRequestParameters>, IGetAliasRequest
	{
		public string Alias { get; set; }
		
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<GetAliasRequestParameters> pathInfo)
		{
			GetAliasPathInfo.Update(pathInfo, this);
		}
	}

	[DescriptorFor("IndicesGetAlias")]
	public partial class GetAliasDescriptor 
		: IndicesOptionalPathDescriptor<GetAliasDescriptor, GetAliasRequestParameters>, IGetAliasRequest
	{

		private IGetAliasRequest Self { get { return this; } }

		string IGetAliasRequest.Alias { get; set; }

		public GetAliasDescriptor Alias(string alias)
		{
			Self.Alias = alias;
			return this;
		}

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<GetAliasRequestParameters> pathInfo)
		{
			GetAliasPathInfo.Update(pathInfo, this);
		}
	}
}
