using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Nest
{
    public interface IPutScriptResponse : IResponse
    {
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class PutScriptResponse : BaseResponse, IPutScriptResponse
    {
    }
}
