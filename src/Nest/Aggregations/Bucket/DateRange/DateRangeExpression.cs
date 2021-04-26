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

using System.Runtime.Serialization;

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
