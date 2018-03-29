using System;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// A similarity that allows a script to be used in order to specify how scores should be computed.
	/// </summary>
	/// <remarks>
	/// Valid in Elasticsearch 6.1.0+
	/// </remarks>
	public interface IScriptedSimilarity : ISimilarity
	{
		/// <summary>
		/// Script to calculate similarity
		/// </summary>
		[JsonProperty("script")]
		IScript Script { get; set; }
	}

	/// <inheritdoc />
	public class ScriptedSimilarity : IScriptedSimilarity
	{
		public string Type => "scripted";

		/// <inheritdoc />
		public IScript Script { get; set; }
	}

	/// <inheritdoc cref="IScriptedSimilarity" />
	public class ScriptedSimilarityDescriptor
		: DescriptorBase<ScriptedSimilarityDescriptor, IScriptedSimilarity>, IScriptedSimilarity
	{
		string ISimilarity.Type => "scripted";
		IScript IScriptedSimilarity.Script { get; set; }

		/// <inheritdoc cref="IScriptedSimilarity.Script" />
		public ScriptedSimilarityDescriptor Script(Func<ScriptDescriptor, IScript> selector) =>
			Assign(a => a.Script = selector?.Invoke(new ScriptDescriptor()));
	}
}
