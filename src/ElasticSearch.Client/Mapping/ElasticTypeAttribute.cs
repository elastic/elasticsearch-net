using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElasticSearch.Client.Mapping
{
	[AttributeUsage(System.AttributeTargets.Class, AllowMultiple = false)]
	public class ElasticTypeAttribute : Attribute
	{
		public string Name { get; set; }
	}
}
