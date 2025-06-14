using BetThanYes.Application.Services.Interfaces;
using BetThanYes.Domain.Models;
using BetThanYes.Domain.DTOs.Request.Publication;
using BetThanYes.Infrastructure.Services.Publication;

namespace BetThanYes.Application.Services
{
    public class PublicationService : IPublicationService
    {
        private readonly IPublicationRepository _publicationRepository;
        public PublicationService(IPublicationRepository publicationRepository) //Constructor
        {
            _publicationRepository = publicationRepository;
        }

        public async Task<Guid> CreateAsync(CreatePublicationDto request)
        {
            var objResult = await _publicationRepository.CreateAsync(request);

            return objResult;
        }
    }
}
