using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IAnalyzeResponse : IResponse
	{
		/// <summary>
		/// When <see cref="IAnalyzeRequest.Explain "/> is not true this will hold the analyzed tokens.
		/// </summary>
		IReadOnlyCollection<AnalyzeToken> Tokens { get; }

		/// <summary>
		/// When <see cref="IAnalyzeRequest.Explain "/> is to true this will hold the detailed view of the analyzed tokens.
		/// </summary>
		AnalyzeDetail Detail { get; }
	}

	[JsonObject]
	public class AnalyzeResponse : ResponseBase, IAnalyzeResponse
	{
		[JsonProperty("tokens")]
		public IReadOnlyCollection<AnalyzeToken> Tokens { get; internal set; } = EmptyReadOnly<AnalyzeToken>.Collection;

		[JsonProperty("detail")]
		public AnalyzeDetail Detail { get; internal set; }
	}


	[JsonObject]
	public class AnalyzeDetail
	{
		[JsonProperty("custom_analyzer")]
		public bool CustomAnalyzer { get; internal set; }

		[JsonProperty("charfilters")]
		public IReadOnlyCollection<CharFilterDetail> CharFilters { get; internal set; } = EmptyReadOnly<CharFilterDetail>.Collection;

		[JsonProperty("tokenfilters")]
		public IReadOnlyCollection<TokenDetail> Filters { get; internal set; } = EmptyReadOnly<TokenDetail>.Collection;

		[JsonProperty("tokenizer")]
		public TokenDetail Tokenizer { get; internal set; }
	}

	[JsonObject]
	public class CharFilterDetail
	{
		[JsonProperty("name")]
		public string Name { get; internal set; }

		[JsonProperty("filtered_text")]
		public IReadOnlyCollection<string> FilteredText { get; internal set; } = EmptyReadOnly<string>.Collection;

	}

	[JsonObject]
	public class TokenDetail
	{
		[JsonProperty("name")]
		public string Name { get; internal set; }

		[JsonProperty("tokens")]
		public IReadOnlyCollection<ExplainAnalyzeToken> Tokens { get; internal set; } = EmptyReadOnly<ExplainAnalyzeToken>.Collection;
	}

	//TODO create an issue on the main repos that this API uses camelCase
	//this causes us to be unable to subclass from AnalyzeToken directly
	[JsonObject]
	public class ExplainAnalyzeToken
	{
		[JsonProperty("token")]
		public string Token { get; internal set; }

		[JsonProperty("type")]
		public string Type { get; internal set; }

		[JsonProperty("start_offset")]
		public long StartOffset { get; internal set; }

		[JsonProperty("end_offset")]
		public long EndOffset { get; internal set; }

		[JsonProperty("position")]
		public long Position { get; internal set; }

		[JsonProperty("positionLength")]
		public long? PositionLength { get; internal set; }

		[JsonProperty("termFrequency")]
		public long? TermFrequency { get; internal set; }

		[JsonProperty("keyword")]
		public bool? Keyword { get; internal set; }

		[JsonProperty("bytes")]
		public string Bytes { get; internal set; }
	}
}
