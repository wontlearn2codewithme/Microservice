﻿using Microsoft.AspNetCore.Http;
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
        public ActionResult<List<PlatformReadResponse>> GetAllPlatforms()
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
                return CreatedAtRoute(nameof(GetPlatformByGuid), new { guid = createdPlatform.Guid }, createdPlatform);
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
                if (platform == null)
                {
                    return NotFound(platformId);
                }
                return Ok(platform);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.ToString());
            }
            
        }

        [HttpGet("{guid}", Name = nameof(GetPlatformByGuid))]
        public ActionResult<PlatformReadResponse> GetPlatformByGuid([FromRoute] Guid guid)
        {
            try
            {
                var platform = _platformManagement.GetPlatformByGuid(guid);
                if (platform == null) 
                {
                    return NotFound(guid);
                }
                return Ok(platform);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
    }
}
