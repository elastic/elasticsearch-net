using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Nest.Resolvers.Converters;

namespace Nest
{
	[JsonConverter(typeof(TermConverter))]
	public class Prefix : Term
	{
		public Prefix()
		{
		
		}
	}
}
