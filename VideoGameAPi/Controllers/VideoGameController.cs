using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VideoGameAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoGameController : ControllerBase
    {
        static private List<VideoGame> videoGames = new List<VideoGame>// creaye a list of VideoGame type
        {
            new VideoGame
            {
                Id = 1,
                Title = "The Legend of Zelda: Breath of the Wild",
                Platform = "Nintendo Switch",
                Developer = "Nintendo",
                Publisher = "Nintendo"
            },
            new VideoGame
            {
                Id = 2,
                Title = "God of War: Ragnarok",
                Platform = "PlayStation 5",
                Developer = "Santa Monica Studio",
                Publisher = "Sony Interactive Entertainment"
            },
            new VideoGame
            {
                Id = 3,
                Title = "Halo Infinite",
                Platform = "Xbox Series X|S",
                Developer = "343 Industries",
                Publisher = "Xbox Game Studios"
            }

        };
        [HttpGet]//get is generally used for fetching data
        public ActionResult<List<VideoGame>> GetVideoGames()//ActionResult is used so that we can send the status codes too
        {
            return Ok(videoGames);//returns status code 200 along with in=tems in videoGames List
        }

        [HttpPost]//put generally used for adding data which involves creating new or updating the data
        [Route("{id}")]//requires an id of the videogame
        public ActionResult<VideoGame> GetVideoGameById(int id)
        {
            var game =videoGames.FirstOrDefault(g => g.Id == id);//g is a lambda funciton 
            if (game is null) return NotFound();//returns 404 status code
            return Ok(game);

        }
        //note HttpPatch can be used for updating certain section  of data

        [HttpPost]
        public ActionResult<VideoGame> AddVideoGame(VideoGame newGame)
        {
            if (newGame is null) return BadRequest();//returns 201 status code if there is no videoGame provided 
            newGame.Id = videoGames.Max(g => g.Id) + 1;
            videoGames.Add(newGame);
            return CreatedAtAction(nameof(GetVideoGameById),new { id=newGame.Id}, newGame);
        }

        [HttpPut]//for updating data
        [Route("{id}")]
        //[HttpPut("{id}")] is also another way 
        public ActionResult<VideoGame> UpdateVideoGame(int id,VideoGame updatedGame)
        {
            var game=videoGames.FirstOrDefault(game => game.Id == id);//FirstorDefault returns the first data that meets the certain codition
           if (game is null) return NotFound();

           //changing the previous value of the game of certain id with updated value
           game.Title = updatedGame.Title;
           game.Platform = updatedGame.Platform;
           game.Developer = updatedGame.Developer;
           game.Publisher= updatedGame.Publisher;

            return NoContent();
        }

        [HttpDelete("{id}")]

        public ActionResult<VideoGame> DeleteVideoGame(int id)
        {
            var game = videoGames.FirstOrDefault(g => g.Id == id);
            if (game is null) return NotFound();
            videoGames.Remove(game);//using remove method of List to remove the data from list
            return NoContent();
        }
       
    }
}
