using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace Nest
{
	public interface IDecayFunction : IScoreFunction
	{
		[DataMember(Name ="decay")]
		double? Decay { get; set; }

		string DecayType { get; }

		Field Field { get; set; }

		[DataMember(Name ="multi_value_mode")]
		MultiValueMode? MultiValueMode { get; set; }
	}

	public interface IDecayFunction<TOrigin, TScale> : IDecayFunction
	{
		[DataMember(Name ="offset")]
		TScale Offset { get; set; }

		[DataMember(Name ="origin")]
		TOrigin Origin { get; set; }

		[DataMember(Name ="scale")]
		TScale Scale { get; set; }
	}


	public abstract class DecayFunctionBase<TOrigin, TScale> : FunctionScoreFunctionBase, IDecayFunction<TOrigin, TScale>
	{
		public double? Decay { get; set; }

		public Field Field { get; set; }

		public MultiValueMode? MultiValueMode { get; set; }

		public TScale Offset { get; set; }

		public TOrigin Origin { get; set; }

		public TScale Scale { get; set; }
		protected abstract string DecayType { get; }

		string IDecayFunction.DecayType => DecayType;
	}

	public abstract class DecayFunctionDescriptorBase<TDescriptor, TOrigin, TScale, T>
		: FunctionScoreFunctionDescriptorBase<TDescriptor, IDecayFunction<TOrigin, TScale>, T>, IDecayFunction<TOrigin, TScale>
		where TDescriptor : DecayFunctionDescriptorBase<TDescriptor, TOrigin, TScale, T>, IDecayFunction<TOrigin, TScale>
		where T : class
	{
		protected abstract string DecayType { get; }

		double? IDecayFunction.Decay { get; set; }

		string IDecayFunction.DecayType => DecayType;

		Field IDecayFunction.Field { get; set; }

		MultiValueMode? IDecayFunction.MultiValueMode { get; set; }

		TScale IDecayFunction<TOrigin, TScale>.Offset { get; set; }

		TOrigin IDecayFunction<TOrigin, TScale>.Origin { get; set; }

		TScale IDecayFunction<TOrigin, TScale>.Scale { get; set; }

		public TDescriptor Origin(TOrigin origin) => Assign(a => a.Origin = origin);

		public TDescriptor Scale(TScale scale) => Assign(a => a.Scale = scale);

		public TDescriptor Offset(TScale offset) => Assign(a => a.Offset = offset);

		public TDescriptor Decay(double? decay) => Assign(a => a.Decay = decay);

		public TDescriptor MultiValueMode(MultiValueMode? mode) => Assign(a => a.MultiValueMode = mode);

		public TDescriptor Field(Field field) => Assign(a => a.Field = field);

		public TDescriptor Field(Expression<Func<T, object>> field) => Assign(a => a.Field = field);
	}
}
