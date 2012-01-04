using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest.DSL
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
	}
}
