using System;

namespace Nest
{
	[AttributeUsage(System.AttributeTargets.Class, AllowMultiple = false)]
	public class ElasticTypeAttribute : Attribute 
	{
		public string Name { get; set; }
		public string IdProperty { get; set; }
	}

}