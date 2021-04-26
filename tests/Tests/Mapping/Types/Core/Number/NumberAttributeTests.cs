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
using Nest;

namespace Tests.Mapping.Types.Core.Number
{
	public class NumberTest
	{
		public byte Byte { get; set; }

		public decimal Decimal { get; set; }

		public double Double { get; set; }

		public float Float { get; set; }

		[Number(
			DocValues = true,
			Similarity = "classic",
			Store = true,
			Index = false,
			Boost = 1.5,
			NullValue = 0.0,
			IgnoreMalformed = true,
			Coerce = true,
			ScalingFactor = 10)]
		public double Full { get; set; }

		public int Integer { get; set; }

		public long Long { get; set; }

		[Number]
		public double Minimal { get; set; }

		public short Short { get; set; }

		public sbyte SignedByte { get; set; }

		public TimeSpan TimeSpan { get; set; }

		public uint UnsignedInteger { get; set; }

		public ulong UnsignedLong { get; set; }

		public ushort UnsignedShort { get; set; }
	}

	public class NumberAttributeTests
		: AttributeTestsBase<NumberTest>
	{
		protected override object ExpectJson => new
		{
			properties = new
			{
				full = new
				{
					type = "float",
					doc_values = true,
					similarity = "classic",
					store = true,
					index = false,
					boost = 1.5,
					null_value = 0.0,
					ignore_malformed = true,
					coerce = true,
					scaling_factor = 10.0
				},
				minimal = new
				{
					type = "float"
				},
				@byte = new
				{
					type = "short"
				},
				@short = new
				{
					type = "short"
				},
				integer = new
				{
					type = "integer"
				},
				@long = new
				{
					type = "long"
				},
				signedByte = new
				{
					type = "byte"
				},
				unsignedShort = new
				{
					type = "integer"
				},
				unsignedInteger = new
				{
					type = "long"
				},
				unsignedLong = new
				{
					type = "double"
				},
				@float = new
				{
					type = "float"
				},
				@double = new
				{
					type = "double"
				},
				@decimal = new
				{
					type = "double"
				},
				timeSpan = new
				{
					type = "long"
				}
			}
		};
	}
}
