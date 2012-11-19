using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Nest.Resolvers.Converters;

namespace Nest
{
	[JsonConverter(typeof(RawOrQueryDescriptorConverter))]
	public class RawOrQueryDescriptor<T> where T : class
	{
		public string Raw { get; set; }
		public BaseQuery Descriptor { get; set; }

		internal bool IsConditionless
		{
			get
			{
				return this.Raw.IsNullOrEmpty() && Descriptor.IsConditionlessQueryDescriptor;
			}
		}
	}
}
