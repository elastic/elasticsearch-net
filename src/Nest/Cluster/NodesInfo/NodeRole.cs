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
	public enum NodeRole
	{
		[EnumMember(Value = "master")]
		Master,
		[EnumMember(Value = "data")]
		Data,
		[EnumMember(Value = "client")]
		Client,
		[EnumMember(Value = "ingest")]
		Ingest
	}
}
