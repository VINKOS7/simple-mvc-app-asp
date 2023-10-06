using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using WeatherForecast.Domain.Aggregates.Account;

namespace WeatherForecast.Infrastructure.EntityConfigures;

public class AccountEntityConfig : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder) => builder
        .OwnsMany(acc => acc.Devices);
}
