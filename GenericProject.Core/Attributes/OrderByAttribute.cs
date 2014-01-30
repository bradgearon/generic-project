using System;

namespace GenericProject.Core.Attributes
{
    public class OrderByAttribute : Attribute
    {
        public string PropertyName { get; set; }
    }
}