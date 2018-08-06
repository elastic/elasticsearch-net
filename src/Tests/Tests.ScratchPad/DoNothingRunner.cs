namespace Tests.ScratchPad
{
	public class DoNothingRunner : RunBase
	{
		protected override int LoopCount => 100_000_000;

		protected override RoutineBase Routine() => this.Loop(() => 1, (c, f) => { });
	}
}
