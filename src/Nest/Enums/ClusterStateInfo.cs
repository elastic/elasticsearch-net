using System;

namespace Nest
{
    [Flags]
    public enum ClusterStateInfo
    {
        All = 0,
        ExcludeNodes = 1 << 1,
        ExcludeRoutingTable = 1 << 2,
        ExcludeMetadata = 1 << 3,
        ExcludeBlocks = 1 << 4
    }
}
