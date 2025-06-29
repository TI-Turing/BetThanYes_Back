using BetThanYes.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BetThanYes.Domain.DTOs.Request.Publication;
namespace BetThanYes.Infrastructure.Services.Publication
{
    public interface IPublicationRepository
    {
        Task<Guid> CreateAsync(CreatePublicationDto objPublication);
        
    }
} 
