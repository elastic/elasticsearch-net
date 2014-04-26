using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;

namespace Nest
{
    public partial class TermvectorDescriptor<T> : DocumentPathDescriptorBase<TermvectorDescriptor<T>, T, TermvectorRequestParameters>
        , IPathInfo<TermvectorRequestParameters> where T : class
    {
        ElasticsearchPathInfo<TermvectorRequestParameters> IPathInfo<TermvectorRequestParameters>.ToPathInfo(IConnectionSettingsValues settings)
        {
            var pathInfo = base.ToPathInfo<TermvectorRequestParameters>(settings, this._QueryString);
            pathInfo.HttpMethod = PathInfoHttpMethod.GET;

            return pathInfo;
        }
    }
}
