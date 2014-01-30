using System;

namespace GenericProject.Web.ViewModels
{
    public interface IMappableViewModel<TModel> where TModel: class, new() { 
       TModel ToModel(TModel modelToFill);
    }
}