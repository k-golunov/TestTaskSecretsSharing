using System.Net.Mime;
using SecretsSharing.Interface;

namespace SecretsSharing.Managers
{
    public class TextManager : ITextManager
    {
        private ITextRepository _textRepository;

        public TextManager(ITextRepository textRepository)
        {
            _textRepository = textRepository;
        }
    }
}