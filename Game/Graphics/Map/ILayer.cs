namespace TeamCherry.Project;

public interface ILayer
{
    public string? Name { get; init; }
    public int LayerWidth { get; init; }  // Should usually be same as MapWidth
    public int LayerHeight { get; init; }  // Should usually be same as MapHeight
    public int[]? Data { get; init; }
    public string? Type { get; init; }
    public bool Visible { get; init; }
    public int Id { get; init; }

}