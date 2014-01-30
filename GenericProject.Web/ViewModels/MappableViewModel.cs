using GenericProject.Core.Data;

namespace GenericProject.Web.ViewModels
{
    public abstract class MappableViewModel<TModel> : IMappableViewModel<TModel> where TModel: class, new()
    {
        public TModel ToModel() { return this.ToModel(null); }

        public TModel ToModel(TModel modelToFill, int index) { return ToModel(modelToFill); }

        public TModel ToModel(TModel modelToFill)
        {
            modelToFill = modelToFill ?? new TModel();
            modelToFill.InjectFrom(this);
            return modelToFill;
        }
    }
}