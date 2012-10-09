
namespace ProtocolLoadTest
{
    internal interface ITester
    {
        void Run(string indexName, int port, int numMessages, int bufferSize);
    }
}
