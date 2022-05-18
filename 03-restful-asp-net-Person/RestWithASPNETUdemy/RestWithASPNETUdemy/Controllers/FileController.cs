using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestWithASPNETUdemy.Business;
using RestWithASPNETUdemy.Data.VO;

namespace RestWithASPNETUdemy.Controllers
{
    [ApiVersion("1")]
    [Authorize("Bearer")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class FileController : Controller
    {
        private readonly IFileBusiness _fileBusiness;

        public FileController(IFileBusiness fileBusiness)
        {
            _fileBusiness = fileBusiness;
        }

        [HttpPost("uploadFile")]
        [ProducesResponseType((200), Type = typeof(FileDatailsVO))]
        [ProducesResponseType((400))]
        [ProducesResponseType((401))]
        [Produces("application/json")]
        public async Task<IActionResult> UploadOnFile([FromForm] IFormFile file)
        {
            FileDatailsVO detail = await _fileBusiness.SaveFileToDisk(file);

            return new OkObjectResult(detail);

        }

    }
}
