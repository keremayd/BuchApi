using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects;

public record BookDtoForUpdate : BookDtoForManipulation
{
    // tanımlandığı yerde set edilecek anlamına gelir init
    [Required]
    public int Id { get; init; }
}