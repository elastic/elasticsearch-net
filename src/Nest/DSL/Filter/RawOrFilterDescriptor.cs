using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Nest.Resolvers.Converters;

namespace Nest
{
	public interface IRawOrFilterDescriptor
	{
		string Raw { get; set; }
		BaseFilter Descriptor { get; set; }
	}

	[JsonConverter(typeof(CustomJsonConverter))]
	public class RawOrFilterDescriptor<T> : ICustomJson, IRawOrFilterDescriptor where T : class
	{
		public string Raw { get; set; }
		public BaseFilter Descriptor { get; set; }
		
		object ICustomJson.GetCustomJson()
		{
			return this.Descriptor != null ? (object) Descriptor : new RawJson(this.Raw);
		}
	}
}
