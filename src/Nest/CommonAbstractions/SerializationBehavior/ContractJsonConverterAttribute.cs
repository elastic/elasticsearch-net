using System;
using Newtonsoft.Json;

namespace Nest
{

	public class ContractJsonConverterAttribute : Attribute
	{
		public JsonConverter Converter { get; }

		public ContractJsonConverterAttribute(Type jsonConverter)
		{
			if (typeof(JsonConverter).IsAssignableFrom(jsonConverter))
			{
				Converter = jsonConverter.CreateInstance<JsonConverter>();
			}
		}
	}
	public class ExactContractJsonConverterAttribute : Attribute
	{
		public JsonConverter Converter { get; }

		public ExactContractJsonConverterAttribute(Type jsonConverter)
		{
			if (typeof(JsonConverter).IsAssignableFrom(jsonConverter))
			{
				Converter = jsonConverter.CreateInstance<JsonConverter>();
			}
		}
	}
}