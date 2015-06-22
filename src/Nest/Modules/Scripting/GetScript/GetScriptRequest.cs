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

    public partial class GetScriptRequest : BasePathRequest<GetScriptRequestParameters>, IGetScriptRequest
    {
        public string Lang { get; set; }
        public string Id { get; set; }

        protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<GetScriptRequestParameters> pathInfo)
        {
            GetScriptPathInfo.Update(pathInfo, this);
        }
    }

    internal static class GetScriptPathInfo
    {
        public static void Update(ElasticsearchPathInfo<GetScriptRequestParameters> pathInfo, IGetScriptRequest request)
        {
            pathInfo.Id = request.Id;
            pathInfo.Lang = request.Lang;
            pathInfo.HttpMethod = PathInfoHttpMethod.GET;
        }
    }

    [DescriptorFor("ScriptGet")]
    public partial class GetScriptDescriptor : BasePathDescriptor<GetScriptDescriptor, GetScriptRequestParameters>, IGetScriptRequest
    {
        IGetScriptRequest Self { get { return this; } }
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

        protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<GetScriptRequestParameters> pathInfo)
        {
            GetScriptPathInfo.Update(pathInfo, this);
        }
    }
}