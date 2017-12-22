using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum NumberType
	{
		[EnumMember(Value = "float")]
		Float,
		[EnumMember(Value = "half_float")]
		HalfFloat,
		[EnumMember(Value = "scaled_float")]
		ScaledFloat,
		[EnumMember(Value = "double")]
		Double,
		[EnumMember(Value = "integer")]
		Integer,
		[EnumMember(Value = "long")]
		Long,
		[EnumMember(Value = "short")]
		Short,
		[EnumMember(Value = "byte")]
		Byte
	}

	internal static class NumberTypeExtensions
	{
		public static FieldType ToFieldType(this NumberType numberType)
		{
			switch (numberType)
			{
				case NumberType.Float: return FieldType.Float;
				case NumberType.HalfFloat: return FieldType.HalfFloat;
				case NumberType.ScaledFloat: return FieldType.ScaledFloat;
				case NumberType.Double: return FieldType.Double;
				case NumberType.Integer: return FieldType.Integer;
				case NumberType.Long: return FieldType.Long;
				case NumberType.Short: return FieldType.Short;
				case NumberType.Byte: return FieldType.Byte;
				default:
					throw new ArgumentOutOfRangeException(nameof(numberType), numberType, null);
			}

		}
	}
}
