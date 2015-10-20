using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	// TODO come up with a better name for this, or replace as ReadAsTypeConverter
	internal class ReadAsTypeInternalConstructorJsonConverter<T> : JsonConverter
		where T : class
	{
		public override bool CanRead => true;
		public override bool CanWrite => false;

		public override bool CanConvert(Type objectType) => true;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			// TODO is Activator.CreateInstance better?
			var flags = BindingFlags.NonPublic | BindingFlags.Instance;
			var t = (T)typeof(T).GetConstructor(flags, null, new Type[]{}, null).Invoke(new object[]{});
			serializer.Populate(reader, t);
			return t;
		}
	}
}
