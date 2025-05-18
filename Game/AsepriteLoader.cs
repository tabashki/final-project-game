using System.IO;
using Newtonsoft.Json;
using TeamCherry.Project;

public static class AsepriteLoader
{
    public static AsepriteData LoadAsepriteData(string jsonPath)
    {
        string json = File.ReadAllText(jsonPath);
        return JsonConvert.DeserializeObject<AsepriteData>(json);
    }
}
