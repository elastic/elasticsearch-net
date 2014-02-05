using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ICustomJsonConverter))]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class SpanTerm : Term, ISpanQuery
	{
	}
}
