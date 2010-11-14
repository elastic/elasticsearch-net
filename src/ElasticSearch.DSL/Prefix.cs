using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElasticSearch.DSL
{
	public class Prefix : IQuery
	{
		public string Field { get; private set; }
		public string Value { get; private set; }
		public double Boost { get; private set; }

		public Prefix(string field, string value, double boost)
		{
			this.Value = value;
			this.Field = field;
			this.Boost = boost;
		}
		public Prefix(Field field)
		{
			field.ThrowIfNull("field");
		
			this.Field = field.Name;
			this.Value = field.Value;
			this.Boost = (field.Boost.HasValue) ? field.Boost.Value : 1.0;
		}


	}
}
