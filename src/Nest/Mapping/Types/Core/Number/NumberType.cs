/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;
using System.Runtime.Serialization;
using Elasticsearch.Net;


namespace Nest
{
	[StringEnum]
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
		Byte,

		[EnumMember(Value = "unsigned_long")]
		UnsignedLong
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
				case NumberType.UnsignedLong: return FieldType.UnsignedLong;
				default:
					throw new ArgumentOutOfRangeException(nameof(numberType), numberType, null);
			}
		}
	}
}
