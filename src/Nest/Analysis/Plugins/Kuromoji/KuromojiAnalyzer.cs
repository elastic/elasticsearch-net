// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// An analyzer tailored for japanese that is bootstrapped with defaults.
	/// Part of the `analysis-kuromoji` plugin: https://www.elastic.co/guide/en/elasticsearch/plugins/current/analysis-kuromoji.html
	/// </summary>
	public interface IKuromojiAnalyzer : IAnalyzer
	{
		[DataMember(Name ="mode")]
		KuromojiTokenizationMode? Mode { get; set; }

		[DataMember(Name ="user_dictionary")]
		string UserDictionary { get; set; }
	}

	/// <inheritdoc />
	public class KuromojiAnalyzer : AnalyzerBase, IKuromojiAnalyzer
	{
		public KuromojiAnalyzer() : base("kuromoji") { }

		public KuromojiTokenizationMode? Mode { get; set; }

		public string UserDictionary { get; set; }
	}

	/// <inheritdoc />
	public class KuromojiAnalyzerDescriptor : AnalyzerDescriptorBase<KuromojiAnalyzerDescriptor, IKuromojiAnalyzer>, IKuromojiAnalyzer
	{
		protected override string Type => "kuromoji";

		KuromojiTokenizationMode? IKuromojiAnalyzer.Mode { get; set; }
		string IKuromojiAnalyzer.UserDictionary { get; set; }

		public KuromojiAnalyzerDescriptor Mode(KuromojiTokenizationMode? mode) => Assign(mode, (a, v) => a.Mode = v);

		public KuromojiAnalyzerDescriptor UserDictionary(string userDictionary) => Assign(userDictionary, (a, v) => a.UserDictionary = v);
	}
}
