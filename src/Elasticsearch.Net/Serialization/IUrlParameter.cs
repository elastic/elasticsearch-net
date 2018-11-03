namespace Elasticsearch.Net
{
	public interface IUrlParameter
	{
		string GetString(IConnectionConfigurationValues settings);
	}
}
