using AdviceCompliance.Application.Common.Interfaces;

namespace AdviceCompliance.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
