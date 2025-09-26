using H.Core.Models;
using H.Core.Models.Animals;

namespace H.Core.Factories
{
    public class GenericFactory<T> : IFactory<T> where T : new()
    {
        #region Constructors

        public GenericFactory()
        {
            
        }

        #endregion

        #region Public Methods

        public T Create(Farm farm)
        {
            // If T has a parameterless constructor, just create a new instance.
            // You can add logic here to use the Farm parameter if needed.
            return new T();
        }

        public IDto CreateFromTemplate(IDto template)
        {
            throw new NotImplementedException();
        } 

        #endregion
    }
}