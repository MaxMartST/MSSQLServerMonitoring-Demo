using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MSSQLServerMonitoring.Domain.AlertModel;

namespace MSSQLServerMonitoring.Infrastructure.Data.AlertModel.EntityConfigurations
{
    public class AlertMap : IEntityTypeConfiguration<Alert>
    {
        public void Configure(EntityTypeBuilder<Alert> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ForSqlServerUseSequenceHiLo();

        }
    }
}
