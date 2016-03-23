using System;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	public interface IDecayFunction : IScoreFunction
	{
		string DecayType { get; }

		Field Field { get; set; }

		[JsonProperty(PropertyName = "decay")]
		double? Decay { get; set; }

		[JsonProperty(PropertyName = "multi_value_mode")]
		MultiValueMode? MultiValueMode { get; set; }


	}
	public interface IDecayFunction<TOrigin, TScale> : IDecayFunction
	{

		[JsonProperty(PropertyName = "origin")]
		TOrigin Origin { get; set; }

		[JsonProperty(PropertyName = "scale")]
		TScale Scale { get; set; }

		[JsonProperty(PropertyName = "offset")]
		TScale Offset { get; set; }

	}


	public abstract class DecayFunctionBase<TOrigin, TScale> : FunctionScoreFunctionBase, IDecayFunction<TOrigin, TScale>
	{
		protected abstract string DecayType { get; }

		string IDecayFunction.DecayType => this.DecayType;

		public Field Field { get; set; }

		public TOrigin Origin { get; set; }

		public TScale Scale { get; set; }

		public TScale Offset { get; set; }

		public double? Decay { get; set; }

		public MultiValueMode? MultiValueMode { get; set; }
	}

	public abstract class DecayFunctionDescriptorBase<TDescriptor, TOrigin, TScale, T>
		: FunctionScoreFunctionDescriptorBase<TDescriptor, IDecayFunction<TOrigin, TScale>, T>, IDecayFunction<TOrigin, TScale>
		where TDescriptor : DecayFunctionDescriptorBase<TDescriptor, TOrigin, TScale, T>, IDecayFunction<TOrigin, TScale>
		where T : class
	{
		protected abstract string DecayType { get; }

		string IDecayFunction.DecayType => this.DecayType;

		Field IDecayFunction.Field { get; set; }

		TOrigin IDecayFunction<TOrigin, TScale>.Origin { get; set; }

		TScale IDecayFunction<TOrigin, TScale>.Scale { get; set; }

		TScale IDecayFunction<TOrigin, TScale>.Offset { get; set; }

		double? IDecayFunction.Decay { get; set; }

		MultiValueMode? IDecayFunction.MultiValueMode { get; set; }

		public TDescriptor Origin(TOrigin origin) => Assign(a => a.Origin = origin);

		public TDescriptor Scale(TScale scale) => Assign(a => a.Scale = scale);

		public TDescriptor Offset(TScale offset) => Assign(a => a.Offset = offset);

		public TDescriptor Decay(double? decay) => Assign(a => a.Decay = decay);

		public TDescriptor MultiValueMode(MultiValueMode? mode) => Assign(a => a.MultiValueMode = mode);

		public TDescriptor Field(Field field) => Assign(a => a.Field = field);

		public TDescriptor Field(Expression<Func<T, object>> field) => Assign(a => a.Field = field);
	}
}
