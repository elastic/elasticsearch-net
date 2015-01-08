using System;
using Elasticsearch.Net;

namespace Nest
{
    public class RestoreException : Exception
    {
        public IElasticsearchResponse Status { get; private set; }

        public RestoreException(IElasticsearchResponse status, string message = null)
            : base(message)
        {
            this.Status = status;
        }
    }
}