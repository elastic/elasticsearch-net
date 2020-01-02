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
