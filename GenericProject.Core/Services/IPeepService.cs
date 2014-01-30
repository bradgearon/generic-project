using System.Collections.Generic;
using GenericProject.Core.Model;
using GenericProject.Core.Models;

namespace GenericProject.Core.Services
{
    public interface IPeepService
    {
        IEnumerable<Peep> GetPeeps();
        PagedEnumerable<Peep> GetPeeps(PaginationRequest paginationRequest);

        Peep GetPeep(long peepId);
    }
}