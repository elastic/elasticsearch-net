using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum TermVectorOption
	{
		no,
		yes,
		with_offsets,
		with_positions,
		with_positions_offsets

	}
}
