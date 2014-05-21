using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Nest.Resolvers.Converters;

namespace Nest
{

	//TODO Remove!
	[JsonConverter(typeof(CustomJsonConverter))]
	public class RawOrQueryDescriptor<T> : ICustomJson where T : class
	{
		public string Raw { get; set; }
		public IQueryContainer Descriptor { get; set; }
	
		object ICustomJson.GetCustomJson()
		{
			return this.Descriptor != null ? (object) Descriptor : new RawJson(this.Raw);
		}
		
	}
}
