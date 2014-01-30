using System;
using GenericProject.Core.Data.EntityFramework;

namespace GenericProject.Core.Model
{
    public abstract class ModelBase
    {
        public long Id { get; set; }

        public virtual void Delete<TModel>(TModel model) where TModel: ModelBase { EntityFrameworkContext.Instance.Set(model.GetType()).Remove(model); }
    }
}