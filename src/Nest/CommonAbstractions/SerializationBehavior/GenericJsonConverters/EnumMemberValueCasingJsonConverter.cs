using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.Reflection;

namespace Nest
{
	/// <summary>
	/// A Json converter that can serialize enums to strings where the string values
	/// are specified using <see cref="EnumMemberAttribute.Value"/> and where values
	/// differ in casing.
	/// </summary>
	/// <remarks>
	/// See https://github.com/JamesNK/Newtonsoft.Json/issues/1067
	/// </remarks>
	/// <typeparam name="TEnum">the type of enum</typeparam>
	internal class EnumMemberValueCasingJsonConverter<TEnum> : JsonConverter where TEnum : struct
	{
		private static readonly Dictionary<TEnum, string> EnumToString;
		private static readonly Dictionary<string, TEnum> StringToEnum;

		static EnumMemberValueCasingJsonConverter()
		{
			var enumType = typeof(TEnum);

			if (!enumType.IsEnumType())
				throw new InvalidOperationException($"{nameof(TEnum)} must be an enum.");

			var enums = Enum.GetValues(enumType).Cast<TEnum>().ToList();
			EnumToString = new Dictionary<TEnum, string>(enums.Count);
			StringToEnum = new Dictionary<string, TEnum>(enums.Count, StringComparer.Ordinal);

			foreach (var e in enums)
			{
				var field = enumType.GetTypeInfo().GetDeclaredField(e.ToString());
				var enumMemberValue = field.GetCustomAttributes(typeof(EnumMemberAttribute), true)
					.Cast<EnumMemberAttribute>()
					.Select(a => a.Value)
					.SingleOrDefault() ?? field.Name;

				TEnum first;
				if (StringToEnum.TryGetValue(enumMemberValue, out first))
					throw new InvalidOperationException($"Enum name '{enumMemberValue}' already exists on enum '{enumType.Name}'.");

				var enumValue = (TEnum)Enum.Parse(enumType, field.Name);

				EnumToString.Add(enumValue, enumMemberValue);
				StringToEnum.Add(enumMemberValue, enumValue);
			}
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var enumValue = (TEnum)value;
			string stringValue;
			if (!EnumToString.TryGetValue(enumValue, out stringValue))
				throw new InvalidOperationException($"'{value}' is not a valid value for '{typeof(TEnum).Name}'");

			writer.WriteValue(stringValue);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var value = (string)reader.Value;

			if (value == null)
				return default(TEnum);

			TEnum enumValue;
			if (StringToEnum.TryGetValue(value, out enumValue))
				return enumValue;

			return Enum.TryParse(value, true, out enumValue) ? enumValue : default(TEnum);
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(TEnum);
		}
	}
}
