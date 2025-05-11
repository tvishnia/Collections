using System;

namespace CollectionBasic.Domain;

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


    
}
