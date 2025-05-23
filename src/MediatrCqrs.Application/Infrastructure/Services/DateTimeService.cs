using MediatrCqrs.Application.Common.Interfaces;

namespace MediatrCqrs.Application.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}