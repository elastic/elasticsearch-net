using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGetScriptRequest : IRequest<GetScriptRequestParameters>
	{
		[JsonProperty("lang")]
		string Lang { get; set; }
		[JsonProperty("id")]
		string Id { get; set; }
	}

	public partial class GetScriptRequest : RequestBase<GetScriptRequestParameters>, IGetScriptRequest
	{
		public string Lang { get; set; }
		public string Id { get; set; }
	}

	[DescriptorFor("ScriptGet")]
	public partial class GetScriptDescriptor : RequestDescriptorBase<GetScriptDescriptor, GetScriptRequestParameters>, IGetScriptRequest
	{
		IGetScriptRequest Self => this;
		string IGetScriptRequest.Lang { get; set; }
		string IGetScriptRequest.Id { get; set; }

		public GetScriptDescriptor Id(string id)
		{
			this.Self.Id = id;
			return this;
		}

		public GetScriptDescriptor Lang(ScriptLang lang)
		{
			this.Self.Lang = lang.GetStringValue();
			return this;
		}

		public GetScriptDescriptor Lang(string lang)
		{
			this.Self.Lang = lang;
			return this;
		}
	}
}