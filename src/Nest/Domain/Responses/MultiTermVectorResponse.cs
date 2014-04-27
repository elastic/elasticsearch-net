using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Nest
{
    public interface IMultiTermVectorResponse : IResponse
    {
        IEnumerable<TermVectorResponse> Documents { get; }
    }

    [JsonObject]
    public class MultiTermVectorResponse : BaseResponse, IMultiTermVectorResponse
    {
        public MultiTermVectorResponse()
        {
            IsValid = true;
        }

        [JsonProperty("docs")]
        public IEnumerable<TermVectorResponse> Documents { get; internal set; }
    }
}
