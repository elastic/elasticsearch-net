// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Transport;
using Nest;

namespace Tests.Domain.Extensions
{
	public static class AnonymizerExtensions
	{
		private static readonly Inferrer Infer = new Inferrer(new ConnectionSettings(new InMemoryConnection()).ApplyDomainSettings());

		public static object ToAnonymousObject(this JoinField field) =>
			field.Match<object>(p => Infer.RelationName(p.Name), c => new
			{
				parent = Infer.Id(c.ParentId),
				name = Infer.RelationName(c.Name)
			});
	}
}
