using System.IO;
using Newtonsoft.Json;
using TeamCherry.Project;

namespace TeamCherry.Project
{
    public class AsepriteLoader : IAssetLoader
    {
        public Type AssetType => typeof(AsepriteSheet);
        public bool IgnoreCache => false;
        public string[] SupportedExtensions => new[] { ".json" };

        public object LoadFromStream(Stream stream, GameContentManager contentManager)
        {
            using var reader = new StreamReader(stream);
            string json = reader.ReadToEnd();
            return JsonConvert.DeserializeObject<AsepriteSheet>(json);
        }
    }
}
