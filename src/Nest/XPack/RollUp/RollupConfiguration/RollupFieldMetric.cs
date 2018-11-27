using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace Nest
{
	[ReadAs(typeof(RollupFieldMetric))]
	public interface IRollupFieldMetric
	{
		[DataMember(Name ="field")]
		Field Field { get; set; }

		[DataMember(Name ="metrics")]
		IEnumerable<RollupMetric> Metrics { get; set; }
	}

	public class RollupFieldMetric : IRollupFieldMetric
	{
		public Field Field { get; set; }
		public IEnumerable<RollupMetric> Metrics { get; set; }
	}

	public class RollupFieldMetricsDescriptor<T> : DescriptorPromiseBase<RollupFieldMetricsDescriptor<T>, IList<IRollupFieldMetric>>
		where T : class
	{
		public RollupFieldMetricsDescriptor() : base(new List<IRollupFieldMetric>()) { }

		public RollupFieldMetricsDescriptor<T> Field(Expression<Func<T, object>> field, params RollupMetric[] metrics) =>
			Assign(a => a.Add(new RollupFieldMetric { Field = field, Metrics = metrics }));

		public RollupFieldMetricsDescriptor<T> Field(Field field, params RollupMetric[] metrics) =>
			Assign(a => a.Add(new RollupFieldMetric { Field = field, Metrics = metrics }));

		public RollupFieldMetricsDescriptor<T> Field(Expression<Func<T, object>> field, IEnumerable<RollupMetric> metrics) =>
			Assign(a => a.Add(new RollupFieldMetric { Field = field, Metrics = metrics }));

		public RollupFieldMetricsDescriptor<T> Field(Field field, IEnumerable<RollupMetric> metrics) =>
			Assign(a => a.Add(new RollupFieldMetric { Field = field, Metrics = metrics }));
	}
}
