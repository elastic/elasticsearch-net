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
    }

    public partial class PutScriptRequest : BasePathRequest<PutScriptRequestParameters>, IPutScriptRequest
    {
        public string Lang { get; set; }
        public string Id { get; set; }
        public string Script { get; set; }

        protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<PutScriptRequestParameters> pathInfo)
        {
            PutScriptPathInfo.Update(pathInfo, Id, Lang);
        }
    }

    internal static class PutScriptPathInfo
    {
        public static void Update(ElasticsearchPathInfo<PutScriptRequestParameters> pathInfo, string id, string lang)
        {
            pathInfo.Id = id;
            pathInfo.Lang = lang;
            pathInfo.HttpMethod = PathInfoHttpMethod.POST;
        }
    }

    [DescriptorFor("ScriptPut")]
    public partial class PutScriptDescriptor : BasePathDescriptor<PutScriptDescriptor, PutScriptRequestParameters>, IPutScriptRequest
    {
        private string _id;
        private string _lang;

        IPutScriptRequest Self { get { return this; } }
        string IPutScriptRequest.Script { get; set; }

        public PutScriptDescriptor Id(string id)
        {
            id.ThrowIfNullOrEmpty("id");
            this._id = id;
            return this;
        }

        public PutScriptDescriptor Lang(string lang)
        {
            lang.ThrowIfNullOrEmpty("lang");
            this._lang = lang;
            return this;
        }

        public PutScriptDescriptor Script(string script)
        {
            this.Self.Script = script;
            return this;
        }

        protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<PutScriptRequestParameters> pathInfo)
        {
            PutScriptPathInfo.Update(pathInfo, _id, _lang);
        }
    }
}