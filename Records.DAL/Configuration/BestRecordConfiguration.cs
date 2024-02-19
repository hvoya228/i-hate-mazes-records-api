using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Records.Data.Models;

namespace Records.DAL.Configuration;

public class BestRecordConfiguration : IEntityTypeConfiguration<BestRecord>
{
    public void Configure(EntityTypeBuilder<BestRecord> builder)
    {
        builder.HasKey(m => m.Id);
        
        builder
            .Property(m => m.TotalScore)
            .IsRequired();

        builder
            .Property(m => m.PinkScore)
            .IsRequired();

        builder
            .Property(m => m.GreenScore)
            .IsRequired();
        
        builder
            .HasOne(m => m.Player)
            .WithOne(o => o.BestRecord)
            .HasForeignKey<BestRecord>(m => m.PlayerId);
    }
}