// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
