using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.EFCore.Config;

public class BookConfig : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasData(
            new Book { Id = 1, Title = "Hacivat ve Karagöz", Price = 15 },
            new Book { Id = 2, Title = "Olasılıksız", Price = 20 },
            new Book { Id = 3, Title = "Mesnevi", Price = 30 }
        );
    }
}