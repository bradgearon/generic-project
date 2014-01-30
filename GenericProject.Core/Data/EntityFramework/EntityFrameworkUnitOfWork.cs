using System.Data.Entity;
using GenericProject.Data;

namespace GenericProject.Core.Data.EntityFramework
{
    public class EntityFrameworkUnitOfWork : IUnitOfWork
    {
        public EntityFrameworkUnitOfWork(DbContext container) { Container = container; }

        public DbContext Container { get; private set; }

        public bool UseSerializableEntities { get { return !Container.Configuration.ProxyCreationEnabled; } set { Container.Configuration.ProxyCreationEnabled = !value; } }

        public void Dispose() { }

        public void SubmitChanges() { Container.SaveChanges(); }

        internal DbSet<TEntity> CreateDbSet<TEntity>() where TEntity: class { return Container.Set<TEntity>(); }
    }
}