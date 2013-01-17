using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Nest.Resolvers.Converters;

namespace Nest
{
	[JsonConverter(typeof(TermConverter))]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class Wildcard : Term, IQuery
	{
		public Wildcard()
		{

		}
	}
}
