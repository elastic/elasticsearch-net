using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	internal class RepositoryJsonConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType) => typeof(IRepository).IsAssignableFrom(objectType);
		public override bool CanRead => false;
		public override bool CanWrite => true;
		
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			// TODO	
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			throw new NotSupportedException();	
		}
	}
}
