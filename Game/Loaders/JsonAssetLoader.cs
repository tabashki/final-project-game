
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TeamCherry.Project;

class JsonAssetLoader<T> : IAssetLoader
{
    protected readonly JsonSerializerOptions defaultOptions = new JsonSerializerOptions
    {
        ReadCommentHandling = JsonCommentHandling.Skip,
        AllowTrailingCommas = true,
    };

    public Type AssetType => typeof(T);
    public string[] SupportedExtensions => [ ".json" ];
    public bool LoadSubAssets { get; set; } = true;

    protected void AddDefaultSubAssetLoaders(IList<JsonConverter> list, GameContentManager contentManager)
    {
        list.Add(new JsonSubAssetLoader<Texture2D>(contentManager));
    }

    public virtual object LoadFromStream(Stream stream, GameContentManager contentManager)
    {
        var reader = new StreamReader(stream);
        JsonSerializerOptions opts = defaultOptions;
        if (LoadSubAssets)
        {
            AddDefaultSubAssetLoaders(opts.Converters, contentManager);
        }

        T? asset = JsonSerializer.Deserialize<T>(reader.ReadToEnd(), opts);
        if (asset == null)
        {
            throw new ContentLoadException($"Failed to load asset from stream: {typeof(T)}");
        }
        return asset;
    }
}
