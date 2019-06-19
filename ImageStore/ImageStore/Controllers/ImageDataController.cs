using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VisionCommon.Models;

namespace ImageStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageDataController : ControllerBase
    {
        //private readonly IDbClient _db;
        private readonly IBusinessLogic _bll;
        public ImageDataController(IBusinessLogic bl)
        {
            _bll = bl;
            //_db = new MySqlDbClient(dbs.Value);
        }


        [HttpPost]
        [Route("AddImage")]
        public async Task<IActionResult> AddImage([FromBody] string s)
        {
            try
            {
                var image = JsonConvert.DeserializeObject<Image>(s);
                await _bll.AddImageAsync(image);
                return StatusCode(StatusCodes.Status202Accepted);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }

        [HttpPost]
        [Route("AddContent")]
        public async Task<IActionResult> AddContent([FromBody] string s)
        {
            try
            {
                var content = JsonConvert.DeserializeObject<ImageContent>(s);
                await _bll.AddImageContentAsync(content);
                return StatusCode(StatusCodes.Status202Accepted);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        [Route("Delete/{name}")]
        public async Task<IActionResult> DeleteImageData(string name)
        {
            try
            {
                await _bll.DeleteImageData(name);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}