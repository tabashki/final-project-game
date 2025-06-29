namespace TeamCherry.Project;

public interface ILayer
{
    public string? Name { get; set; }
    public int LayerWidth { get; set; }  // Should usually be same as MapWidth
    public int LayerHeight { get; set; }  // Should usually be same as MapHeight
    public int[]? Data { get; set; }
    public string? Type { get; set; }
    public bool Visible { get; set; }
    public int Id { get; set; }

}