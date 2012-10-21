using Newtonsoft.Json;
using System.Collections.Generic;
using Nest.Resolvers.Converters;

namespace Nest
{
  public interface IBulkResponse : IResponse
  {
    int Took { get; }
    IEnumerable<BulkOperationResponseItem> Items { get; }
  }
  [JsonObject]
  public class BulkResponse : BaseResponse, IBulkResponse
  {
    [JsonProperty("took")]
    public int Took { get; internal set; }

    [JsonProperty("items")]
    public IEnumerable<BulkOperationResponseItem> Items { get; internal set;  }
  }
  [JsonObject]
  public abstract class BulkOperationResponseItem
  {
    public abstract string Operation { get; internal set; }
    public abstract string Index { get; internal set; }
    public abstract string Type { get; internal set; }
    public abstract string Id { get; internal set; }
    public abstract string Version { get; internal set; }
    public abstract bool OK { get; internal set; }
  }

	[JsonObject]
  public class BulkDeleteResponseItem : BulkOperationResponseItem
	{
    public override string Operation { get; internal set; }
    [JsonProperty("_index")]
    public override string Index { get; internal set; }
    [JsonProperty("_type")]
    public override string Type { get; internal set; }
    [JsonProperty("_id")]
    public override string Id { get; internal set; }
    [JsonProperty("_version")]
    public override string Version { get; internal set; }
    [JsonProperty("ok")]
    public override bool OK { get; internal set; }
    
	}
  [JsonObject]
  public class BulkIndexResponseItem : BulkOperationResponseItem
  {
    public override string Operation { get; internal set; }
    [JsonProperty("_index")]
    public override string Index { get; internal set; }
    [JsonProperty("_type")]
    public override string Type { get; internal set; }
    [JsonProperty("_id")]
    public override string Id { get; internal set; }
    [JsonProperty("_version")]
    public override string Version { get; internal set; }
    [JsonProperty("ok")]
    public override bool OK { get; internal set; }

  }
}
