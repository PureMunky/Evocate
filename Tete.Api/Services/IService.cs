using System.Collections.Generic;

namespace Tete.Api.Services
{
  public interface IService<T>
  {
    IEnumerable<T> Get();
    T New();
    T Get(string Id);
    void Save(T Object);
    
  }
}