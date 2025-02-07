namespace Elastic.Clients.Elasticsearch;

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

internal static class EnumValueParser<T>
	where T : struct, Enum
{
	private static readonly Dictionary<string, T> ValueMap = new(StringComparer.OrdinalIgnoreCase);

	static EnumValueParser()
	{
		foreach (var value in Enum.GetValues<T>())
		{
			var name = value.ToString();
			ValueMap[name] = value;

			var field = typeof(T).GetField(name);
			var attribute = (EnumMemberAttribute?)Attribute.GetCustomAttribute(field!, typeof(EnumMemberAttribute));
			if (attribute?.Value is not null)
			{
				ValueMap[attribute.Value] = value;
			}
		}
	}

	public static bool TryParse(string input, out T result)
	{
		return ValueMap.TryGetValue(input, out result);
	}

	public static T Parse(string input)
	{
		if (ValueMap.TryGetValue(input, out var result))
		{
			return result;
		}

		throw new ArgumentException($"Unknown member '{input}' for enum '{typeof(T).Name}'.");
	}
}
