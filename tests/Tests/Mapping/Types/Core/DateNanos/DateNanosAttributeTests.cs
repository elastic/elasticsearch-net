// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using Nest;

namespace Tests.Mapping.Types.Core.DateNanos
{
	public class DateNanosTest
	{
		[DateNanos(
			DocValues = true,
			Similarity = "classic",
			Store = true,
			Index = false,
			Boost = 1.2,
			IgnoreMalformed = true,
			Format = "yyyy-MM-dd'T'HH:mm[:ss][.S]")]
		public DateTime Full { get; set; }

		[DateNanos]
		public DateTime Minimal { get; set; }
	}

	public class DateNanosAttributeTests : AttributeTestsBase<DateNanosTest>
	{
		protected override object ExpectJson => new
		{
			properties = new
			{
				full = new
				{
					type = "date_nanos",
					doc_values = true,
					similarity = "classic",
					store = true,
					index = false,
					boost = 1.2,
					ignore_malformed = true,
					format = "yyyy-MM-dd'T'HH:mm[:ss][.S]"
				},
				minimal = new
				{
					type = "date_nanos"
				}
			}
		};
	}
}
