using System;
using System.Collections.Generic;
using System.Linq;
using GenericProject.Core.Model;
using GenericProject.Core.Models;
using GenericProject.Data;

namespace GenericProject.Core.Services
{
    public class PeepService : IPeepService
    {
        private readonly IRepositoryFactory<Peep> _peepRepositoryFactory;

        public PeepService(IRepositoryFactory<Peep> peepRepositoryFactory) { _peepRepositoryFactory = peepRepositoryFactory; }

        public PagedEnumerable<Peep> GetPeeps(PaginationRequest paginationRequest)
        {
            if (paginationRequest == null) paginationRequest = new PaginationRequest();
            var query = GetPeepRepo().AsQueryable();

            var filter = "";
            if (paginationRequest.FilterArgs.TryGetValue("Search", out filter) && !String.IsNullOrWhiteSpace(filter))
            {
                filter = filter.ToLower();
                query = query.Where(x => x.Name.ToLower().Contains(filter) || x.Email.Contains(filter) || x.Note.Contains(filter));
            }

            if (paginationRequest.FilterArgs.TryGetValue("ZipCode", out filter) && !String.IsNullOrEmpty(filter)) {
                filter = filter.ToLower().Trim();
                query = query.Where(x => x.ZipCode.ToLower().Equals(filter));
            }

            return new PagedEnumerable<Peep>(query.ApplyPaging(paginationRequest), paginationRequest, query.Count());
        }

        public IEnumerable<Peep> GetPeeps() { return GetPeepRepo().AsQueryable().OrderBy(x => x.Name).ToList(); }

        public Peep GetPeep(long peepId) { return GetPeepRepo().AsQueryable().FirstOrDefault(x => x.Id == peepId); }

        private IRepository<Peep> GetPeepRepo() { return _peepRepositoryFactory.GetRepository(); }
    }
}