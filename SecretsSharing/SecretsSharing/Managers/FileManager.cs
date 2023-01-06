using SecretsSharing.Interface;

namespace SecretsSharing.Managers
{
    public class FileManager : IFileManager
    {
        private IFileRepository _fileRepository;

        public FileManager(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }
    }
}