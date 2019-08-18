using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Tete.Api.Services
{
  public class Service<T> : IService<T> where T : class, new()
  {
    private DbSet<T> Set { get; set; }

    public Service(DbSet<T> Set)
    {
      this.Set = Set;
    }

    public T New()
    {
      return new T();
    }

    public IEnumerable<T> Get()
    {
      return this.Set;
    }

    public T Get(string Id)
    {
      return this.Set.Find(Id);
    }

    public void Save(T Object)
    {
      this.Set.Add(Object);
    }

  }
}