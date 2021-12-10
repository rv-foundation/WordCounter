using System;
using WordCounter.Application.Common.Interfaces;

namespace WordCounter.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
