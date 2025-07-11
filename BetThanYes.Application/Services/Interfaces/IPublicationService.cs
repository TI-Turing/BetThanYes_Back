using BetThanYes.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetThanYes.Domain.DTOs.Request.Publication;
namespace BetThanYes.Application.Services.Interfaces
{
    public interface IPublicationService
    {
        Task<Guid> AddAsync(CreatePublicationDto request);
        Task<List<Domain.Models.Publication>> GetAsync();
        Task<Domain.Models.Publication?> GetByIdAsync(Guid id);
        Task<bool>UpdateAsync(UpdatePublicationDto request);
    }
}
