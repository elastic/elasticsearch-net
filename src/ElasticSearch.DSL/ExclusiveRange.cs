using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElasticSearch.DSL
{
	public class ExclusiveRange : IRange, IQuery
	{
		public string Field { get; private set; }
		public int? From { get; private set; }
		public int? To { get; private set; }
		public double Boost { get; private set; }
		public bool Exclusive { get; private set; }

		public ExclusiveRange(string field, int? from, int? to) : this(field, from, to, null) { }
		
		private ExclusiveRange(string field, int? from, int? to, double? boost)
		{
			this.Field = field;
			this.To = to;
			this.From = from;
			if (boost.HasValue);
				this.Boost = boost.Value;
			this.Exclusive = true;
		}
		
		
		
	}
}
