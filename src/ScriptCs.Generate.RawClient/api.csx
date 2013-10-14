public class ApiRequest {
	public IDictionary<string, ApiEndpoint> Endpoint { get; set; }
}
public class ApiEndpoint {
	public string Documentation { get; set; }
	public IEnumerable<string> Methods { get; set; }
	public ApiUrl { get; set; }
	public ApiBody Body { get; set; }
}
public class ApiBody {
	public class Description { get; set; }
}
public class ApiUrl { 
	public string Path { get; set; }
	public IEnumerable<string> Paths { get; set; }
	public IDictionary<string, ApiUrlPart> Parts { get; set; }
	public IDictionary<string, ApiQueryParameters> Params { get; set; }
}
