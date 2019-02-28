using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(MatrixStatsAggregation))]
	public interface IMatrixStatsAggregation : IMatrixAggregation
	{
		[DataMember(Name ="mode")]
		MatrixStatsMode? Mode { get; set; }
	}

	public class MatrixStatsAggregation : MatrixAggregationBase, IMatrixStatsAggregation
	{
		internal MatrixStatsAggregation() { }

		public MatrixStatsAggregation(string name, Fields fields) : base(name, fields) { }

		public MatrixStatsMode? Mode { get; set; }

		internal override void WrapInContainer(AggregationContainer c) => c.MatrixStats = this;
	}

	public class MatrixStatsAggregationDescriptor<T>
		: MatrixAggregationDescriptorBase<MatrixStatsAggregationDescriptor<T>, IMatrixStatsAggregation, T>
			, IMatrixStatsAggregation
		where T : class
	{
		MatrixStatsMode? IMatrixStatsAggregation.Mode { get; set; }

		public MatrixStatsAggregationDescriptor<T> Mode(MatrixStatsMode? mode) => Assign(a => a.Mode = mode);
	}
}
