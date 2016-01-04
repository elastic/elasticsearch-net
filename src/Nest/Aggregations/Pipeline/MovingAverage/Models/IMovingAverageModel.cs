using System;

namespace Nest
{
	public interface IMovingAverageModel 
	{
		string Name { get; }
	}

	public class MovingAverageModelDescriptor : DescriptorBase<MovingAverageModelDescriptor, IDescriptor>
	{
		public IEwmaModel Ewma(Func<EwmaModelDescriptor, IEwmaModel> ewmaSelector = null) => 
			ewmaSelector.InvokeOrDefault(new EwmaModelDescriptor());

		public IHoltLinearModel HoltLinear(Func<HoltLinearModelDescriptor, IHoltLinearModel> holtSelector = null) => 
			holtSelector.InvokeOrDefault(new HoltLinearModelDescriptor());

		public IHoltWintersModel HoltWinters(Func<HoltWintersModelDescriptor, IHoltWintersModel> holtWintersSelector) => 
			holtWintersSelector?.Invoke(new HoltWintersModelDescriptor());

		public ILinearModel Linear(Func<LinearModelDescriptor, ILinearModel> linearSelector = null) => 
			linearSelector.InvokeOrDefault(new LinearModelDescriptor());

		public ISimpleModel Simple(Func<SimpleModelDescriptor, ISimpleModel> simpleSelector = null) => 
			simpleSelector.InvokeOrDefault(new SimpleModelDescriptor());
	}
}
