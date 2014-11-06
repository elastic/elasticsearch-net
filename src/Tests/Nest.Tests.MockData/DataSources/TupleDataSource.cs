using System;
using AutoPoco.Engine;

namespace Nest.Tests.MockData.DataSources
{
    public class TupleDataSource : DatasourceBase<Tuple<int, int>>
    {
        public override Tuple<int, int> Next(IGenerationSession session)
        {
            return Tuple.Create(1, 1);
        }
    }
}
