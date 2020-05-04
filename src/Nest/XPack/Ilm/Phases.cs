// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;

namespace Nest
{
	[ReadAs(typeof(Phases))]
	public interface IPhases
	{
		[DataMember(Name = "cold")]
		IPhase Cold { get; set; }

		[DataMember(Name = "delete")]
		IPhase Delete { get; set; }

		[DataMember(Name = "hot")]
		IPhase Hot { get; set; }

		[DataMember(Name = "warm")]
		IPhase Warm { get; set; }
	}

	public class Phases : IPhases
	{
		public IPhase Cold { get; set; }
		public IPhase Delete { get; set; }
		public IPhase Hot { get; set; }
		public IPhase Warm { get; set; }
	}

	public class PhasesDescriptor : DescriptorBase<PhasesDescriptor, IPhases>, IPhases
	{
		IPhase IPhases.Cold { get; set; }
		IPhase IPhases.Delete { get; set; }
		IPhase IPhases.Hot { get; set; }
		IPhase IPhases.Warm { get; set; }

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
