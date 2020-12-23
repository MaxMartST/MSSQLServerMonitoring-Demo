using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MSSQLServerMonitoring.Domain.UserModel;

namespace MSSQLServerMonitoring.Infrastructure.Data.UserModel.EntityConfigurations
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure( EntityTypeBuilder<User> builder )
        {
            builder.HasKey( x => x.Id );
            builder.HasAlternateKey( x => x.Username );
            
            builder.Property( x => x.Id ).ForSqlServerUseSequenceHiLo();

            builder.Property( x => x.Username ).HasMaxLength( 255 ).IsRequired();
            
            IMutableNavigation assignedRolesNavigation =
                builder.Metadata.FindNavigation( nameof( User.AssignedGroups ) );
            assignedRolesNavigation.SetPropertyAccessMode( PropertyAccessMode.Field );
        }
    }
}
