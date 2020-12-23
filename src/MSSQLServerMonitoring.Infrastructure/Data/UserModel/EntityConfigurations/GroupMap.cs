using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MSSQLServerMonitoring.Domain.UserModel;

namespace MSSQLServerMonitoring.Infrastructure.Data.UserModel.EntityConfigurations
{
    public class GroupMap : IEntityTypeConfiguration<Group>
    {
        public void Configure( EntityTypeBuilder<Group> builder )
        {
            builder.HasKey( x => x.Id );
            builder.HasAlternateKey( x => x.Name );
            
            builder.Property( x => x.Id ).ForSqlServerUseSequenceHiLo();

            builder.Property( x => x.Name ).HasMaxLength( 255 ).IsRequired();
        }
    }
}
