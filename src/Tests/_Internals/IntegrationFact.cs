using Xunit;

namespace Tests._Internals
{
	public class IntegrationFact : FactAttribute
	{
		public IntegrationFact()
		{
			if (!TestClient.RunIntegrationTests)
			{
				Skip = "Ignored because we are not running integration tests";
			}
		}
	}
}