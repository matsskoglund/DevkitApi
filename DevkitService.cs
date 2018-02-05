using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Configuration;
using System;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;
using DevkitApi.Model;
using System.Linq;

namespace DevkitApi.Services
{
  public class DevkitService : IDevkitService
  {
    private DevkitContext _dkContext;

    public DevkitService (DevkitContext dkContext)
    {
        _dkContext = dkContext;
    }

    public Devkit FindById (int id)
    {
            var devkit = _dkContext.Devkits.Where(i => i.DevkitID == id)
           //     .Include(m => m.DevkitTools)
                .SingleOrDefault();
            return devkit;
    }

    public IEnumerable<Tool> GetToolsForDevkit(int devkitID)
    {            
            var devkit = _dkContext.Devkits.Where(i => i.DevkitID == devkitID).Include(d => d.DevkitTools);
            var devkittools = devkit.SelectMany(z => z.DevkitTools);
            var tools = devkittools.Select(d => d.Tool);

            return tools.OrderBy(tool => tool.Name);
    }
        public IEnumerable<DevkitTools> GetDevkitToolsForDevkit(int devkitID)
        {
            var devkit = _dkContext.Devkits.Where(i => i.DevkitID == devkitID).Include(d => d.DevkitTools);
            var devkittools = devkit.SelectMany(z => z.DevkitTools);            

            return devkittools;
        }

    }
}