using System;
using System.Collections.Generic;
using System.Linq;
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
		public static void Update(RequestPath pathInfo, IClearScrollRequest request)
		{
			if (request.ScrollId.IsNullOrEmpty())
				throw new DslException("missing ScrollId()");

			pathInfo.ScrollId = request.ScrollId;
			pathInfo.HttpMethod = HttpMethod.DELETE;
		}
	}
	
	public partial class ClearScrollRequest : RequestBase<ClearScrollRequestParameters>, IClearScrollRequest
	{
		public string ScrollId { get; set; }

		public ClearScrollRequest(string scrollId)
		{
			this.ScrollId = scrollId;
		}

		protected override void UpdateRequestPath(IConnectionSettingsValues settings, RequestPath pathInfo)
		{
			ClearScrollPathInfo.Update(pathInfo, this);
		}

	}
	[DescriptorFor("ClearScroll")]
	public partial class ClearScrollDescriptor : RequestDescriptorBase<ClearScrollDescriptor, ClearScrollRequestParameters>, IClearScrollRequest
	{
		private IClearScrollRequest Self => this;

		string IClearScrollRequest.ScrollId { get; set; }

		/// <summary>
		/// Specify the {name} part of the operation
		/// </summary>
		public ClearScrollDescriptor ScrollId(string scrollId)
		{
			Self.ScrollId = scrollId;
			return this;
		}

		protected override void UpdateRequestPath(IConnectionSettingsValues settings, RequestPath pathInfo)
		{
			ClearScrollPathInfo.Update(pathInfo, this);
		}
	}
}
