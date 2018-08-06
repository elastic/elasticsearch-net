using Nest;
using Tests.Domain;

namespace Tests.ScratchPad.Runners.Inferrence
{
	public class IdInferenceRunner : RunBase
	{
		protected override int LoopCount => 1_000_000;

		protected override RoutineBase Routine() => this.Loop(() => Infer.Id(new Project {Name = "x"}), (c, f) => c.Infer.Id(f));
	}
}
