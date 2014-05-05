using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;

namespace Nest
{
    public partial class MtermvectorsDescriptor<T> : DocumentPathDescriptorBase<MtermvectorsDescriptor<T>, T, MtermvectorsRequestParameters>
        , IPathInfo<MtermvectorsRequestParameters> where T : class
    {
        ElasticsearchPathInfo<MtermvectorsRequestParameters> IPathInfo<MtermvectorsRequestParameters>.ToPathInfo(IConnectionSettingsValues settings)
        {
            var pathInfo = base.ToPathInfo(settings, this._QueryString);
            pathInfo.HttpMethod = PathInfoHttpMethod.POST;

            return pathInfo;
        }
    }
}
