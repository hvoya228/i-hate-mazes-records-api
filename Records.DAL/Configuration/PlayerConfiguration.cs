using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Records.Data.Models;

namespace Records.DAL.Configuration;

public class PlayerConfiguration : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        builder.HasKey(p => p.Id);
        
        builder
            .Property(p => p.Name)
            .IsRequired();
        
        builder
            .HasOne(p => p.BestRecord)
            .WithOne(o => o.Player)
            .HasForeignKey<Player>(p => p.BestRecordId);
    }
}