using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WordCounter.Application.Common.Models;
using WordCounter.Application.Dto;
using WordCounter.Application.Words.Queries;

namespace WordCounter.Api.Controllers
{
    [ApiController]
    public class WordsController : BaseApiController
    {
        /// <summary>
        /// Create city
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        [HttpGet("{url}")]
        public async Task<ActionResult<ServiceResult<WordDto>>> GetWordsWithFrequencyByUrl(string url)
        {
            return Ok(await Mediator.Send(new GetWordsWithFrequencyByUrlQuery { Url = url }));
        }
    }
}
