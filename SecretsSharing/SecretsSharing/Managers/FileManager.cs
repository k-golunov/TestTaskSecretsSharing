using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using SecretsSharing.Interface;
using SecretsSharing.Model;
using File = SecretsSharing.DTO.File;

namespace SecretsSharing.Managers
{
    public class FileManager : IFileManager
    {
        private readonly IFileRepository _fileRepository;
        private readonly IMapper _mapper;
        

        public FileManager(IFileRepository fileRepository, IMapper mapper)
        {
            _fileRepository = fileRepository;
            _mapper = mapper;
        }

        public async Task<Guid> UploadFileAsync(UploadFileModel model, IFormFile file)
        {
            var fileEntity = _mapper.Map<File>(model);
            fileEntity.FileName = $"{Guid.NewGuid()}.{file.FileName.Split('.').Last()}";
            var path = $".\\Files\\{fileEntity.FileName}.{file.FileName.Split('.').Last()}";
            fileEntity.Path = path;
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }
    
            var id = await _fileRepository.AddAsync(fileEntity);
            return id;
        }
        
        public void DeleteAutomatically(File file)
        {
            if (file.IsDelete)
            {
                _fileRepository.DeleteFile(file);
                System.IO.File.Delete(file.Path);
            }
        }

        public File GetFile(Guid id) => _fileRepository.GetById(id);
        
        public bool DeleteFile(Guid id)
        {
            var file = _fileRepository.GetById(id);
            if (file is null)
                return false;
            _fileRepository.DeleteFile(file);
            System.IO.File.Delete(file.Path);
            return true;
        }
    }
}