<Query Kind="Statements">
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Text.Json</Namespace>
</Query>

var pat = "";  // PAT needs Release read scope
var email = "";  // the email associated with the PAT

// get a list of classic release definitions

var httpClient = new HttpClient();
httpClient.DefaultRequestHeaders.Add("Authorization", $"Basic {Convert.ToBase64String(Encoding.ASCII.GetBytes($"{email}:{pat}"))}");

// classic release pipelines
var adoInstance = "";
var adoProject = "";
var releaseDefinitionsUrl = $"https://vsrm.dev.azure.com/{adoInstance}/{adoProject}/_apis/release/definitions?api-version=6.0";
var response = await httpClient.GetAsync(releaseDefinitionsUrl);//.Dump("response");
var content = await response.Content.ReadAsStringAsync();//.Dump("response content");
var definitions = JsonSerializer.Deserialize<ReleaseDefinition>(content);//.Dump("release definitions");
definitions.value.Dump("full definitions");
definitions.value.ToDictionary(x => x.id, x => x.name).OrderBy(x => x.Value).Dump("id & name");

//public class Avatar
//{
//	public string href { get; set; }
//}

public class CreatedBy
{
	public string displayName { get; set; }
	//public string url { get; set; }
	//public Links _links { get; set; }
	//public string id { get; set; }
	//public string uniqueName { get; set; }
	//public string imageUrl { get; set; }
	public bool inactive { get; set; }
	public string descriptor { get; set; }
}

//public class Links
//{
//	public Avatar avatar { get; set; }
//	public Self self { get; set; }
//	public Web web { get; set; }
//}

public class ModifiedBy
{
	public string displayName { get; set; }
	//public string url { get; set; }
	//public Links _links { get; set; }
	//public string id { get; set; }
	//public string uniqueName { get; set; }
	//public string imageUrl { get; set; }
	public bool inactive { get; set; }
	public string descriptor { get; set; }
}

public class Properties
{
}

public class ReleaseDefinition
{
	public int count { get; set; }
	public List<Value> value { get; set; }
}

//public class Self
//{
//	public string href { get; set; }
//}

public class Value
{
	public string source { get; set; }
	public int revision { get; set; }
	public object description { get; set; }
	public CreatedBy createdBy { get; set; }
	public DateTime createdOn { get; set; }
	public ModifiedBy modifiedBy { get; set; }
	public DateTime modifiedOn { get; set; }
	public bool isDeleted { get; set; }
	public object variableGroups { get; set; }
	public string releaseNameFormat { get; set; }
	public Properties properties { get; set; }
	public int id { get; set; }
	public string name { get; set; }
	public string path { get; set; }
	public object projectReference { get; set; }
	//public string url { get; set; }
	//public Links _links { get; set; }
}

//public class Web
//{
//	public string href { get; set; }
//}