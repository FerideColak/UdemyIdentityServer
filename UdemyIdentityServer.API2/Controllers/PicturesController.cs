using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UdemyIdentityServer.API2.Models;

namespace UdemyIdentityServer.API2.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PicturesController : ControllerBase
    {
        [Authorize(Policy ="ReadPicture")]
        [HttpGet]
        public IActionResult GetPictures()
        {
            var pictureList = new List<Picture>()
            {
                new Picture() {ID = 1, Name = "dogaresmi", Url = "doga.jpeg"},
                new Picture() {ID = 2, Name = "golresmi", Url = "gol.jpeg"},
                new Picture() {ID = 3, Name = "kayikresmi", Url = "kayik.jpeg"},
                new Picture() {ID = 4, Name = "ucurtmaresmi", Url = "ucurtma.jpeg"},
                new Picture() {ID = 5, Name = "agacresmi", Url = "agac.jpeg"}
            };
            return Ok(pictureList);
        }

        [Authorize(Policy ="UpdateOrCreate")]
        //[HttpPut]
        public IActionResult UpdatePicture(int id)
        {
            return Ok($"id'si {id} olan resim güncellendi");
        }

        [Authorize(Policy ="UpdateOrCreate")]
        //[HttpPost]
        public IActionResult CreatePicture(Picture picture)
        {
            return Ok(picture);
        }
    }
}
