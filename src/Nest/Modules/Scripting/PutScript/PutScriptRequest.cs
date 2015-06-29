using System.IO;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public interface IPutScriptRequest : IRequest<PutScriptRequestParameters>
    {
        [JsonProperty("script")]
        string Script { get; set; }
        [JsonIgnore]
        string Id { get; set; }
        [JsonIgnore]
        string Lang { get; set; }
    }

    public partial class PutScriptRequest : BasePathRequest<PutScriptRequestParameters>, IPutScriptRequest
    {
        public string Lang { get; set; }
        public string Id { get; set; }
        public string Script { get; set; }

        protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<PutScriptRequestParameters> pathInfo)
        {
            PutScriptPathInfo.Update(pathInfo, this);
        }
    }

    internal static class PutScriptPathInfo
    {
        public static void Update(ElasticsearchPathInfo<PutScriptRequestParameters> pathInfo, IPutScriptRequest putScriptRequest)
        {
            pathInfo.Id = putScriptRequest.Id;
            pathInfo.Lang = putScriptRequest.Lang;
            pathInfo.HttpMethod = PathInfoHttpMethod.POST;
        }
    }

    [DescriptorFor("ScriptPut")]
    public partial class PutScriptDescriptor : BasePathDescriptor<PutScriptDescriptor, PutScriptRequestParameters>, IPutScriptRequest
    {
        IPutScriptRequest Self => this;
        string IPutScriptRequest.Script { get; set; }
        string IPutScriptRequest.Id { get; set; }
        string IPutScriptRequest.Lang { get; set; }

        public PutScriptDescriptor Id(string id)
        {
            id.ThrowIfNullOrEmpty("id");
            this.Self.Id = id;
            return this;
        }

        public PutScriptDescriptor Lang(ScriptLang lang)
        {
            this.Self.Lang = lang.GetStringValue();
            return this;
        }

        public PutScriptDescriptor Lang(string lang)
        {
            lang.ThrowIfNullOrEmpty("lang");
            this.Self.Lang = lang;
            return this;
        }

        public PutScriptDescriptor Script(string script)
        {
            this.Self.Script = script;
            return this;
        }

        protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<PutScriptRequestParameters> pathInfo)
        {
            PutScriptPathInfo.Update(pathInfo, this);
        }
    }
}