using System;
using Elasticsearch.Net;

namespace Nest
{
    public class SnapshotException : Exception
    {
        public IElasticsearchResponse Status { get; private set; }

        public SnapshotException(IElasticsearchResponse status, string message) : base(message)
        {
            Status = status;
        }
    }
}