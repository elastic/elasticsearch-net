using System;

namespace Nest
{
	public class BooleanAttribute : ElasticsearchPropertyAttribute 
	{
		public NonStringIndexOption? Index { get; set; }
		public double Boost { get; set; }
		public bool NullValue { get; set; }

		public override IProperty ToProperty() => new BooleanProperty(this);
	}	
}
