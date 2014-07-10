using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{


	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IClearScrollRequest : IRequest<ClearScrollRequestParameters>
	{
		string ScrollId { get; set; }
	}

	internal static class ClearScrollPathInfo
	{
		public static void Update(ElasticsearchPathInfo<ClearScrollRequestParameters> pathInfo, IClearScrollRequest request)
		{
			if (request.ScrollId.IsNullOrEmpty())
				throw new DslException("missing ScrollId()");

			pathInfo.ScrollId = request.ScrollId;
			pathInfo.HttpMethod = PathInfoHttpMethod.DELETE;
		}
	}
	
	public partial class ClearScrollRequest : BasePathRequest<ClearScrollRequestParameters>, IClearScrollRequest
	{
		public string ScrollId { get; set; }

		public ClearScrollRequest(string scrollId)
		{
			this.ScrollId = scrollId;
		}

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<ClearScrollRequestParameters> pathInfo)
		{
			ClearScrollPathInfo.Update(pathInfo, this);
		}

	}
	[DescriptorFor("ClearScroll")]
	public partial class ClearScrollDescriptor : BasePathDescriptor<ClearScrollDescriptor, ClearScrollRequestParameters>, IClearScrollRequest
	{
		private IClearScrollRequest Self { get { return this; } }

		string IClearScrollRequest.ScrollId { get; set; }

		/// <summary>
		/// Specify the {name} part of the operation
		/// </summary>
		public ClearScrollDescriptor ScrollId(string scrollId)
		{
			Self.ScrollId = scrollId;
			return this;
		}

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<ClearScrollRequestParameters> pathInfo)
		{
			ClearScrollPathInfo.Update(pathInfo, this);
		}
	}
}
