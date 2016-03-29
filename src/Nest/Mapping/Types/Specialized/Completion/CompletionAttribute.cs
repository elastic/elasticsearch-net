using System.Collections.Generic;

namespace Nest
{
	public class CompletionAttribute : ElasticsearchPropertyAttributeBase, ICompletionProperty
	{
		ICompletionProperty Self => this;

		string ICompletionProperty.SearchAnalyzer { get; set; }
		string ICompletionProperty.Analyzer { get; set; }
		bool? ICompletionProperty.PreserveSeparators { get; set; }
		bool? ICompletionProperty.PreservePositionIncrements { get; set; }
		int? ICompletionProperty.MaxInputLength { get; set; }
		IList<ISuggestContext> ICompletionProperty.Contexts { get; set; }

		public string SearchAnalyzer { get { return Self.SearchAnalyzer; } set { Self.SearchAnalyzer = value; } }
		public string Analyzer { get { return Self.Analyzer; } set { Self.Analyzer = value; } }
		public bool PreserveSeparators { get { return Self.PreserveSeparators.GetValueOrDefault(); } set { Self.PreserveSeparators = value; } }
		public bool PreservePositionIncrements { get { return Self.PreservePositionIncrements.GetValueOrDefault(); } set { Self.PreservePositionIncrements = value; } }
		public int MaxInputLength { get { return Self.MaxInputLength.GetValueOrDefault(); } set { Self.MaxInputLength = value; } }

		public CompletionAttribute() : base("completion") { }
	}
}
