// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Tests.Domain;

namespace Tests.ScratchPad.Runners.ApiCalls
{
	public class IndexDocumentRunner : RunBase
	{
		protected override int LoopCount => 10_000;

		protected override RoutineBase Routine() => Loop(() => new Project { Name = "x" }, (c, d) => c.IndexDocument(d));
	}
}
