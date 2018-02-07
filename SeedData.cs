using DevkitApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevkitApi
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider services)
        {
            var context = services.GetRequiredService<DevkitContext>();
           // context.Database.OpenConnection();
            context.Database.EnsureCreated();
            bool toolEx = context.Tools.Any();
            if (!toolEx)
            {
                context.Tools.Add(new Model.Tool()
                {
                    ToolID = 1,
                    Name = "Visual Studio Code",
                    Aquire = "https://code.visualstudio.com/#alt-downloads",
                    AquireType = "Download",
                    Description = "An awesome code editor for free. Get it by clicking \"Download\".",
                    URLRef = "https://code.visualstudio.com/"
                });
                context.Tools.Add(new Model.Tool()
                {
                    ToolID = 2,
                    Name = "Visual Studio Enterprise",
                    Aquire = "https://www.visualstudio.com/vs",
                    AquireType = "Download",
                    Description = "The complete IDE for .NET framework developers.Comes with a monthly fee.get it by clicking \"Order\"",
                    URLRef = "https://www.visualstudio.com/vs"
                });
                context.Tools.Add(new Model.Tool()
                {
                    ToolID = 3,
                    Name = "Github",
                    Aquire = "https://www.github.com/register",
                    AquireType = "Register",
                    Description = "Github for source control to use the popular Github.com style of working in an open-source manner. ",
                    URLRef = "https://www.github.com"
                });
                context.Tools.Add(new Model.Tool()
                {
                    ToolID = 4,
                    Name = "Artifactory",
                    Aquire = "https://www.jfrog.com/register",
                    AquireType = "Register",
                    Description = "Our repository manager. Requires an account and comes with a monthly fee.",
                    URLRef = "https://www.artifactory.com"
                });

                context.Tools.Add(new Model.Tool()
                {
                    ToolID = 5,
                    Name = "Postman",
                    Aquire = "https://www.getpostman.org",
                    AquireType = "Download",
                    Description = "A complete API development environment",
                    URLRef = "https://www.getpostman.org"
                });
            }
            if(!context.Devkits.Any())
            {
                context.Devkits.Add(new Model.Devkit()
                {
                    DevkitID = 1,
                    Name = "Devkit for .NET Framework developers",
                    ShortName = ".NET devkit",
                    Description = "For developers working in .NET Framework we have a set of resources that help you get going with your development with a swosh. Check out the items in the Devkit below.",
                    Email = "mats.skoglund@devkits.com",                    
                });
                context.Devkits.Add(new Model.Devkit()
                {
                    DevkitID = 2,
                    Name = "Devkit for Java developers",
                    ShortName = "Java devkit",
                    Description = "For developers working in Java we have a set of resources that help you get going with your development with a swosh. Check out the items in the Devkit below.",
                    Email = "mats.skoglund@devkits.com",
                });
                context.Devkits.Add(new Model.Devkit()
                {
                    DevkitID = 3,
                    Name = "Devkit for Front-end developers",
                    ShortName = "FrontEnd devkit",
                    Description = "For developers working with frontend to help you get going with creating awesome user experiences. Check out the items in the Devkit below and get them by clicking the links one by one in the right column.",
                    Email = "mats.skoglund@devkits.com",
                });
            }   
            
            if(!context.DevkitTools.Any())
            {
                context.DevkitTools.Add(new Model.DevkitTools()
                {
                    DevkitToolsID = 1,
                    DevkitID = 1,
                    ToolId = 2,
                    ToolType = "Core"
                });
                context.DevkitTools.Add(new Model.DevkitTools()
                {
                    DevkitToolsID = 2,
                    DevkitID = 1,
                    ToolId = 3,
                    ToolType = "Core"
                });
                context.DevkitTools.Add(new Model.DevkitTools()
                {
                    DevkitToolsID = 3,
                    DevkitID = 1,
                    ToolId = 4,
                    ToolType = "Optional"
                });

                // Kit 2
                context.DevkitTools.Add(new Model.DevkitTools()
                {
                    DevkitToolsID = 4,
                    DevkitID = 2,
                    ToolId = 1,
                    ToolType = "Core"
                });
                context.DevkitTools.Add(new Model.DevkitTools()
                {
                    DevkitToolsID = 5,
                    DevkitID = 2,
                    ToolId = 3,
                    ToolType = "Core"
                });
                context.DevkitTools.Add(new Model.DevkitTools()
                {
                    DevkitToolsID = 6,
                    DevkitID = 2,
                    ToolId = 4,
                    ToolType = "Core"
                });

                context.DevkitTools.Add(new Model.DevkitTools()
                {
                    DevkitToolsID = 7,
                    DevkitID = 2,
                    ToolId = 5,
                    ToolType = "Optional"
                });

                // Kit 3
                context.DevkitTools.Add(new Model.DevkitTools()
                {
                    DevkitToolsID = 8,
                    DevkitID = 3,
                    ToolId = 1,
                    ToolType = "Core"
                });
                context.DevkitTools.Add(new Model.DevkitTools()
                {
                    DevkitToolsID = 9,
                    DevkitID = 3,
                    ToolId = 3,
                    ToolType = "Core"
                });

                context.DevkitTools.Add(new Model.DevkitTools()
                {
                    DevkitToolsID = 10,
                    DevkitID = 3,
                    ToolId = 4,
                    ToolType = "Optional"
                });
            }
            context.SaveChanges();
        }
    }
}
