using System;

namespace Nest
{
	public class CompletionAttribute : ElasticsearchPropertyAttribute 
	{
		public string SearchAnalyzer { get; set; }
		public string Analyzer { get; set; }
		public bool Payloads { get; set; }
		public bool PreserveSeparators { get; set; }
		public bool PreservePositionIncrements { get; set; }
		public int MaxInputLength { get; set; }

		public override IElasticsearchProperty ToProperty() => new CompletionProperty(this);
	}	
}
