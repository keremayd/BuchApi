namespace Services.DataTransferObjects.Response;

public record BookDtoResponse
{
    public int Id { get; set; }
    public string Title { get; set; }
    public decimal Price { get; set; }
}