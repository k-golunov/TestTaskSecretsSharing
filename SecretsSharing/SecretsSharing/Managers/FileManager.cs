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
            var path = $"..\\Files\\File\\{Guid.NewGuid()}.{file.FileName.Split('.').Last()}";
            fileEntity.FileName = file.FileName;
            fileEntity.Path = path;
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }
    
            var id = await _fileRepository.AddAsync(fileEntity);
            return id;
        }
    }
}