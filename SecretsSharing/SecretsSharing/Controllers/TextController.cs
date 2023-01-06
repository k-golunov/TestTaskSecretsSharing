using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SecretsSharing.Interface;
using SecretsSharing.Model;

namespace SecretsSharing.Controllers
{        
    [ApiController]
    [Route("api/v1/text")]
    public class TextController : ControllerBase
    {

        private readonly ITextManager _textManager;

        public TextController(ITextManager textManager)
        {
            _textManager = textManager;
        }


        [HttpPost("upload")]
        public async Task<IActionResult> UploadText(TextModel model)
        {
            await _textManager.UploadText(model);
            return Ok();
        }
    }
    
}