using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using WordCounter.Application.Common.Extensions;
using WordCounter.Application.Common.Interfaces;
using WordCounter.Application.Common.Models;
using WordCounter.Application.Dto;

namespace WordCounter.Application.Words.Queries
{
    public class GetWordsWithFrequencyByUrlQuery : IRequestWrapper<List<WordDto>>
    {
        public string Url { get; set; }
    }

    public class GetWordsWithFrequencyByUrlQueryHandler : IRequestHandlerWrapper<GetWordsWithFrequencyByUrlQuery, List<WordDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetWordsWithFrequencyByUrlQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<List<WordDto>>> Handle(GetWordsWithFrequencyByUrlQuery request, CancellationToken cancellationToken)
        {
            WebClient webClient = new WebClient();
            byte[] reqHTML = webClient.DownloadData(request.Url.DecodeUrlString());

            UTF8Encoding objUTF8 = new UTF8Encoding();
            string pageContentWithHtmlTags = objUTF8.GetString(reqHTML);
            string pageContent = Regex.Replace(pageContentWithHtmlTags, "<.*?>|&.*?;", string.Empty);

            List<WordDto> list = null;
            if (!string.IsNullOrEmpty(pageContent))
            {
                list = CountWordFrequency(pageContent);
            }

            return list != null ? ServiceResult.Success(list) : ServiceResult.Failed<List<WordDto>>(ServiceError.NotFound);
        }

        public List<WordDto> CountWordFrequency(string content)
        {
            Dictionary<string, int> wordCounts = new Dictionary<string, int>();
            string pattern = @"[^a-zA-Z0-9'\- ]";

            string cleanedLine = Regex.Replace(content, pattern, string.Empty).ToLowerInvariant();
            string[] words = cleanedLine.Split(' ');
            foreach (string word in words)
            {
                if (!string.IsNullOrEmpty(word))
                {
                    int frequency = 1;
                    if (wordCounts.ContainsKey(word))
                    {
                        frequency = wordCounts[word] + 1;
                    }
                    wordCounts[word] = frequency;
                }
            }

            List<KeyValuePair<string, int>> pairList = new List<KeyValuePair<string, int>>(wordCounts);

            pairList.Sort((first, second) => { return second.Value.CompareTo(first.Value); });

            List<WordDto> result = new List<WordDto>();
            foreach (KeyValuePair<string, int> pair in pairList)
            {
                result.Add(new WordDto { Name = pair.Key, Count = pair.Value });
            }

            return result;
        }
    }
}
