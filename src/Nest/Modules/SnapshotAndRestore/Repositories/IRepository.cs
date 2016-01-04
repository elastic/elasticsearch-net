using Newtonsoft.Json;

namespace Nest
{
	public interface IRepository
	{
		[JsonProperty("type")]
		string Type { get; }
	}

	public interface IRepository<TSettings> : IRepository
		where TSettings : class, IRepositorySettings
	{
		[JsonProperty("settings")]
		TSettings Settings { get; set; }
	}

	public interface IRepositorySettings { }
}