using HomPageServices.Features.HomePage.Command;
using HomPageServices.Features.HomePage.Queries;
using HomPageServices.Models.Dtos;
using HomPageServices.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Net;
using System.Text;
using System.Text.Json;

namespace HomPageServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomePageController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IDistributedCache distributedCache;
        public HomePageController(IMediator mediator, IDistributedCache distributedCache)
        {
            this.mediator = mediator;
            this.distributedCache = distributedCache;
        }

        [Authorize(Roles ="Admin")]

        [HttpPost]
        public IActionResult Post(HomePagePartDto homePagePartDto)
        {
            
            var result = mediator.Send(new AddHomePagePartItemsCommand(homePagePartDto)).Result;
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<PartGroupDto>),(int)HttpStatusCode.OK)]
        public IActionResult Get()
        {
            List<PartGroupDto> partGroupDtos = new List<PartGroupDto>();
            var chachedData = distributedCache.GetAsync("homePageData").Result;
            if(chachedData != null)
            {
                partGroupDtos = JsonSerializer.Deserialize<List<PartGroupDto>>(chachedData);
            }
            else
            {
                partGroupDtos = mediator.Send(new GetHomePagePartsItemsQuery()).Result;
                string JsonData = JsonSerializer.Serialize(partGroupDtos);
                byte[] encodedJson = Encoding.UTF8.GetBytes(JsonData);

                var options = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(60));
                distributedCache.SetAsync("homePageData", encodedJson, options);
            }

            
            return Ok(partGroupDtos);
        }
    }
}
