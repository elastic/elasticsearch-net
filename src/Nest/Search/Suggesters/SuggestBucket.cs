// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(SuggestBucket))]
	public interface ISuggestBucket
	{
		[DataMember(Name = "completion")]
		ICompletionSuggester Completion { get; set; }

		[DataMember(Name = "phrase")]
		IPhraseSuggester Phrase { get; set; }

		[DataMember(Name = "prefix")]
		string Prefix { get; set; }

		[DataMember(Name = "regex")]
		string Regex { get; set; }

		[DataMember(Name = "term")]
		ITermSuggester Term { get; set; }

		[DataMember(Name = "text")]
		string Text { get; set; }
	}

	public class SuggestBucket : ISuggestBucket
	{
		[DataMember(Name = "completion")]
		public ICompletionSuggester Completion { get; set; }

		[DataMember(Name = "phrase")]
		public IPhraseSuggester Phrase { get; set; }

		[DataMember(Name = "prefix")]
		public string Prefix { get; set; }

		[DataMember(Name = "regex")]
		public string Regex { get; set; }

		[DataMember(Name = "term")]
		public ITermSuggester Term { get; set; }

		[DataMember(Name = "text")]
		public string Text { get; set; }
	}
}
