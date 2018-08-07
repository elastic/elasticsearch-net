using Nest;
using Tests.Domain;

namespace Tests.ScratchPad.Runners.Inferrence
{
	public class PropertyNameInferenceRunner : RunBase
	{
		protected override RoutineBase Routine() =>
			this.Loop(
				create:() => Infer.Property<Project>(p => p.LeadDeveloper.FirstName),
				act: (c, f) => c.Infer.PropertyName(f)
			);
	}
}
