using System;
using Newtonsoft.Json;

namespace Nest
{
	internal static class FromJson
	{
		/// <summary>
		/// Reads the json to T and returns an instance of it
		/// </summary>
		public static T ReadAs<T>(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var t = (T)typeof(T).CreateInstance();
			serializer.Populate(reader, t);
			return t;
		}

		/// <summary>
		/// Read the json as an instance of <para name="objectType"></para>
		/// </summary>
		public static object Read(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var t = objectType.CreateInstance();
			serializer.Populate(reader, t);
			return t;
		}
	}
}
