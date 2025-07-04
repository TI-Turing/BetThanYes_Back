using BetThanYes.Domain.DTOs.Request.Publication;
using BetThanYes.Infrastructure.Services.Publication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetThanYes.Application.Services
{
    public class PublicationService : Interfaces.IPublicationService

    {
        private readonly IPublicationRepository _publicationRepository;
        public PublicationService(IPublicationRepository publicationRepository)
        {
            _publicationRepository = publicationRepository;
        }
        public Task<Guid> AddAsync(CreatePublicationDto objPublication)
        {
            return _publicationRepository.AddAsync(objPublication);
        }

        public Task<List<Domain.Models.Publication>> GetAsync()
        {
            return _publicationRepository.GetAsync();
        }
        public async Task<Domain.Models.Publication?> GetByIdAsync(Guid id)
        {
            return await _publicationRepository.GetByIdAsync(id);
        }
        public Task<bool> UpdateAsync(UpdatePublicationDto request)
        {
            return _publicationRepository.UpdateAsync(request);
        }
    }
}
