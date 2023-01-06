using Microsoft.AspNetCore.Mvc;
using SecretsSharing.Interface;

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
    }
}