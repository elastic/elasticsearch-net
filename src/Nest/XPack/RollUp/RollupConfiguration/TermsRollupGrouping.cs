using System;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary> The histogram group aggregates one or more numeric fields into numeric histogram intervals. </summary>
	[JsonConverter(typeof(ReadAsTypeJsonConverter<TermsRollupGrouping>))]
	public interface ITermsRollupGrouping
	{
		/// <summary>
		/// The set of fields that you wish to collect terms for. This array can contain fields that are both keyword and numerics.
		/// Order does not matter
		/// </summary>
		[JsonProperty("fields")]
		Fields Fields { get; set; }
	}

	/// <inheritdoc cref="ITermsRollupGrouping"/>
	public class TermsRollupGrouping : ITermsRollupGrouping
	{
		/// <inheritdoc cref="ITermsRollupGrouping.Fields"/>
		public Fields Fields { get; set; }
	}

	/// <inheritdoc cref="ITermsRollupGrouping"/>
	public class TermsRollupGroupingDescriptor<T>
		: DescriptorBase<TermsRollupGroupingDescriptor<T>, ITermsRollupGrouping>, ITermsRollupGrouping
		where T : class
	{
		Fields ITermsRollupGrouping.Fields { get; set; }

		/// <inheritdoc cref="ITermsRollupGrouping.Fields"/>
		public TermsRollupGroupingDescriptor<T> Fields(Func<FieldsDescriptor<T>, IPromise<Fields>> fields) =>
			Assign(a => a.Fields = fields?.Invoke(new FieldsDescriptor<T>())?.Value);

		/// <inheritdoc cref="ITermsRollupGrouping.Fields"/>
		public TermsRollupGroupingDescriptor<T> Fields(Fields fields) => Assign(a => a.Fields = fields);
	}
}
