using Newtonsoft.Json;

namespace Application.Utils;

public class JsonReader(string jsonString)
{
    private readonly Dictionary<string, object> _json =
        JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonString)!;

    public string Value(params string[] keys)
    {
        var value = AccessValues(_json, keys)!;
        return value;
    }

    private string? AccessValues(Dictionary<string, object> data, string[] keys, int currentKeyIndex = 0)
    {
        if (currentKeyIndex >= keys.Length) return null;
        var child = data[keys[currentKeyIndex]];
        if (currentKeyIndex == keys.Length - 1) return child.ToString();
        return AccessValues(
            JsonConvert.DeserializeObject<Dictionary<string, object>>(child.ToString()!)!, 
            keys,
            currentKeyIndex + 1
        );
    }
}