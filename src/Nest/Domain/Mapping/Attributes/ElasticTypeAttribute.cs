using Nest.Resolvers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[AttributeUsage(System.AttributeTargets.Class, AllowMultiple = false)]
	public class ElasticTypeAttribute : Attribute, IElasticType
	{
		public string Name { get; set; }
		public TypeNameMarker TypeNameMarker { get; set; }
		public TypeNameMarker Type { get; set; }
		public string IndexAnalyzer { get; set; }
		public string SearchAnalyzer { get; set; }
		public string[] DynamicDateFormats { get; set; }
		public bool DateDetection { get; set; }
		public bool NumericDetection { get; set; }
		public string ParentType { get; set; }
		public string IdProperty { get; set; }
		public bool DisableAllField { get; set; }
		public ElasticTypeAttribute()
		{
			this.DateDetection = true;
			if (!this.Name.IsNullOrEmpty() && this.TypeNameMarker == null)
			{
				this.TypeNameMarker = this.Name;
			}
		}
	}

}