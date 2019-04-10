using System;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IIlmMoveToStepRequest
	{
		[JsonProperty("current_step")]
		IStepKey CurrentStep { get; set; }

		[JsonProperty("next_step")]
		IStepKey NextStep { get; set; }
	}

	public interface IStepKey
	{
		[JsonProperty("phase")]
		string Phase { get; set; }

		[JsonProperty("action")]
		string Action { get; set; }

		[JsonProperty("name")]
		string Name { get; set; }
	}

	public partial class IlmMoveToStepRequest
	{
		public IStepKey CurrentStep { get; set; }
		public IStepKey NextStep { get; set; }
	}

	[DescriptorFor("IlmMoveToStep")]
	public partial class IlmMoveToStepDescriptor
	{
		IStepKey IIlmMoveToStepRequest.CurrentStep { get; set; }
		IStepKey IIlmMoveToStepRequest.NextStep { get; set; }

		public IlmMoveToStepDescriptor CurrentStep(Func<StepKeyDescriptor, IStepKey> selector)
			=> Assign(selector, (a, v) => a.CurrentStep = v?.Invoke(new StepKeyDescriptor()));

		public IlmMoveToStepDescriptor NextStep(Func<StepKeyDescriptor, IStepKey> selector)
			=> Assign(selector, (a, v) => a.NextStep = v?.Invoke(new StepKeyDescriptor()));
	}

	public class StepKeyDescriptor : DescriptorBase<StepKeyDescriptor, IStepKey>, IStepKey
	{
		string IStepKey.Phase { get; set; }
		string IStepKey.Action { get; set; }
		string IStepKey.Name { get; set; }

		public StepKeyDescriptor Phase(string phase) => Assign(phase, (a, v) => a.Phase = v);
		public StepKeyDescriptor Action(string action) => Assign(action, (a, v) => a.Action = v);
		public StepKeyDescriptor Name(string name) => Assign(name, (a, v) => a.Name = v);
	}
}
