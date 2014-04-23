using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;

namespace Nest
{
    public partial class TermvectorDescriptor<T> : DocumentPathDescriptorBase<TermvectorDescriptor<T>, T, TermvectorQueryString>
        , IPathInfo<TermvectorQueryString> where T : class
    {
        ElasticsearchPathInfo<TermvectorQueryString> IPathInfo<TermvectorQueryString>.ToPathInfo(IConnectionSettingsValues settings)
        {
            var pathInfo = base.ToPathInfo<TermvectorQueryString>(settings, this._QueryString);
            pathInfo.HttpMethod = PathInfoHttpMethod.GET;

            return pathInfo;
        }
    }
}
