using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Newtonsoft.Json.Serialization;

namespace ElasticSearch.Client.DSL
{
	public class Term : IQuery
	{
		internal string Field { get; set; }
		internal string Value { get; set; }
		internal double? Boost { get; set; }
		
		public Term()
		{
		}
	}
}
