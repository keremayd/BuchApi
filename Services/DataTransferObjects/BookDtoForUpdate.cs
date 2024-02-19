namespace Entities.DataTransferObjects;

public record BookDtoForUpdate
{
    // tanımlandığı yerde set edilecek anlamına gelir init
    public int Id { get; init; }
    public string Title { get; init; }
    public decimal Price { get; init; }
}