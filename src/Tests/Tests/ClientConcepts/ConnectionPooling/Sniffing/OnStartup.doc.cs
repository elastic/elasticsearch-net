using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using Tests.Framework;
using static Tests.Framework.TimesHelper;
using static Elasticsearch.Net.AuditEvent;

namespace Tests.ClientConcepts.ConnectionPooling.Sniffing
{
    public class OnStartupSniffing
    {
        /**=== Sniffing on startup
         *
        * <<connection-pooling, Connection pools>> that return `true` for `SupportsReseeding`
        * will sniff on startup by default.
        */
        [U] [SuppressMessage("AsyncUsage", "AsyncFixer001:Unnecessary async/await usage", Justification = "Its a test")]
        public async Task ASniffOnStartupHappens()
        {
            /** We can demonstrate this by creating a _virtual_ Elasticsearch cluster using NEST's Test cluster framework.
             *
            * Here we create a 10 node cluster that uses a <<sniffing-connection-pool,Sniffing connection pool>>, setting
            * sniff to fail on all nodes _*except*_ 9202
            */
            var audit = new Auditor(() => Framework.Cluster
                .Nodes(10)
                .Sniff(s => s.Fails(Always))
                .Sniff(s => s.OnPort(9202).Succeeds(Always))
                .SniffingConnectionPool()
                .AllDefaults()
            );

            /**
            * When the client call is made, from the audit trail we see that the pool first tried to sniff on startup,
            * with a sniff failure on 9200 and 9201, followed by a sniff success on 9202. A ping and then healthy response are made on
            * 9200
            */
             await audit.TraceCall(
                 new ClientCall
                 {
                    { SniffOnStartup},
                    { SniffFailure, 9200},
                    { SniffFailure, 9201},
                    { SniffSuccess, 9202},
                    { PingSuccess , 9200},
                    { HealthyResponse, 9200}
                }
             );
        }

        /** ==== Occurs once
         * **A sniff on startup only happens once**. That is,
         *
         * . a sniff is attempted on a node
         * . if that node fails, a sniff is attempted on the next node
         * . this continues until a sniff succeeds
         * . a sniff on startup does not occur again.
         *
         */
        [U] [SuppressMessage("AsyncUsage", "AsyncFixer001:Unnecessary async/await usage", Justification = "Its a test")]
        public async Task ASniffOnStartupHappensOnce()
        {
            var audit = new Auditor(() => Framework.Cluster
                .Nodes(10)
                .Sniff(s => s.Fails(Always))
                .Sniff(s => s.OnPort(9202).Succeeds(Always))
                .SniffingConnectionPool()
                .AllDefaults()
            );

             await audit.TraceCalls(
                 new ClientCall
                 {
                    { SniffOnStartup},
                    { SniffFailure, 9200},
                    { SniffFailure, 9201},
                    { SniffSuccess, 9202},
                    { PingSuccess , 9200},
                    { HealthyResponse, 9200}
                },
                new ClientCall
                {
                    { PingSuccess, 9201},
                    { HealthyResponse, 9201}
                }
            );
        }

        /** ==== Uses cluster state
         *
         * **A sniff on startup will use the returned cluster state**.
         *
         * In this next example, the state of the cluster returned from the successful sniff
         * is used for subsequent client requests
         */
        [U] [SuppressMessage("AsyncUsage", "AsyncFixer001:Unnecessary async/await usage", Justification = "Its a test")]
        public async Task SniffOnStartUpTakesNewClusterState()
        {
            var audit = new Auditor(() => Framework.Cluster
                .Nodes(10)
                .Sniff(s => s.Fails(Always))
                .Sniff(s => s.OnPort(9202).Succeeds(Always, Framework.Cluster.Nodes(8, startFrom: 9204))) // <1> Sniffing returns 8 nodes, starting from 9204
                .SniffingConnectionPool()
                .AllDefaults()
            );

            await audit.TraceCall(new ClientCall {
                { SniffOnStartup},
                { SniffFailure, 9200},
                { SniffFailure, 9201},
                { SniffSuccess, 9202},
                { PingSuccess, 9204}, // <2> After successfully sniffing, the ping now happens on 9204
                { HealthyResponse, 9204}
            });
        }

        //hide
        [U] [SuppressMessage("AsyncUsage", "AsyncFixer001:Unnecessary async/await usage", Justification = "Its a test")]
        public async Task SniffTriesAllNodes()
        {
            var audit = new Auditor(() => Framework.Cluster
                .Nodes(10)
                .Sniff(s => s.Fails(Always))
                .Sniff(s => s.OnPort(9209).Succeeds(Always))
                .SniffingConnectionPool()
                .AllDefaults()
            );

            await audit.TraceCall(new ClientCall {
                { SniffOnStartup},
                { SniffFailure, 9200},
                { SniffFailure, 9201},
                { SniffFailure, 9202},
                { SniffFailure, 9203},
                { SniffFailure, 9204},
                { SniffFailure, 9205},
                { SniffFailure, 9206},
                { SniffFailure, 9207},
                { SniffFailure, 9208},
                { SniffSuccess, 9209},
                { PingSuccess, 9200},
                { HealthyResponse, 9200}
            });
        }

        /**==== Prefers master eligible nodes
         *
         * Sniffing prefers to run on master eligible nodes
         */
        [U] [SuppressMessage("AsyncUsage", "AsyncFixer001:Unnecessary async/await usage", Justification = "Its a test")]
        public async Task SniffPrefersMasterNodes()
        {
            var audit = new Auditor(() => Framework.Cluster
                .Nodes(new[] {
                    new Node(new Uri("http://localhost:9200")) { MasterEligible = false },
                    new Node(new Uri("http://localhost:9201")) { MasterEligible = false },
                    new Node(new Uri("http://localhost:9202")) { MasterEligible = true },
                })
                .Sniff(s => s.Succeeds(Always))
                .SniffingConnectionPool()
                .AllDefaults()
            );

            await audit.TraceCall(new ClientCall {
                { SniffOnStartup},
                { SniffSuccess, 9202},
                { PingSuccess, 9200},
                { HealthyResponse, 9200}
            });
        }

        /**
         * although it will fail over to non-master eligible nodes when sniffing fails on master eligible nodes
         */
        [U] [SuppressMessage("AsyncUsage", "AsyncFixer001:Unnecessary async/await usage", Justification = "Its a test")]
        public async Task SniffPrefersMasterNodesButStillFailsOver()
        {
            var audit = new Auditor(() => Framework.Cluster
                .Nodes(new[] {
                    new Node(new Uri("http://localhost:9200")) { MasterEligible = true },
                    new Node(new Uri("http://localhost:9201")) { MasterEligible = true },
                    new Node(new Uri("http://localhost:9202")) { MasterEligible = false },
                })
                .Sniff(s => s.Fails(Always))
                .Sniff(s => s.OnPort(9202).Succeeds(Always))
                .SniffingConnectionPool()
                .AllDefaults()
            );

            await audit.TraceCall(new ClientCall {
                { SniffOnStartup},
                { SniffFailure, 9200},
                { SniffFailure, 9201},
                { SniffSuccess, 9202},
                { PingSuccess, 9200},
                { HealthyResponse, 9200}
            });
        }
    }
}
