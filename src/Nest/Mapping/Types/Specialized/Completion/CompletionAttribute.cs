using System.Collections.Generic;

namespace Nest
{
	public class CompletionAttribute : ElasticsearchDocValuesPropertyAttributeBase, ICompletionProperty
	{
		private ICompletionProperty Self => this;

		public CompletionAttribute() : base(FieldType.Completion) { }

		string ICompletionProperty.SearchAnalyzer { get; set; }
		string ICompletionProperty.Analyzer { get; set; }
		bool? ICompletionProperty.PreserveSeparators { get; set; }
		bool? ICompletionProperty.PreservePositionIncrements { get; set; }
		int? ICompletionProperty.MaxInputLength { get; set; }
		IList<ISuggestContext> ICompletionProperty.Contexts { get; set; }

		public string SearchAnalyzer { get => Self.SearchAnalyzer; set => Self.SearchAnalyzer = value; }
		public string Analyzer { get => Self.Analyzer; set => Self.Analyzer = value; }
		public bool PreserveSeparators { get => Self.PreserveSeparators.GetValueOrDefault(); set => Self.PreserveSeparators = value; }
		public bool PreservePositionIncrements { get => Self.PreservePositionIncrements.GetValueOrDefault(); set => Self.PreservePositionIncrements = value; }
		public int MaxInputLength { get => Self.MaxInputLength.GetValueOrDefault(); set => Self.MaxInputLength = value; }

	}
}
