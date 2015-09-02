using Newtonsoft.Json;
using System;

namespace Nest
{
	public class StringAttribute : ElasticsearchPropertyAttribute
	{
		public string Analyzer { get; set; }
		public double Boost { get; set; }
		public int IgnoreAbove { get; set; }
		public bool IncludeInAll { get; set; }
		public FieldIndexOption Index { get; set; }
		public IndexOptions IndexOptions { get; set; }
		public string NullValue { get; set; }
		public int PositionOffsetGap { get; set; }
		public string SearchAnalyzer { get; set; }
		public TermVectorOption TermVector { get; set; }

		public override IProperty ToProperty() => new StringProperty(this);
	}
}
