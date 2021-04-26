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

namespace Nest
{
	/// <summary>
	/// The terms group can be used on keyword or numeric fields, to allow bucketing via the terms aggregation at a
	/// later point. The terms group is optional. If defined, the indexer will enumerate and store all values of a field for
	/// each time-period.
	/// </summary>
	[ReadAs(typeof(TermsRollupGrouping))]
	public interface ITermsRollupGrouping
	{
		/// <summary>
		/// The set of fields that you wish to collect terms for. This array can contain fields that are both keyword and numerics.
		/// Order does not matter
		/// </summary>
		[DataMember(Name ="fields")]
		Fields Fields { get; set; }
	}

	/// <inheritdoc />
	public class TermsRollupGrouping : ITermsRollupGrouping
	{
		/// <inheritdoc />
		public Fields Fields { get; set; }
	}

	/// <inheritdoc cref="ITermsRollupGrouping" />
	public class TermsRollupGroupingDescriptor<T>
		: DescriptorBase<TermsRollupGroupingDescriptor<T>, ITermsRollupGrouping>, ITermsRollupGrouping
		where T : class
	{
		Fields ITermsRollupGrouping.Fields { get; set; }

		/// <inheritdoc cref="ITermsRollupGrouping.Fields" />
		public TermsRollupGroupingDescriptor<T> Fields(Func<FieldsDescriptor<T>, IPromise<Fields>> fields) =>
			Assign(fields, (a, v) => a.Fields = v?.Invoke(new FieldsDescriptor<T>())?.Value);

		/// <inheritdoc cref="ITermsRollupGrouping.Fields" />
		public TermsRollupGroupingDescriptor<T> Fields(Fields fields) => Assign(fields, (a, v) => a.Fields = v);
	}
}
