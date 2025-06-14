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
        Task<Publication> CreateAsync(CreatePublicationDto request);
    }
}
