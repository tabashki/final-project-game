
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Xna.Framework.Content;

namespace TeamCherry.Project;

// JsonConverter read-only specialization that supports sub-asset loading via path.
// Must be injected into the list of `Converters` in the `JsonSerializerOptions`
// used to read the object containing asset type `T` we want to load.
class JsonSubAssetLoader<T> : JsonConverter<T>
{
    // The content manager used to load the target type from a deserialized path
    protected ContentManager contentManager;

    public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? path = reader.GetString();
        if (path == null)
        {
            throw new JsonException($"Expected string path to load sub-asset with");
        }
        return contentManager.Load<T>(path);
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public JsonSubAssetLoader(GameContentManager contentManager)
    {
        this.contentManager = contentManager;
    }
}