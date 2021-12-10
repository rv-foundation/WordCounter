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
        /// Create city
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
