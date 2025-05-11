namespace Collections.DB.Domain;

/// <summary>
/// PictureFileInfo
/// </summary>
public class PictureFileInfo
{
    /// <summary>
    /// Id
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// FileName
    /// </summary>
    public string FileName { get; init; } = string.Empty;

    // /// <summary>
    // /// File Description
    // /// </summary>
    // public string FileDescription { get; init; } = string.Empty;
}
