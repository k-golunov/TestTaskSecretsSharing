using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using SecretsSharing.DTO;
using SecretsSharing.Interface;
using SecretsSharing.Model;

namespace SecretsSharing.Managers
{
    public class TextManager : ITextManager
    {
        private readonly ITextRepository _textRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        
        public TextManager(ITextRepository textRepository, IConfiguration configuration, IMapper mapper)
        {
            _textRepository = textRepository;
            _configuration = configuration;
            _mapper = mapper;
        }

        public Task<Guid> UploadText(TextModel model)
        {
            var text = _mapper.Map<UserText>(model);
            return _textRepository.AddAsync(text);
        }

        public UserText GetText(Guid id) => _textRepository.GetById(id);
        
        public void DeleteText(Guid id)
        {
            _textRepository.Delete(id);
        }
        
        public List<UserText> GetAllForUser(Guid userId) => _textRepository.GetAllForUser(userId);
    }
}