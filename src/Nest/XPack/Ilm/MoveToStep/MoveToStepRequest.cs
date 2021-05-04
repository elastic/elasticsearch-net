// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;

namespace Nest
{
	[MapsApi("ilm.move_to_step")]
	public partial interface IMoveToStepRequest
	{
		[DataMember(Name = "current_step")]
		IStepKey CurrentStep { get; set; }

		[DataMember(Name = "next_step")]
		IStepKey NextStep { get; set; }
	}

	[ReadAs(typeof(StepKey))]
	public interface IStepKey
	{
		[DataMember(Name = "action")]
		string Action { get; set; }

		[DataMember(Name = "name")]
		string Name { get; set; }

		[DataMember(Name = "phase")]
		string Phase { get; set; }
	}

	public class StepKey : IStepKey
	{
		public string Action { get; set; }

		public string Name { get; set; }

		public string Phase { get; set; }
	}

	public partial class MoveToStepRequest
	{
		public IStepKey CurrentStep { get; set; }
		public IStepKey NextStep { get; set; }
	}

	public partial class MoveToStepDescriptor
	{
		IStepKey IMoveToStepRequest.CurrentStep { get; set; }
		IStepKey IMoveToStepRequest.NextStep { get; set; }

		public MoveToStepDescriptor CurrentStep(Func<StepKeyDescriptor, IStepKey> selector)
			=> Assign(selector, (a, v) => a.CurrentStep = v?.Invoke(new StepKeyDescriptor()));

		public MoveToStepDescriptor NextStep(Func<StepKeyDescriptor, IStepKey> selector)
			=> Assign(selector, (a, v) => a.NextStep = v?.Invoke(new StepKeyDescriptor()));
	}

	public class StepKeyDescriptor : DescriptorBase<StepKeyDescriptor, IStepKey>, IStepKey
	{
		string IStepKey.Action { get; set; }
		string IStepKey.Name { get; set; }
		string IStepKey.Phase { get; set; }

		public StepKeyDescriptor Phase(string phase) => Assign(phase, (a, v) => a.Phase = v);

		public StepKeyDescriptor Action(string action) => Assign(action, (a, v) => a.Action = v);

		public StepKeyDescriptor Name(string name) => Assign(name, (a, v) => a.Name = v);
	}
}
