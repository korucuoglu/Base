using Base.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Base.Api.Persistence.EntityConfigurations;

public class NoteEntityConfiguration : BaseEntityConfiguration<Note>
{
    public override void Configure(EntityTypeBuilder<Note> builder)
    {
        base.Configure(builder);
        builder.ToTable("notes");
        builder.Property(x => x.Title).HasColumnName("title").IsRequired();
        builder.Property(x => x.IsPublic).HasColumnName("is_public").IsRequired();
        builder.Property(x => x.Content).HasColumnName("content").IsRequired();
        builder.HasIndex(x => new { x.ApplicationUserId, x.Title }).IsUnique(true);

        builder.HasOne(x => x.Category).WithMany(x => x.Notes).HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.NoAction);
    }
}