using FluentAssertions;
using Nest;
using System.Linq;
using Tests.Framework;
using Tests.Framework.MockData;

namespace Tests.Framework
{
	public class ClientCallAssertations
	{
		private VirtualizedCluster _cluster;
		public ISearchResponse<Project> Response { get; }

		public ClientCallAssertations(VirtualizedCluster cluster)
		{
			this._cluster = cluster;
			this.Response = cluster.ClientCall();
		}

		public ClientCallAssertations Sees(Audits audits)
		{
			var auditTrail = this.Response.ConnectionStatus.AuditTrail;
			auditTrail.Count().Should().Be(audits.Count(),
			"Expected {0} audit assertions to test ALL seen audits but received {1}",
			auditTrail.Count(), audits.Count()
			);
			foreach (var audit in auditTrail.Select((s, i) => new { s, i }))
			{
				var because = $"thats the type specified on the {(audit.i + 1).ToOrdinal()} audit";
				audit.s.Event.Should().Be(audits[audit.i].Key, because);
				audits[audit.i].Value?.Invoke(audit.s);
			}

			return this;
		}
	}
}