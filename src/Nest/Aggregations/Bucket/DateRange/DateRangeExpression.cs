// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;

namespace Nest
{
	[ReadAs(typeof(DateRangeExpression))]
	public interface IDateRangeExpression
	{
		[DataMember(Name ="from")]
		DateMath From { get; set; }

		[DataMember(Name ="key")]
		string Key { get; set; }

		[DataMember(Name ="to")]
		DateMath To { get; set; }
	}

	public class DateRangeExpression : IDateRangeExpression
	{
		public DateMath From { get; set; }

		public string Key { get; set; }

		public DateMath To { get; set; }
	}

	public class DateRangeExpressionDescriptor
		: DescriptorBase<DateRangeExpressionDescriptor, IDateRangeExpression>, IDateRangeExpression
	{
		DateMath IDateRangeExpression.From { get; set; }

		string IDateRangeExpression.Key { get; set; }

		DateMath IDateRangeExpression.To { get; set; }

		public DateRangeExpressionDescriptor From(DateMath from) => Assign(from, (a, v) => a.From = v);

		public DateRangeExpressionDescriptor To(DateMath to) => Assign(to, (a, v) => a.To = v);

		public DateRangeExpressionDescriptor Key(string key) => Assign(key, (a, v) => a.Key = v);
	}
}
