using SQLite;

namespace Discovery.Models;

[Table("Photos")]
public sealed class PhotoEntity
{
    [Column("pk_photo")]
    [PrimaryKey]
    public int Id { get; set; }

    [Column("photo_alt")]
    [NotNull]
    public string Alt { get; set; } = default!;

    [Column("photo_url")]
    [NotNull]
    public string Url { get; set; } = default!;

    [Column("photographer")]
    [NotNull]
    public string Photographer { get; set; } = default!;
}
