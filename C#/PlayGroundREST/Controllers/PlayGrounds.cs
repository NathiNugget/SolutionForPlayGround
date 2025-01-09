using ExampleExam;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PlayGroundREST.Models;

namespace PlayGroundREST.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("defPolicy")]
    public class PlayGrounds : ControllerBase
    {
        private PlayGroundRepository _repo;

        public PlayGrounds(PlayGroundRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Get()
        {

            return _repo.GetAll().Count == 0 ? NoContent() : Ok(_repo.GetAll());
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            PlayGround pgr = _repo.GetById(id);
            if (pgr == null)
            {
                return NotFound(id);
            }
            return Ok(pgr);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult POST([FromBody] PlayGroundDTO dto)
        {
            try
            {
                PlayGround pgr = new PlayGround(dto.id, dto.name, dto.maxchildren, dto.minage);
                pgr = _repo.Add(pgr);
                return Created($"Id/{pgr.Id}", pgr);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Update(int id, [FromBody] PlayGroundDTO dto)
        {
            try
            {
                PlayGround pgr = _repo.GetById(id);
                Console.WriteLine(pgr);
                PlayGround newPgr = new PlayGround(id, dto.name, dto.maxchildren, dto.minage);
                if (_repo.GetById(id) == null)
                {
                    return NotFound(id);
                }
                var newpgr = _repo.Update(id, newPgr);

                return Ok(newpgr);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }








    }
}
