using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using WordCounter.Domain.Entities;

namespace WordCounter.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Word> Words { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
