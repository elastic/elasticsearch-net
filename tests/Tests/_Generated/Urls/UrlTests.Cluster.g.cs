// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.
//
// ███╗   ██╗ ██████╗ ████████╗██╗ ██████╗███████╗ 
// ████╗  ██║██╔═══██╗╚══██╔══╝██║██╔════╝██╔════╝ 
// ██╔██╗ ██║██║   ██║   ██║   ██║██║     █████╗   
// ██║╚██╗██║██║   ██║   ██║   ██║██║     ██╔══╝   
// ██║ ╚████║╚██████╔╝   ██║   ██║╚██████╗███████╗ 
// ╚═╝  ╚═══╝ ╚═════╝    ╚═╝   ╚═╝ ╚═════╝╚══════╝ 
// ------------------------------------------------
//
// This file is automatically generated.
// Please do not edit these files manually.
// Run the following in the root of the repository:
//
// TODO - RUN INSTRUCTIONS
//
// ------------------------------------------------
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.Threading.Tasks;
using Tests.Domain;
using Tests.Framework.EndpointTests;

namespace Tests.Urls.Cluster
{
    public class ClusterAllocationExplainUrlTests : UrlTestsBase
    {
        [U]
        public override async Task Urls()
        {
            await UrlTester.POST("/_cluster/allocation/explain").Request(c => c.Cluster.AllocationExplain(new ClusterAllocationExplainRequest())).RequestAsync(c => c.Cluster.AllocationExplainAsync(new ClusterAllocationExplainRequest()));
        }
    }

    public class ClusterDeleteVotingConfigExclusionsUrlTests : UrlTestsBase
    {
        [U]
        public override async Task Urls()
        {
            await UrlTester.DELETE("/_cluster/voting_config_exclusions").Request(c => c.Cluster.DeleteVotingConfigExclusions(new ClusterDeleteVotingConfigExclusionsRequest())).RequestAsync(c => c.Cluster.DeleteVotingConfigExclusionsAsync(new ClusterDeleteVotingConfigExclusionsRequest()));
        }
    }

    public class ClusterGetComponentTemplateUrlTests : UrlTestsBase
    {
        [U]
        public override async Task Urls()
        {
            await UrlTester.GET("/_component_template").Request(c => c.Cluster.GetComponentTemplate(new ClusterGetComponentTemplateRequest())).RequestAsync(c => c.Cluster.GetComponentTemplateAsync(new ClusterGetComponentTemplateRequest()));
        }
    }

    public class ClusterGetSettingsUrlTests : UrlTestsBase
    {
        [U]
        public override async Task Urls()
        {
            await UrlTester.GET("/_cluster/settings").Request(c => c.Cluster.GetSettings(new ClusterGetSettingsRequest())).RequestAsync(c => c.Cluster.GetSettingsAsync(new ClusterGetSettingsRequest()));
        }
    }

    public class ClusterHealthUrlTests : UrlTestsBase
    {
        [U]
        public override async Task Urls()
        {
            await UrlTester.GET("/_cluster/health").Request(c => c.Cluster.Health(new ClusterHealthRequest())).RequestAsync(c => c.Cluster.HealthAsync(new ClusterHealthRequest()));
            await UrlTester.GET("/_cluster/health/_all").Request(c => c.Cluster.Health(new ClusterHealthRequest(Nest.Indices.All))).RequestAsync(c => c.Cluster.HealthAsync(new ClusterHealthRequest(Nest.Indices.All)));
            await UrlTester.GET("/_cluster/health/project").Request(c => c.Cluster.Health(new ClusterHealthRequest(Nest.IndexName.From<Project>()))).RequestAsync(c => c.Cluster.HealthAsync(new ClusterHealthRequest(Nest.IndexName.From<Project>())));
        }
    }

    public class ClusterPendingTasksUrlTests : UrlTestsBase
    {
        [U]
        public override async Task Urls()
        {
            await UrlTester.GET("/_cluster/pending_tasks").Request(c => c.Cluster.PendingTasks(new ClusterPendingTasksRequest())).RequestAsync(c => c.Cluster.PendingTasksAsync(new ClusterPendingTasksRequest()));
        }
    }

    public class ClusterPostVotingConfigExclusionsUrlTests : UrlTestsBase
    {
        [U]
        public override async Task Urls()
        {
            await UrlTester.POST("/_cluster/voting_config_exclusions").Request(c => c.Cluster.PostVotingConfigExclusions(new ClusterPostVotingConfigExclusionsRequest())).RequestAsync(c => c.Cluster.PostVotingConfigExclusionsAsync(new ClusterPostVotingConfigExclusionsRequest()));
        }
    }

    public class ClusterPutSettingsUrlTests : UrlTestsBase
    {
        [U]
        public override async Task Urls()
        {
            await UrlTester.PUT("/_cluster/settings").Request(c => c.Cluster.PutSettings(new ClusterPutSettingsRequest())).RequestAsync(c => c.Cluster.PutSettingsAsync(new ClusterPutSettingsRequest()));
        }
    }

    public class ClusterRemoteInfoUrlTests : UrlTestsBase
    {
        [U]
        public override async Task Urls()
        {
            await UrlTester.GET("/_remote/info").Request(c => c.Cluster.RemoteInfo(new RemoteInfoRequest())).RequestAsync(c => c.Cluster.RemoteInfoAsync(new RemoteInfoRequest()));
        }
    }

    public class ClusterRerouteUrlTests : UrlTestsBase
    {
        [U]
        public override async Task Urls()
        {
            await UrlTester.POST("/_cluster/reroute").Request(c => c.Cluster.Reroute(new ClusterRerouteRequest())).RequestAsync(c => c.Cluster.RerouteAsync(new ClusterRerouteRequest()));
        }
    }

    public class ClusterStateUrlTests : UrlTestsBase
    {
        [U]
        public override async Task Urls()
        {
            await UrlTester.GET("/_cluster/state").Request(c => c.Cluster.State(new ClusterStateRequest())).RequestAsync(c => c.Cluster.StateAsync(new ClusterStateRequest()));
        }
    }
}