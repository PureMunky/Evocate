using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Tete.Api.Contexts;
using Tete.Api.Services.Content;
using Tete.Models.Content;
using Tete.Tests.Setup;

namespace Tete.Tests.Api.Services.Content
{
  public class LinkServiceTests : TestBase
  {
    LinkService service;

    [SetUp]
    public void SetupSettings()
    {
      this.service = new LinkService(mockContext.Object, AdminUserVM);
    }

    [Test]
    public void GetTest()
    {
      var result = this.service.GetLinks();

      mockContext.Verify(m => m.Links, Times.Once);
    }

    [Test]
    public void SaveLinkTest()
    {
      var newLink = new Link();
      this.service.SaveLink(newLink);

      mockContext.Verify(m => m.Links.Add(It.IsAny<Link>()), Times.Once);
    }

    [Test]
    public void SaveExistingTest()
    {
      var existingLink = new Link()
      {
        LinkId = linkId
      };
      this.service.SaveLink(existingLink);

      mockContext.Verify(m => m.Links.Update(It.IsAny<Link>()), Times.Once);
    }
  }
}