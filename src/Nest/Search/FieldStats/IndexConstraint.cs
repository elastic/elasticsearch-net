using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	public interface IIndexConstraint
	{
		[JsonProperty("min_value")]
		IIndexConstraintComparison MinValue { get; set; }

		[JsonProperty("max_value")]
		IIndexConstraintComparison MaxValue { get; set; }
	}

	[JsonObject]
	public class IndexConstraint : IIndexConstraint
	{
		public IIndexConstraintComparison MinValue { get; set; }
		public IIndexConstraintComparison MaxValue { get; set; }
	}

	public class IndexConstraintDescriptor
		: DescriptorBase<IndexConstraintDescriptor, IIndexConstraint>, IIndexConstraint
	{
		IIndexConstraintComparison IIndexConstraint.MaxValue { get; set; }
		IIndexConstraintComparison IIndexConstraint.MinValue { get; set; }

		public IndexConstraintDescriptor MinValue(Func<IndexConstraintComparisonDescriptor, IIndexConstraintComparison> minValue) =>
			Assign(a => a.MinValue = minValue?.Invoke(new IndexConstraintComparisonDescriptor()));

		public IndexConstraintDescriptor MaxValue(Func<IndexConstraintComparisonDescriptor, IIndexConstraintComparison> maxValue) =>
			Assign(a => a.MaxValue = maxValue?.Invoke(new IndexConstraintComparisonDescriptor()));
	}

	public interface IIndexConstraintComparison
	{
		[JsonProperty("gte")]
		string GreaterThanOrEqualTo { get; set; }

		[JsonProperty("gt")]
		string GreaterThan { get; set; }

		[JsonProperty("lte")]
		string LessThanOrEqualTo { get; set; }

		[JsonProperty("lt")]
		string LessThan { get; set; }

		[JsonProperty("format")]
		string Format { get; set; }
	}

	public class IndexConstraintComparison : IIndexConstraintComparison
	{
		public string GreaterThanOrEqualTo { get; set; }
		public string GreaterThan { get; set; }
		public string LessThanOrEqualTo { get; set; }
		public string LessThan { get; set; }
		public string Format { get; set; }
	}

	public class IndexConstraintComparisonDescriptor
		: DescriptorBase<IndexConstraintComparisonDescriptor, IIndexConstraintComparison>, IIndexConstraintComparison
	{
		string IIndexConstraintComparison.Format { get; set; }
		string IIndexConstraintComparison.GreaterThan { get; set; }
		string IIndexConstraintComparison.GreaterThanOrEqualTo { get; set; }
		string IIndexConstraintComparison.LessThan { get; set; }
		string IIndexConstraintComparison.LessThanOrEqualTo { get; set; }

		public IndexConstraintComparisonDescriptor Format(string format) => Assign(a => a.Format = format);
		public IndexConstraintComparisonDescriptor GreaterThan(string gt) => Assign(a => a.GreaterThan = gt);
		public IndexConstraintComparisonDescriptor GreaterThanOrEqualTo(string gte) => Assign(a => a.GreaterThanOrEqualTo = gte);
		public IndexConstraintComparisonDescriptor LessThan(string lt) => Assign(a => a.LessThan = lt);
		public IndexConstraintComparisonDescriptor LessThanOrEqualTo(string lte) => Assign(a => a.LessThanOrEqualTo = lte);
	}
}
