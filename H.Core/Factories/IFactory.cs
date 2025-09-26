using H.Core.Models;

namespace H.Core.Factories;

public interface IFactory<T>
{
    T Create(Farm farm);
    IDto CreateFromTemplate(IDto template);
}