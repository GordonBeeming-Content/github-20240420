namespace SampleApp.Database.Configurations;

public sealed class ContactConfiguration : IEntityTypeConfiguration<Contact>
{
  public void Configure(EntityTypeBuilder<Contact> builder)
  {
    builder.ToTable(nameof(Contact), o => o.IsTemporal());
    builder.HasKey(e => e.Id);
    builder.Property(e => e.Id)
        .ValueGeneratedOnAdd()
        .UseIdentityColumn();

    builder.Property(e => e.FullName)
        .IsRequired()
        .HasMaxLength(256);
    builder.Property(e => e.Email)
        .IsRequired()
        .HasMaxLength(256);

    builder.Property(e => e.CreatedDate)
        .IsRequired()
        .HasColumnType("DATETIME2(7)")
        .HasDefaultValueSql("SYSUTCDATETIME()")
        .ValueGeneratedOnAdd()
        .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
    builder.Property(e => e.ModifiedDate)
        .IsRequired()
        .HasColumnType("DATETIME2(7)")
        .HasDefaultValueSql("SYSUTCDATETIME()")
        .ValueGeneratedOnAdd()
        .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Save);
  }
}
