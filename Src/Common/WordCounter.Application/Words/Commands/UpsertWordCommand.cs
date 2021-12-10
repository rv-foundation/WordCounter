using MapsterMapper;
using System.Threading;
using System.Threading.Tasks;
using WordCounter.Application.Common.Interfaces;
using WordCounter.Application.Common.Models;
using WordCounter.Application.Dto;
using WordCounter.Domain.Entities;
using System.Linq;

namespace WordCounter.Application.Words.Commands
{
    public class UpsertWordCommand : IRequestWrapper<WordDto>
    {
        public string Name { get; set; }
        public int Count { get; set; }
    }

    public class UpsertWordCommandHandler : IRequestHandlerWrapper<UpsertWordCommand, WordDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpsertWordCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<WordDto>> Handle(UpsertWordCommand request, CancellationToken cancellationToken)
        {
            var word = _context.Words.FirstOrDefault(x => x.Name == request.Name);
            if(word != null)
            {
                word.Count += request.Count;
            }
            else
            {
                word = new Word
                {
                    Name = request.Name,
                    Count = request.Count
                };

                await _context.Words.AddAsync(word, cancellationToken);
            }

            await _context.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(_mapper.Map<WordDto>(word));
        }
    }
}
