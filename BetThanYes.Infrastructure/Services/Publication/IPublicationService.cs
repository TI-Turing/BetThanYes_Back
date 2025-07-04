using BetThanYes.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BetThanYes.Domain.DTOs.Request.Publication;
namespace BetThanYes.Infrastructure.Services.Publication
{
    public interface IPublicationRepository
    {
        Task<Guid> AddAsync(CreatePublicationDto objPublication);// Este método agrega una nueva publicación a la base de datos y devuelve el ID de la nueva publicación
        Task<List<Domain.Models.Publication>> GetAsync();// Este método obtiene todas las publicaciones y devuelve una lista de objetos Publication
        Task<Domain.Models.Publication?> GetByIdAsync(Guid id); // Este método obtiene una publicación por su ID y puede devolver null si no se encuentra la publicación
        Task<bool> UpdateAsync(UpdatePublicationDto request);// Este método actualiza una publicación existente en la base de datos
    }
} 
