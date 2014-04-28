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
        public IMultiTermVectorResponse MultiTermVectors<T>(Func<MtermvectorsDescriptor<T>, MtermvectorsDescriptor<T>> multiTermVectorsSelector)
            where T : class
        {
            return this.Dispatch<MtermvectorsDescriptor<T>, MtermvectorsRequestParameters, MultiTermVectorResponse>(
                multiTermVectorsSelector,
                (p, d) => this.RawDispatch.MtermvectorsDispatch<MultiTermVectorResponse>(p, d)
            );
        }

        public Task<IMultiTermVectorResponse> MultiTermVectorsAsync<T>(Func<MtermvectorsDescriptor<T>, MtermvectorsDescriptor<T>> multiTermVectorsSelector)
            where T : class
        {
            return this.DispatchAsync<MtermvectorsDescriptor<T>, MtermvectorsRequestParameters, MultiTermVectorResponse, IMultiTermVectorResponse>(
                multiTermVectorsSelector,
                (p, d) => this.RawDispatch.MtermvectorsDispatchAsync<MultiTermVectorResponse>(p, d)
            );
        }
    }
}
