using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// A token filter of type keep that only keeps tokens with text contained in a predefined set of words.
	/// </summary>
	public interface IKeepTypesTokenFilter : ITokenFilter
	{
		/// <summary>
		/// A list of types to keep.
		/// </summary>
		[JsonProperty("types")]
		IEnumerable<string> Types { get; set; }
	}
	/// <inheritdoc/>
	public class KeepTypesTokenFilter : TokenFilterBase
	{
		public KeepTypesTokenFilter() : base("keep_types") { }

		/// <inheritdoc/>
		public IEnumerable<string> Types { get; set; }

	}
	///<inheritdoc/>
	public class KeepTypesTokenFilterDescriptor 
		: TokenFilterDescriptorBase<KeepTypesTokenFilterDescriptor, IKeepTypesTokenFilter>, IKeepTypesTokenFilter
	{
		protected override string Type => "keep_types";

		IEnumerable<string> IKeepTypesTokenFilter.Types { get; set; }

		///<inheritdoc/>
		public KeepTypesTokenFilterDescriptor Types(IEnumerable<string> types) => Assign(a => a.Types = types);

		///<inheritdoc/>
		public KeepTypesTokenFilterDescriptor Types(params string[] types) => Assign(a => a.Types = types);

	}

}