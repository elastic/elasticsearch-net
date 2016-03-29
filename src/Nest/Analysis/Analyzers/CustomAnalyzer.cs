using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// An analyzer of type custom that allows to combine a Tokenizer with zero or more Token Filters, 
	/// and zero or more Char Filters. 
	/// <para>The custom analyzer accepts a logical/registered name of the tokenizer to use, and a list of 
	/// logical/registered names of token filters.</para>
	/// </summary>
	public interface ICustomAnalyzer : IAnalyzer
	{
		/// <summary>
		/// The logical / registered name of the tokenizer to use.
		/// </summary>
		[JsonProperty("char_filter")]
		IEnumerable<string> CharFilter { get; set; }

		/// <summary>
		/// An optional list of logical / registered name of token filters.
		/// </summary>
		[JsonProperty("filter")]
		IEnumerable<string> Filter { get; set; }

		/// <summary>
		/// An optional list of logical / registered name of char filters.
		/// </summary>
		[JsonProperty("tokenizer")]
		string Tokenizer { get; set; }

		/// <summary>
		/// An optional number of positions to increment between each field value of a 
		/// field using this analyzer.
		/// </summary>
		[JsonProperty("position_offset_gap")]
		int? PositionOffsetGap { get; set; }
	}

	public class CustomAnalyzer : AnalyzerBase, ICustomAnalyzer
	{
		public CustomAnalyzer()  { Type = "custom"; }

		/// <inheritdoc/>
		public string Tokenizer { get; set; }

		/// <inheritdoc/>
		public IEnumerable<string> Filter { get; set; }

		/// <inheritdoc/>
		public IEnumerable<string> CharFilter { get; set; }

		/// <inheritdoc/>
		public int? PositionOffsetGap { get; set; }
	}

	public class CustomAnalyzerDescriptor 
		: AnalyzerDescriptorBase<CustomAnalyzerDescriptor, ICustomAnalyzer>, ICustomAnalyzer
	{
		protected override string Type => "custom";

		IEnumerable<string> ICustomAnalyzer.CharFilter { get; set; }
		IEnumerable<string> ICustomAnalyzer.Filter { get; set; }
		string ICustomAnalyzer.Tokenizer { get; set; }
		int? ICustomAnalyzer.PositionOffsetGap { get; set; }
		
		/// <inheritdoc/>
		public CustomAnalyzerDescriptor Filters(params string[] filters) => Assign(a => a.Filter = filters);

		/// <inheritdoc/>
		public CustomAnalyzerDescriptor Filters(IEnumerable<string> filters) => Assign(a => a.Filter = filters);

		/// <inheritdoc/>
		public CustomAnalyzerDescriptor CharFilters(params string[] charFilters) => Assign(a => a.CharFilter = charFilters);

		/// <inheritdoc/>
		public CustomAnalyzerDescriptor CharFilters(IEnumerable<string> charFilters) => Assign(a => a.CharFilter = charFilters);

		/// <inheritdoc/>
		public CustomAnalyzerDescriptor Tokenizer(string tokenizer) => Assign(a => a.Tokenizer = tokenizer);

		/// <inheritdoc/>
		public CustomAnalyzerDescriptor PositionOffsetGap(int? positionOffsetGap) => 
			Assign(a => a.PositionOffsetGap = positionOffsetGap);

	}
}