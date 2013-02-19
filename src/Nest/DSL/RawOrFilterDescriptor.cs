using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Nest.Resolvers.Converters;

namespace Nest
{
	[JsonConverter(typeof(RawOrQueryDescriptorConverter))]
	public class RawOrFilterDescriptor<T> where T : class
	{
		public string Raw { get; set; }
		public BaseFilter Descriptor { get; set; }
	}
}
