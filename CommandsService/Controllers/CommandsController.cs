using AutoMapper;
using CommandsService.Data;
using CommandsService.DTOs;
using CommandsService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
    [Route("api/platforms/{platformId}/[controller]/[action]")]
    [ApiController]
    public class CommandsController(ICommandRepository commandRepository, IMapper mapper) : ControllerBase
    {
        [HttpGet(Name = nameof(GetCommandsForPlatform))]
        public ActionResult<IEnumerable<CommandReadDTO>> GetCommandsForPlatform(int platformId)
        {
            Console.WriteLine("--> Hit GetCommandsForPlatform <--");
            if (!commandRepository.PlatformExists(platformId)) 
            {
                return BadRequest(platformId);
            }
            var result = commandRepository.GetCommandsForPlatform(platformId);
            if (result == null || !result.Any())
            {
                return NotFound();
            }
            return Ok(mapper.Map<IEnumerable<CommandReadDTO>>(result));
        }

        [HttpGet("{commandId}", Name = nameof(GetCommandForPlatform))]
        public ActionResult<CommandReadDTO> GetCommandForPlatform(int platformId, int commandId)
        {
            Console.WriteLine($"--> Hit GetCommandForPlatform -> platformId: {platformId} commandId: {commandId}");
            if (!commandRepository.PlatformExists(platformId))
            {
                return BadRequest(platformId);
            }
            var result = commandRepository.GetCommandForPlatform(platformId, commandId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<CommandReadDTO>(result));
        }

        [HttpPost]
        public ActionResult<CommandReadDTO> CreateCommandForPlatform(int platformId, CommandCreateDTO commandCreateDTO) 
        {
            Console.WriteLine($"--> Hit CreateCommandForPlatform -> platformId: {platformId}");
            if (!commandRepository.PlatformExists(platformId))
            {
                return BadRequest(platformId);
            }
            var commandToCreate = mapper.Map<Command>(commandCreateDTO);
            commandRepository.CreateCommand(platformId, commandToCreate);
            commandRepository.SaveChanges();
            var commandResponse = mapper.Map<CommandReadDTO>(commandToCreate);
            if (commandResponse == null) 
            {
                return BadRequest();
            }
            return CreatedAtRoute(nameof(GetCommandForPlatform), new { platformId = platformId, commandId = commandResponse.Id }, commandResponse);
        }
    }
}
