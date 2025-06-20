using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace circutBoardWorkFlow.Models.Entity
{
    [EntityTypeConfiguration(typeof(CircuitBoardConfiguration))]
    public class CircuitBoard : Entity<long>
    {
        public string Name { get; set; } = string.Empty;
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Updated { get; set; }
        public Status Status { get; set; }
        public ICollection<HistoryRecord>? HistoryRecords { get; set; }
    }

    internal class CircuitBoardConfiguration : IEntityTypeConfiguration<CircuitBoard>
    {
        public void Configure(EntityTypeBuilder<CircuitBoard> builder)
        {
            builder.Property<long>("Id")
                       .ValueGeneratedOnAdd()
                       .HasColumnType("bigint");

            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(builder.Property<long>("Id"));

            builder.Property<DateTimeOffset>("Created")
                .HasColumnType("timestamp with time zone");

            builder.Property<string>("Name")
                .IsRequired()
                .HasColumnType("text");

            builder.Property<Status>("Status")
                .HasConversion(new EnumToStringConverter<Status>())
                .IsRequired()
                .HasColumnType("text");

            builder.Property<DateTimeOffset?>("Updated")
                .HasColumnType("timestamp with time zone");

            builder.Property<uint>("Version").IsRowVersion();

            builder.HasKey("Id");

            builder.ToTable("CircuitBoards");
        }
    }
}
