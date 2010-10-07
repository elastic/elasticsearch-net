using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElasticSearch.DSL
{
	public class Field
	{
		public string Name { get; private set; }
		public string Value { get; private set; }
		public double? Boost { get; private set; }
		
		public Field(string name, double boost) : this(name, boost, null) {}
		public Field(string name, string value) : this(name, null, value) {}
		public Field (string name, double? boost, string value)
		{
			this.Value = value;
			this.Name = name;
			this.Boost = boost;
		}
	}
}
