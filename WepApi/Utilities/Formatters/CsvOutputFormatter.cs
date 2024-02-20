using System.Text;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using Services.DataTransferObjects.Response;

namespace WebApi.Utilities.Formatters;

public class CsvOutputFormatter : TextOutputFormatter
{
    public CsvOutputFormatter()
    {
        // Gerekli konfigrasyonlar
        SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
        SupportedEncodings.Add(Encoding.UTF8);
        SupportedEncodings.Add(Encoding.Unicode);
    }

    protected override bool CanWriteType(Type? type)
    {
        // Gelen tip BookDtoResponse veya IEnumerator<BookDtoResponse> ise bunu cvs formatında döndürebilirizin onayını verdik
        if(typeof(BookDtoResponse).IsAssignableFrom(type) || typeof(IEnumerable<BookDtoResponse>).IsAssignableFrom(type))
        {
            return base.CanWriteType(type);
        }
        return false;
    }

    private static void FormatCsv(StringBuilder buffer, BookDtoResponse book)
    {
        // Dönüşümü bu methodda yapıyoruz
        buffer.AppendLine($"{book.Id}, {book.Title}, {book.Price}");
    }
    
    public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
    {
        var response = context.HttpContext.Response;
        var buffer = new StringBuilder();

        if(context.Object is IEnumerable<BookDtoResponse>)
        {
            foreach(var book in (IEnumerable<BookDtoResponse>)context.Object)
            {
                FormatCsv(buffer, book);
            }
        }
        else
        {
            FormatCsv(buffer, (BookDtoResponse)context.Object);
        }
        await response.WriteAsync(buffer.ToString());
    }
}