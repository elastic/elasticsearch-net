// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Nest;
using Tests.Domain;

namespace Tests.ScratchPad.Runners.Inferrence
{
	public class FieldInferenceRunner : RunBase
	{
		protected override int LoopCount => 1_000_000;

		protected override RoutineBase Routine() => Loop(() => Infer.Field<Project>(p => p.LeadDeveloper.LastName), (c, f) => c.Infer.Field(f));
	}
}
