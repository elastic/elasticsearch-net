using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(AggregationJsonConverter<MatrixStatsAggregation>))]
	public interface IMatrixStatsAggregation : IMatrixAggregation
	{
		[JsonProperty("mode")]
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
