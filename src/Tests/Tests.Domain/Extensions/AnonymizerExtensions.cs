using Elasticsearch.Net;
using Nest;

namespace Tests.Domain.Extensions
{
	public static class AnonymizerExtensions
	{
		private static readonly Inferrer Infer = new Inferrer(new ConnectionSettings(new InMemoryConnection()).ApplyDomainSettings());

		public static object ToAnonymousObject(this JoinField field) =>
			field.Match<object>(p => Infer.RelationName(p.Name), c => new
			{
				parent = Infer.Id(c.Parent),
				name = Infer.RelationName(c.Name)

			});
	}
}
