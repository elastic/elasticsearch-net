// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Nest;
using Tests.Domain;

namespace Tests.ScratchPad.Runners.Inferrence
{
	public class PropertyNameInferenceRunner : RunBase
	{
		protected override RoutineBase Routine() =>
			Loop(
				() => Infer.Property<Project>(p => p.LeadDeveloper.FirstName),
				(c, f) => c.Infer.PropertyName(f)
			);
	}
}
