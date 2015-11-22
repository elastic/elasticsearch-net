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
	public enum GapPolicy
	{
		[EnumMember(Value = "skip")]
		Skip,
		[EnumMember(Value = "insert_zeros")]
		InsertZeros
	}
}
