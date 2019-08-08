using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Tete.Controllers;
using Tete.Modules;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Tests.Controllers
{
  public class ModuleControllerTests
  {

    [Test]
    public void GetAll()
    {
      ModuleController mc = new ModuleController();

      ActionResult<List<Module>> sr = mc.Get();
      Assert.Inconclusive();
    }

    [Test]
    public void GetOne()
    {
      string name = "Example";
      string baseUrl = "https://test.com";
      
      ModuleController mc = new ModuleController();
      ModuleService ms = new ModuleService();
      Module m = new Module(name, baseUrl);

      ms.Save(m);
      
      ActionResult<Module> response = mc.Get(name);


      Assert.AreEqual(m.Name, response.Value.Name);
    }

    [Test]
    public void PostTest()
    {
      Module m = new Module();
      ModuleController mc = new ModuleController();
      mc.Post(m);
      Assert.Inconclusive();
    }

    [Test]
    public void PutTest()
    {
      Module m = new Module();
      ModuleController mc = new ModuleController();
      mc.Put(m);
      Assert.Inconclusive();
    }

    [Test]
    public void DeleteTest()
    {
      string name = "Example";
      ModuleController mc = new ModuleController();
      mc.Delete(name);
      Assert.Inconclusive();
    }
  }
}