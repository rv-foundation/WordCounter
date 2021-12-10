using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WordCounter.Application.Common.Interfaces;
using WordCounter.Application.Common.Models;
using WordCounter.Application.Dto;

namespace WordCounter.Application.Words.Queries
{
    public class GetMostFrequentWordsQuery : IRequestWrapper<List<WordDto>>
    {

    }

    public class GetMostFrequentWordsQueryHandler : IRequestHandlerWrapper<GetMostFrequentWordsQuery, List<WordDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetMostFrequentWordsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<List<WordDto>>> Handle(GetMostFrequentWordsQuery request, CancellationToken cancellationToken) {
            List<WordDto> list = await _context.Words
                .OrderByDescending(x=> x.Count)
                .Take(100)
                .ProjectToType<WordDto>(_mapper.Config)
                .ToListAsync(cancellationToken);

            return list.Count > 0 ? ServiceResult.Success(list) : ServiceResult.Failed<List<WordDto>>(ServiceError.NotFound);
        }
    }
}
