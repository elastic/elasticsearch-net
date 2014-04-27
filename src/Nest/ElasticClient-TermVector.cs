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
            return this.Dispatch<TermvectorDescriptor<T>, TermvectorRequestParameters, TermVectorResponse>(
                termVectorSelector, 
                (p, d) => this.RawDispatch.TermvectorDispatch<TermVectorResponse>(p, d)
            );
        }

        public Task<ITermVectorResponse> TermVectorAsync<T>(Func<TermvectorDescriptor<T>, TermvectorDescriptor<T>> termVectorSelector)
            where T : class
        {
            return this.DispatchAsync<TermvectorDescriptor<T>, TermvectorRequestParameters, TermVectorResponse, ITermVectorResponse>(
                termVectorSelector,
                (p, d) => this.RawDispatch.TermvectorDispatchAsync<TermVectorResponse>(p, d)
            );
        }
    }
}
