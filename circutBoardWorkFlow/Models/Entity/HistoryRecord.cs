using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace circutBoardWorkFlow.Models.Entity
{
    [EntityTypeConfiguration(typeof(HistoryRecordConfiguration))]
    public class HistoryRecord: Entity<long>
    {
        public Status OldStatus { get; set; }
        public Status NewStatus { get; set; }
        public DateTimeOffset Updated { get; set; }

        public long BoardId { get; set; }
        public required CircuitBoard Board { get; set; }
    }

    internal class HistoryRecordConfiguration : IEntityTypeConfiguration<HistoryRecord>
    {
        public void Configure(EntityTypeBuilder<HistoryRecord> builder)
        {
            builder.Property<long>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("bigint");

            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(builder.Property<long>("Id"));

            builder.Property<long>("BoardId")
                .HasColumnType("bigint");

            builder.Property<Status>("NewStatus")
                .HasConversion(new EnumToStringConverter<Status>())
                .HasColumnType("text");

            builder.Property<Status>("OldStatus")
                .HasConversion(new EnumToStringConverter<Status>())
                .HasColumnType("text");

            builder.Property<DateTimeOffset>("Updated")
                .HasColumnType("timestamp with time zone");

            builder.HasKey("Id");

            builder.HasIndex("BoardId");

            builder.ToTable("HistoryRecords");
        }
    }
}
