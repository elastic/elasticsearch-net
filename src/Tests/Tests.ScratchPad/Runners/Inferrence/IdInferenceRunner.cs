using System;
using Nest;
using Tests.Framework.MockData;

namespace ClientMasterScratch
{
	public class IdInferenceRunner : RunBase
	{
		protected override int LoopCount => 1_000_000;

		protected override RoutineBase Routine() => this.Loop(() => Infer.Id(new Project {Name = "x"}), (c, f) => c.Infer.Id(f));
	}
}
