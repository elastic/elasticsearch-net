using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum SuggestSort
	{
		[EnumMember(Value = "score")]
		Score,
		[EnumMember(Value = "frequency")]
		Frequency
	}
}
