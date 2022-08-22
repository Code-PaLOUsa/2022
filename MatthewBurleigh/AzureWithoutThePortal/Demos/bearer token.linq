<Query Kind="Statements">
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Net.Http.Headers</Namespace>
  <Namespace>System.Text.Json.Serialization</Namespace>
  <Namespace>System.Text.Json</Namespace>
</Query>

string tenantId = "";
string clientId = "";
string clientSecret = "";

// replicates the functionality of az account list

var url = "https://management.azure.com/subscriptions?api-version=2020-01-01";
var response = await GetResponseAuthenticated(url);
var content = await response.Content.ReadAsStringAsync();//.Dump("response content");
JsonSerializer.Deserialize<Root>(content).Value.Dump("subscriptions");

async Task<HttpResponseMessage> GetResponseAuthenticated(string url)
{
	// client id & secret come from the enterprise app registration
	var token = await GetBearerTokenOauth(tenantId, clientId, clientSecret);
	var client = new HttpClient();
	client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
	return await client.GetAsync(url);
}

static async Task<string> GetBearerTokenOauth(string tenantId, string clientId, string clientSecret, string resource = "https://management.azure.com")
{
	var client = new HttpClient();

	var response = await client.PostAsync($"https://login.microsoftonline.com/{tenantId}/oauth2/token",
		new StringContent($"grant_type=client_credentials&client_id={clientId}&client_secret={clientSecret}&resource={resource}",
			System.Text.Encoding.UTF8, "application/x-www-form-urlencoded"));
	var content = await response.Content.ReadAsStringAsync();
	var responseJson = JsonSerializer.Deserialize<TokenResponse>(content);

	return responseJson.access_token;
}

class TokenResponse
{
	public string? token_type { get; set; }
	public string? expires_in { get; set; }
	public string? ext_expires_in { get; set; }
	public string? not_before { get; set; }
	public string? resource { get; set; }
	public string? access_token { get; set; }
}

public class Count
{
	[JsonPropertyName("type")]
	public string? Type { get; set; }

	[JsonPropertyName("value")]
	public int Value { get; set; }
}

public class Root
{
	[JsonPropertyName("value")]
	public List<Value>? Value { get; set; }

	[JsonPropertyName("count")]
	public Count? Count { get; set; }
}

public class SubscriptionPolicies
{
	[JsonPropertyName("locationPlacementId")]
	public string? LocationPlacementId { get; set; }

	[JsonPropertyName("quotaId")]
	public string? QuotaId { get; set; }

	[JsonPropertyName("spendingLimit")]
	public string? SpendingLimit { get; set; }
}

public class Value
{
	//[JsonPropertyName("id")]
	//public string? Id { get; set; }

	[JsonPropertyName("authorizationSource")]
	public string? AuthorizationSource { get; set; }

	[JsonPropertyName("managedByTenants")]
	public List<object>? ManagedByTenants { get; set; }

	//[JsonPropertyName("subscriptionId")]
	//public string SubscriptionId { get; set; }

	//[JsonPropertyName("tenantId")]
	//public string TenantId { get; set; }

	[JsonPropertyName("displayName")]
	public string? DisplayName { get; set; }

	[JsonPropertyName("state")]
	public string? State { get; set; }

	[JsonPropertyName("subscriptionPolicies")]
	public SubscriptionPolicies? SubscriptionPolicies { get; set; }
}