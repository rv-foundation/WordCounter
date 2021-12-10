using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using WordCounter.Application.Common.Models;
using WordCounter.Application.Dto;
using WordCounter.Application.Words.Queries;

namespace WordCounter.Api.Controllers
{
    [Authorize]
    [ApiController]
    public class WordsController : BaseApiController
    {

        /// <summary>
        /// Get Top 100 Most Frequent Words
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<ServiceResult<WordDto>>> GetMostFrequentWords(CancellationToken cancellationToken) {
            //Cancellation token example.
            return Ok(await Mediator.Send(new GetMostFrequentWordsQuery(), cancellationToken));
        }

        /// <summary>
        /// Fetch Words from URL
        /// </summary>
        /// <param name="url"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{url}")]
        public async Task<ActionResult<ServiceResult<WordDto>>> GetWordsWithFrequencyByUrl(string url, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetWordsWithFrequencyByUrlQuery { Url = url }, cancellationToken));
        }
    }
}
