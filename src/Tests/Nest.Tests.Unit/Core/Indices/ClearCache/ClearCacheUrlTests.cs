using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.Core.Indices.ClearCache
{
    [TestFixture]
    public class ClearCacheUrlTests : BaseJsonTests
    {
        [Test]
        public void FluentIncludesFilterKeys()
        {
            var result = this._client.ClearCache(c => c
                .FilterCache()
                .FilterKeys("filter_key_1", "filter_key_2")
            );

            var status = result.ConnectionStatus;
            StringAssert.Contains("USING NEST IN MEMORY CONNECTION", result.ConnectionStatus.ResponseRaw.Utf8String());
            StringAssert.EndsWith(
                "/_cache/clear?filter_cache=true&filter_keys=filter_key_1%2Cfilter_key_2", 
                status.RequestUrl);
            StringAssert.AreEqualIgnoringCase("POST", status.RequestMethod);
        }

        [Test]
        public void ObjectInitializerIncludesFilterKeys()
        {
            var request = new ClearCacheRequest
            {
                FilterCache = true,
                FilterCacheKeys = new[]
                {
                    "filter_key_1",
                    "filter_key_2"
                }
            };

            var result = this._client.ClearCache(request);

            var status = result.ConnectionStatus;
            StringAssert.Contains("USING NEST IN MEMORY CONNECTION", result.ConnectionStatus.ResponseRaw.Utf8String());
            StringAssert.EndsWith(
                "/_cache/clear?filter_cache=true&filter_keys=filter_key_1%2Cfilter_key_2",
                status.RequestUrl);
            StringAssert.AreEqualIgnoringCase("POST", status.RequestMethod);
        }
    }
}
