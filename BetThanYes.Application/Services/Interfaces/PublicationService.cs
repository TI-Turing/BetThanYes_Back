using BetThanYes.Application.Services.Interfaces;
using BetThanYes.Domain.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetThanYes.Domain.DTOs.Request.Publication;
namespace BetThanYes.Application.Services
{
    public class PublicationService : IPublicationService
    {
        private readonly IPublicationRepository _publicationRepository;
        public PublicationService(IPublicationRepository publicationRepository) //Constructor
        {
            _publicationRepository = publicationRepository;
        }

        public async Task<Publication> CreateAsync(CreatePublicationDto request)
        {
            var objResult = await _publicationRepository.CreateAsync(request);

            return objResult;
        }
    }
}
