using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace Tests.ClientConcepts.Troubleshooting
{
    /**=== Logging with Fiddler
     * 
     * A web debugging proxy such as http://www.telerik.com/fiddler[Fiddler] is a useful way to capture HTTP traffic 
     * from a machine, particularly whilst developing against a local Elasticsearch cluster.
     */
    public class LoggingWithFiddler
    {
        /**
         * ==== Capturing traffic to a remote cluster
         * 
         * To capture traffic against a remote cluster is as simple as launching Fiddler! You may want to also
         * filter traffic to only show requests to the remote cluster by using the filters tab
         * 
         * image::capture-requests-remotehost.png[Capturing requests to a remote host]
         * 
         * ==== Capturing traffic to a local cluster
         * 
         * The .NET Framework is hardcoded not to send requests for `localhost` through any proxies and as a proxy 
         * Fiddler will not receive such traffic.
         * 
         * This is easily circumvented by using `ipv4.fiddler` as the hostname instead of `localhost`
         */
        public void UsingAWebDebuggingProxy()
        {
            var isFiddlerRunning = Process.GetProcessesByName("fiddler").Any();
            var host = isFiddlerRunning ? "ipv4.fiddler" : "localhost";

            var connectionSettings = new ConnectionSettings(new Uri($"http://{host}:9200"))
                .PrettyJson(); // <1> prettify json requests and responses to make them easier to read in Fiddler

            var client = new ElasticClient(connectionSettings);
        }
        /**
         * With Fiddler running, the requests and responses will now be captured and can be inspected in the 
         * Inspectors tab
         * 
         * image::inspect-requests.png[Inspecting requests and responses]
         * 
         * As before, you may also want to filter traffic to only show requests to `ipv4.fiddler` on the port 
         * on which you are running Elasticsearch.
         * 
         * image::capture-requests-localhost.png[Capturing requests to localhost]
         */
    }
}
