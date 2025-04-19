using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VideoGameAPi.Data;

namespace VideoGameAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoGameController(VideoGameDbContext context) : ControllerBase
    {
        private readonly VideoGameDbContext _context=context;
                                    
        [HttpGet]//get is generally used for fetching data
        public  async Task<ActionResult<List<VideoGame>>> GetVideoGames()//ActionResult is used so that we can send the status codes too
        {
            return Ok(await _context.VideoGames.ToListAsync());//returns status code 200 along with in=tems in videoGames List
        }

        [HttpPost]//put generally used for adding data which involves creating new or updating the data
        [Route("{id}")]//requires an id of the videogame
        public async Task<ActionResult<VideoGame>> GetVideoGameById(int id)
        {
            var game = await _context.VideoGames.FindAsync(id);//g is a lambda funciton 
            if (game is null) return NotFound();//returns 404 status code
            return Ok(game);

        }
        ////note HttpPatch can be used for updating certain section  of data

        [HttpPost]
        public async Task<ActionResult<VideoGame>> AddVideoGame(VideoGame newGame)
        {
            if (newGame is null) return BadRequest();//returns 201 status code if there is no videoGame provided 
            _context.VideoGames.Add(newGame);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetVideoGameById), new { id = newGame.Id }, newGame);
        }

        [HttpPut]//for updating data
        [Route("{id}")]
        //[HttpPut("{id}")] is also another way 
        public async Task<IActionResult> UpdateVideoGame(int id, VideoGame updatedGame)
        {
            var game =await  _context.VideoGames.FindAsync(id);//FirstorDefault returns the first data that meets the certain codition
            if (game is null) return NotFound();

            //changing the previous value of the game of certain id with updated value
            game.Title = updatedGame.Title;
            game.Platform = updatedGame.Platform;
            game.Developer = updatedGame.Developer;
            game.Publisher = updatedGame.Publisher;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<VideoGame>> DeleteVideoGame(int id)
        {
            var game = await _context.VideoGames.FindAsync(id);
            if (game is null) return NotFound();
            _context.VideoGames.Remove(game);//using remove method of List to remove the data from list
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
