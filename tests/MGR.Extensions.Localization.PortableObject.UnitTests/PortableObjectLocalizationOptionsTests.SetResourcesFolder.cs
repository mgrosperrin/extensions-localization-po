using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MGR.Extensions.Localization.PortableObject.UnitTests;
public partial class PortableObjectLocalizationOptionsTests
{
    public class SetResourcesFolder
    {
        [Fact]
        public void GivenEmptyResourcesFolder_WhenSettingResourcesFolder_ThenShouldSetResourcesFolder()
        {
            // Arrange
            var options = new PortableObjectLocalizationOptions();
            
            // Act
            options.SetResourcesFolder(string.Empty);
            
            // Assert
            Assert.Equal(string.Empty, options.ResourcesFolder);
        }
        [Fact]
        public void GivenValidResourcesFolder_WhenSettingResourcesFolder_ThenShouldSetResourcesFolder()
        {
            // Arrange
            var options = new PortableObjectLocalizationOptions();
            var resourcesFolder = "MyResources";
            
            // Act
            options.SetResourcesFolder(resourcesFolder);

            // Assert
            Assert.Equal("MyResources", options.ResourcesFolder);
        }
    }
}
