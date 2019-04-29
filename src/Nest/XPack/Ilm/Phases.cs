using System;
using Newtonsoft.Json;

namespace Nest
{
	public interface IPhases
	{
		[JsonProperty("warm")]
		IPhase Warm { get; set; }

		[JsonProperty("hot")]
		IPhase Hot { get; set; }

		[JsonProperty("cold")]
		IPhase Cold { get; set; }

		[JsonProperty("delete")]
		IPhase Delete { get; set; }
	}

	public class Phases : IPhases
	{
		public IPhase Warm { get; set; }
		public IPhase Hot { get; set; }
		public IPhase Cold { get; set; }
		public IPhase Delete { get; set; }
	}

	public class PhasesDescriptor : IDescriptor, IPhases
	{
		IPhase IPhases.Warm { get; set; }
		IPhase IPhases.Hot { get; set; }
		IPhase IPhases.Cold { get; set; }
		IPhase IPhases.Delete { get; set; }

		private PhasesDescriptor Assign<TValue>(TValue value, Action<IPhases, TValue> assigner) => Fluent.Assign(this, value, assigner);

		public PhasesDescriptor Warm(Func<PhaseDescriptor, IPhase> selector) =>
			Assign(selector, (a, v) => a.Warm = v?.InvokeOrDefault(new PhaseDescriptor()));

		public PhasesDescriptor Hot(Func<PhaseDescriptor, IPhase> selector) =>
			Assign(selector, (a, v) => a.Hot = v?.InvokeOrDefault(new PhaseDescriptor()));

		public PhasesDescriptor Cold(Func<PhaseDescriptor, IPhase> selector) =>
			Assign(selector, (a, v) => a.Cold = v?.InvokeOrDefault(new PhaseDescriptor()));

		public PhasesDescriptor Delete(Func<PhaseDescriptor, IPhase> selector) =>
			Assign(selector, (a, v) => a.Delete = v?.InvokeOrDefault(new PhaseDescriptor()));
	}
}
