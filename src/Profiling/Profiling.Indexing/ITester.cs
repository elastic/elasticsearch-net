
namespace Profiling.Indexing
{
    internal interface ITester
    {
        void Run(string indexName, int port, int numMessages, int bufferSize);
		void SearchUsingSingleClient(string indexName, int port, int numberOfSearches);
		void SearchUsingMultipleClients(string indexName, int port, int numberOfSearches);
    }
}
