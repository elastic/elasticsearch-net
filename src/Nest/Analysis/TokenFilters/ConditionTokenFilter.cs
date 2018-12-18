using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// The conditional token filter takes a predicate script and a list of subfilters, and
	/// only applies the subfilters to the current token if it matches the predicate.
	/// </summary>
	public interface IConditionTokenFilter : ITokenFilter
	{
		/// <summary>
		/// a predicate script that determines whether or not the filters will be applied
		/// to the current token.  Note that only inline scripts are supported
		/// </summary>
		[JsonProperty("script")]
		IScript Script { get; set; }

		/// <summary>
		///a chain of token filters to apply to the current token if the predicate
		/// matches. These can be any token filters defined elsewhere in the index mappings.
		/// </summary>
		[JsonProperty("filter")]
		IEnumerable<string> Filters { get; set; }
	}

	/// <inheritdoc cref="IConditionTokenFilter" />
	public class ConditionTokenFilter : TokenFilterBase, IConditionTokenFilter
	{
		public ConditionTokenFilter() : base("condition") { }

		/// <inheritdoc cref="IConditionTokenFilter.Script" />
		public IScript Script { get; set; }

		/// <inheritdoc cref="IConditionTokenFilter.Filters" />
		public IEnumerable<string> Filters { get; set; }
	}

	/// <inheritdoc cref="IConditionTokenFilter" />
	public class ConditionTokenFilterDescriptor
		: TokenFilterDescriptorBase<ConditionTokenFilterDescriptor, IConditionTokenFilter>, IConditionTokenFilter
	{
		protected override string Type => "condition";

		IScript IConditionTokenFilter.Script { get; set; }
		IEnumerable<string> IConditionTokenFilter.Filters { get; set; }

		/// <inheritdoc cref="IConditionTokenFilter.Script" />
		public ConditionTokenFilterDescriptor Script(Func<ScriptDescriptor, IScript> scriptSelector) =>
			Assign(a => a.Script = scriptSelector?.Invoke(new ScriptDescriptor()));

		/// <inheritdoc cref="IConditionTokenFilter.Script" />
		public ConditionTokenFilterDescriptor Script(string predicate) =>
			Assign(a => a.Script = new InlineScript(predicate));

		/// <inheritdoc cref="IConditionTokenFilter.Filters" />
		public ConditionTokenFilterDescriptor Filters(params string[] filters) =>
			Assign(a => a.Filters = filters);

		/// <inheritdoc cref="IConditionTokenFilter.Filters" />
		public ConditionTokenFilterDescriptor Filters(IEnumerable<string> filters) =>
			Assign(a => a.Filters = filters);

}
}
