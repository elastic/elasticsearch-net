using System;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IDeleteScriptRequest : IRequest<DeleteScriptRequestParameters>
	{
		[JsonProperty("lang")]
		string Lang { get; set; }
		[JsonProperty("id")]
		string Id { get; set; }
	}

	public partial class DeleteScriptRequest : PathRequestBase<DeleteScriptRequestParameters>, IDeleteScriptRequest
	{
		public string Lang { get; set; }
		public string Id { get; set; }

		protected override void UpdateRequestPath(IConnectionSettingsValues settings, RequestPath<DeleteScriptRequestParameters> pathInfo)
		{
			DeleteScriptPathInfo.Update(pathInfo, this);
		}
	}

	internal static class DeleteScriptPathInfo
	{
		public static void Update(RequestPath<DeleteScriptRequestParameters> pathInfo, IDeleteScriptRequest request)
		{
			pathInfo.Id = request.Id;
			pathInfo.Lang = request.Lang;
			pathInfo.HttpMethod = HttpMethod.DELETE;
		}
	}

	[DescriptorFor("ScriptDelete")]
	public partial class DeleteScriptDescriptor : RequestDescriptorBase<DeleteScriptDescriptor, DeleteScriptRequestParameters>, IDeleteScriptRequest
	{
		IDeleteScriptRequest Self => this;
		string IDeleteScriptRequest.Lang { get; set; }
		string IDeleteScriptRequest.Id { get; set; }

		public DeleteScriptDescriptor Id(string id)
		{
			this.Self.Id = id;
			return this;
		}

		public DeleteScriptDescriptor Lang(ScriptLang lang)
		{
			this.Self.Lang = lang.GetStringValue();
			return this;
		}

		public DeleteScriptDescriptor Lang(string lang)
		{
			this.Self.Lang = lang;
			return this;
		}

		protected override void UpdateRequestPath(IConnectionSettingsValues settings, RequestPath<DeleteScriptRequestParameters> pathInfo)
		{
			DeleteScriptPathInfo.Update(pathInfo, this);
		}
	}
}