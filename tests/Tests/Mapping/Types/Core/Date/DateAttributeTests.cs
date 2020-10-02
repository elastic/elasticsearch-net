// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Nest;

namespace Tests.Mapping.Types.Core.Date
{
	public class DateTest
	{
		[Date(
			DocValues = true,
			Store = true,
			Index = false,
			IgnoreMalformed = true,
			Format = "yyyy-MM-dd'T'HH:mm[:ss][.S]")]
		public DateTime Full { get; set; }

		public DateTime Inferred { get; set; }

		public DateTimeOffset InferredOffset { get; set; }

		[Date]
		public DateTime Minimal { get; set; }
	}

	public class DateAttributeTests : AttributeTestsBase<DateTest>
	{
		protected override object ExpectJson => new
		{
			properties = new
			{
				full = new
				{
					type = "date",
					doc_values = true,
					store = true,
					index = false,
					ignore_malformed = true,
					format = "yyyy-MM-dd'T'HH:mm[:ss][.S]"
				},
				minimal = new
				{
					type = "date"
				},
				inferred = new
				{
					type = "date"
				},
				inferredOffset = new
				{
					type = "date"
				}
			}
		};
	}
}
