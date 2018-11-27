using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// The nori analyzer consists of the following tokenizer and token filters:
	/// <para> - nori_tokenizer</para>
	/// <para> - nori_part_of_speech token filter</para>
	/// <para> - nori_readingform token filter</para>
	/// <para> - lowercase token filter</para>
	/// </summary>
	public interface INoriAnalyzer : IAnalyzer
	{
		/// <inheritdoc cref="INoriTokenizer.DecompoundMode" />
		[DataMember(Name ="decompound_mode")]
		NoriDecompoundMode? DecompoundMode { get; set; }

		/// <inheritdoc cref="INoriPartOfSpeechTokenFilter.StopTags" />
		[DataMember(Name ="stoptags")]
		IEnumerable<string> StopTags { get; set; }

		/// <inheritdoc cref="INoriTokenizer.UserDictionary" />
		[DataMember(Name ="user_dictionary")]
		string UserDictionary { get; set; }
	}

	/// <inheritdoc cref="INoriAnalyzer" />
	public class NoriAnalyzer : AnalyzerBase, INoriAnalyzer
	{
		public NoriAnalyzer() : base("nori") { }

		/// <inheritdoc cref="INoriTokenizer.DecompoundMode" />
		public NoriDecompoundMode? DecompoundMode { get; set; }

		///<inheritdoc cref="INoriPartOfSpeechTokenFilter.StopTags" />
		public IEnumerable<string> StopTags { get; set; }

		/// <inheritdoc cref="INoriTokenizer.UserDictionary" />
		public string UserDictionary { get; set; }
	}

	/// <inheritdoc cref="INoriAnalyzer" />
	public class NoriAnalyzerDescriptor : AnalyzerDescriptorBase<NoriAnalyzerDescriptor, INoriAnalyzer>, INoriAnalyzer
	{
		protected override string Type => "nori";

		NoriDecompoundMode? INoriAnalyzer.DecompoundMode { get; set; }
		IEnumerable<string> INoriAnalyzer.StopTags { get; set; }
		string INoriAnalyzer.UserDictionary { get; set; }

		/// <inheritdoc cref="INoriTokenizer.DecompoundMode" />
		public NoriAnalyzerDescriptor DecompoundMode(NoriDecompoundMode? mode) => Assign(a => a.DecompoundMode = mode);

		/// <inheritdoc cref="INoriTokenizer.UserDictionary" />
		public NoriAnalyzerDescriptor UserDictionary(string path) => Assign(a => a.UserDictionary = path);

		///<inheritdoc cref="INoriPartOfSpeechTokenFilter.StopTags" />
		public NoriAnalyzerDescriptor StopTags(IEnumerable<string> stopTags) => Assign(a => a.StopTags = stopTags);

		///<inheritdoc cref="INoriPartOfSpeechTokenFilter.StopTags" />
		public NoriAnalyzerDescriptor StopTags(params string[] stopTags) => Assign(a => a.StopTags = stopTags);
	}
}
