namespace Nest
{
	public class BoxplotAggregate : MetricAggregateBase
	{
		public double Min { get; set; }

		public double Max { get; set; }

		public double Q1 { get; set; }

		public double Q2 { get; set; }

		public double Q3 { get; set; }
	}
}
