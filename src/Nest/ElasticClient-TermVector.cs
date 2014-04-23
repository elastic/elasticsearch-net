using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
    public partial class ElasticClient
    {
        public ITermVectorResponse TermVector<T>(Func<TermvectorDescriptor<T>, TermvectorDescriptor<T>> termVectorSelector)
            where T : class
        {
            return this.Dispatch<TermvectorDescriptor<T>, TermvectorQueryString, TermVectorResponse>(
                termVectorSelector,
                (p, d) => this.RawDispatch.TermvectorDispatch(p, d)
            );
        }

        public Task<ITermVectorResponse> TermVectorAsync<T>(Func<TermvectorDescriptor<T>, TermvectorDescriptor<T>> termVectorSelector)
            where T : class
        {
            return this.DispatchAsync<TermvectorDescriptor<T>, TermvectorQueryString, TermVectorResponse, ITermVectorResponse>(
                termVectorSelector,
                (p, d) => this.RawDispatch.TermvectorDispatchAsync(p, d)
            );
        }
    }
}
