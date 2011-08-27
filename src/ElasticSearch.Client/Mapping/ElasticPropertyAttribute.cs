using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElasticSearch.Client.Mapping
{
	[AttributeUsage(System.AttributeTargets.Property, AllowMultiple = false)]
	public class ElasticPropertyAttribute : Attribute
	{
		public string Name { get; set; }
	}

	
}
