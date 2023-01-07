using System;
using System.Collections.Generic;
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

        /// <summary>
        /// Upload file on server and save data in database
        /// </summary>
        /// <param name="model">Upload model</param>
        /// <param name="file">user file</param>
        /// <returns>File id</returns>
        public async Task<Guid> UploadFileAsync(UploadFileModel model, IFormFile file)
        {
            var fileEntity = _mapper.Map<File>(model);
            fileEntity.FileName = $"{Guid.NewGuid()}.{file.FileName.Split('.').Last()}";
            var path = $".\\Files\\{fileEntity.FileName}";
            fileEntity.Path = path;
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }
    
            var id = await _fileRepository.AddAsync(fileEntity);
            return id;
        }
        
        /// <summary>
        /// Check file.IsDelete and delete file if is needed
        /// </summary>
        /// <param name="file"></param>
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

        public List<File> GetAllForUser(Guid userId) => _fileRepository.GetAllForUser(userId);
    }
}