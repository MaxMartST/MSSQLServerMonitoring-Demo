using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MSSQLServerMonitoring.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSSQLServerMonitoring.Infrastructure.Data.QueryModel.EntityConfigurations
{
    public class QueryMap : IEntityTypeConfiguration<Query>
    {
        public void Configure(EntityTypeBuilder<Query> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ForSqlServerUseSequenceHiLo();

        }
    }
}
