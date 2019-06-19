using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ImageStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        
        [HttpGet]
        [Route("{name}")]
        public IActionResult Get(string name)
        {
            try
            {
                var path = GetFolderPath(name);

                if (!System.IO.File.Exists(path))
                {
                    return NotFound();
                }

                var stream = new FileStream(path, FileMode.Open);

                return new FileStreamResult(stream, "image/jpeg");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpPost]
        [Route("{name}")]
        public IActionResult Post(string name)
        {
            try
            {
                var path = GetFolderPath(name);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    Request.Body.CopyTo(stream);
                }
                
                return StatusCode(StatusCodes.Status201Created, Request.Path);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpDelete]
        [Route("{name}")]
        public IActionResult Delete(string name)
        {
            try
            {
                var path = GetFolderPath(name);

                if (!System.IO.File.Exists(path))
                {
                    return NotFound();
                }

                System.IO.File.Delete(path);

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        private static string GetFolderPath(string filename)
        {
            return Path.Combine(Directory.GetCurrentDirectory(), "Images", filename);
        }

    }
}