using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// An analyzer tailored for japanese that is bootstrapped with defaults.
	/// Part of the `analysis-kuromoji` plugin: https://www.elastic.co/guide/en/elasticsearch/plugins/current/analysis-kuromoji.html
	/// </summary>
	public interface IKuromojiAnalyzer : IAnalyzer
	{
		[JsonProperty("mode")]
		KuromojiTokenizationMode? Mode { get; set; }

		[JsonProperty("user_dictionary")]
		string UserDictionary { get; set; }
	}

	/// <inheritdoc/>
	public class KuromojiAnalyzer : AnalyzerBase, IKuromojiAnalyzer
	{
		public KuromojiAnalyzer() : base("kuromoji") {}

		public KuromojiTokenizationMode? Mode { get; set; }

		public string UserDictionary { get; set; }
	}

	/// <inheritdoc/>
	public class KuromojiAnalyzerDescriptor :
		AnalyzerDescriptorBase<KuromojiAnalyzerDescriptor, IKuromojiAnalyzer>, IKuromojiAnalyzer
	{
		protected override string Type => "kuromoji";

		KuromojiTokenizationMode? IKuromojiAnalyzer.Mode { get; set; }
		string IKuromojiAnalyzer.UserDictionary { get; set; }

		public KuromojiAnalyzerDescriptor Mode(KuromojiTokenizationMode? mode) => Assign(a => a.Mode = mode);

		public KuromojiAnalyzerDescriptor UserDictionary(string userDictionary) => Assign(a => a.UserDictionary = userDictionary);

	}
}
