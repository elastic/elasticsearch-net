using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elasticsearch.Net
{
    public partial class ClearCacheRequestParameters
    {
        [Obsolete("Use FilterKeys(params string[] filter_keys)", true)]
        public ClearCacheRequestParameters FilterKeys(bool filter_keys)
        {
            return this;
        }
    }
}
