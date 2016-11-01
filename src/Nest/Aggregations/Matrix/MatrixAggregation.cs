using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	public interface IMatrixAggregation : IAggregation
	{
		[JsonProperty("fields")]
		Fields Fields { get; set; }

		[JsonProperty("missing")]
		IDictionary<Field, double> Missing { get; set; }
	}

	public abstract class MatrixAggregationBase : AggregationBase, IMatrixAggregation
	{
		internal MatrixAggregationBase() { }

		protected MatrixAggregationBase(string name, Fields field) : base(name)
		{
			this.Fields = field;
		}

		public Fields Fields { get; set; }

		public IDictionary<Field, double> Missing { get; set; }
	}

	public abstract class MatrixAggregationDescriptorBase<TMatrixAggregation, TMatrixAggregationInterface, T>
		: DescriptorBase<TMatrixAggregation, TMatrixAggregationInterface>, IMatrixAggregation
		where TMatrixAggregation : MatrixAggregationDescriptorBase<TMatrixAggregation, TMatrixAggregationInterface, T>
		, TMatrixAggregationInterface, IMatrixStatsAggregation
		where T : class
		where TMatrixAggregationInterface : class, IMatrixAggregation
	{
		Fields IMatrixAggregation.Fields { get; set; }

		IDictionary<string, object> IAggregation.Meta { get; set; }

		IDictionary<Field, double> IMatrixAggregation.Missing { get; set; }

		string IAggregation.Name { get; set; }

		public TMatrixAggregation Field(Fields fields) => Assign(a => a.Fields = fields);

		public TMatrixAggregation Fields(Func<FieldsDescriptor<T>, IPromise<Fields>> fields) =>
			Assign(a => a.Fields = fields?.Invoke(new FieldsDescriptor<T>())?.Value);

		public TMatrixAggregation Missing(Func<FluentDictionary<Field, double>, FluentDictionary<Field, double>> selector) =>
			Assign(a => a.Missing = selector?.Invoke(new FluentDictionary<Field, double>()));

		public TMatrixAggregation Meta(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector) =>
			Assign(a => a.Meta = selector?.Invoke(new FluentDictionary<string, object>()));
	}
}
