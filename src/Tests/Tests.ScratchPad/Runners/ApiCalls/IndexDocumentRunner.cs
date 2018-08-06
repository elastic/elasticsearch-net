using System;
using Tests.Domain;
using Tests.Framework.MockData;

namespace ClientMasterScratch
{
	public class IndexDocumentRunner : RunBase
	{
		protected override int LoopCount => 10_000;

		protected override RoutineBase Routine() => this.Loop(() => new Project {Name = "x"}, (c, d) => c.IndexDocument(d));
	}
}
