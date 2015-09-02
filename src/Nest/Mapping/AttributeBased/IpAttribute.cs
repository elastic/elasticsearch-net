using System;

namespace Nest
{
	public class IpAttribute : ElasticsearchPropertyAttribute
	{
		public double Boost { get; set; }
		public bool IncludeInAll { get; set; }
		public NonStringIndexOption Index { get; set; }
		public string NullValue { get; set; }
		public int PrecisionStep { get; set; }

		public override IProperty ToProperty() => new IpProperty(this);
	}	
}
