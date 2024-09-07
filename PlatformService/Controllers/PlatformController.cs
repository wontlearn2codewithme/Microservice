using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Contracts.Application;
using PlatformService.Contracts.Models;

namespace PlatformService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PlatformController : ControllerBase
    {
        private readonly IPlatformManagement _platformManagement;

        public PlatformController(IPlatformManagement platformManagement)
        {
            _platformManagement = platformManagement;
        }

        [HttpGet(Name = nameof(GetAllPlatforms))]
        public ActionResult<IEnumerable<PlatformReadResponse>> GetAllPlatforms()
        {
            var platforms = _platformManagement.GetAllPlatforms();
            return platforms.Any() ? Ok(platforms) : NotFound(platforms);
        }

        [HttpPost(Name = nameof(CreatePlatform))]
        public async Task<ActionResult<PlatformReadResponse>> CreatePlatform([FromBody] PlatformCreateRequest PlatformCreateRequest)
        {
            try
            {
                var createdPlatform = await _platformManagement.CreatePlatform(PlatformCreateRequest);
                return CreatedAtRoute(nameof(GetPlatformByUlid), new { ulid = createdPlatform.Ulid }, createdPlatform);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{platformId}", Name = nameof(GetPlatformById))]
        public ActionResult<PlatformReadResponse> GetPlatformById([FromRoute] int platformId) 
        {
            try
            {
                var platform = _platformManagement.GetPlatformById(platformId);
                return Ok(platform);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.ToString());
            }
            
        }

        [HttpGet("{ulid}", Name = nameof(GetPlatformByUlid))]
        public ActionResult<PlatformReadResponse> GetPlatformByUlid([FromRoute] string ulid)
        {
            try
            {
                if (Ulid.TryParse(ulid, out Ulid parsedUlid))
                {
                    var platform = _platformManagement.GetPlatformByGuid(parsedUlid);
                    return Ok(platform);
                }
                return BadRequest("Invalid Guid");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
    }
}
