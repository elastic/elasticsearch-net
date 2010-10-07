using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElasticSearch.Client
{
	public class ElasticQuery
	{
		public int From { get; private set; }
		public int Size { get; private set; }
		public string QueryString { get; private set; }
	}
}
