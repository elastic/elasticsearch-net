using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElasticSearch.Client.DSL
{
	public class Wildcard : IQuery
	{
		public string Field { get; private set; }
		public string Value { get; private set; }
		public double Boost { get; private set; }

		public Wildcard(string field, string value, double boost)
		{
			this.Value = value;
			this.Field = field;
			this.Boost = boost;
		}
		public Wildcard(Field field)
		{
			field.ThrowIfNull("field");
		
			this.Field = field.Name;
			this.Value = field.Value;
			if (field.Boost.HasValue)
				this.Boost = field.Boost.Value;
		}


	}
}
