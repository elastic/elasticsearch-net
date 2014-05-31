using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
    public enum ScoreMode
    {
        avg,
        first,
        max,
        min,
        multiply,
        total,
		sum
    }
}
