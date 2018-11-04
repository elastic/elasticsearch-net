using System;
using Newtonsoft.Json;

namespace Nest
{
	public class ContractJsonConverterAttribute : Attribute
	{
		public ContractJsonConverterAttribute(Type jsonConverter)
		{
			if (typeof(JsonConverter).IsAssignableFrom(jsonConverter)) Converter = jsonConverter.CreateInstance<JsonConverter>();
		}

		public JsonConverter Converter { get; }
	}

	public class ExactContractJsonConverterAttribute : Attribute
	{
		public ExactContractJsonConverterAttribute(Type jsonConverter)
		{
			if (typeof(JsonConverter).IsAssignableFrom(jsonConverter)) Converter = jsonConverter.CreateInstance<JsonConverter>();
		}

		public JsonConverter Converter { get; }
	}
}
