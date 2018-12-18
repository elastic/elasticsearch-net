using System;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// The predicate_token_filter token filter takes a predicate script, and removes tokens that do
	/// not match the predicate.
	/// </summary>
	public interface IPredicateTokenFilter : ITokenFilter
	{
		/// <summary>
		/// a predicate script that determines whether or not the current token will
		/// be emitted.  Note that only inline scripts are supported.
		/// </summary>
		[JsonProperty("script")]
		IScript Script { get; set; }
	}

	public class PredicateTokenFilter : TokenFilterBase, IPredicateTokenFilter
	{
		public PredicateTokenFilter() : base("predicate_token_filter") { }

		public IScript Script { get; set; }
	}

	/// <inheritdoc cref="IPredicateTokenFilter" />
	public class PredicateTokenFilterDescriptor
		: TokenFilterDescriptorBase<PredicateTokenFilterDescriptor, IPredicateTokenFilter>, IPredicateTokenFilter
	{
		protected override string Type => "predicate_token_filter";

		IScript IPredicateTokenFilter.Script { get; set; }

		/// <inheritdoc cref="IPredicateTokenFilter.Script" />
		public PredicateTokenFilterDescriptor Script(Func<ScriptDescriptor, IScript> scriptSelector) =>
			Assign(a => a.Script = scriptSelector?.Invoke(new ScriptDescriptor()));

		/// <inheritdoc cref="IPredicateTokenFilter.Script" />
		public PredicateTokenFilterDescriptor Script(string predicate) =>
			Assign(a => a.Script = new InlineScript(predicate));
	}
}
