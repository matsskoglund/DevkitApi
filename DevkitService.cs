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
        //var devkit = _dkContext.Devkits.Where(i => i.DevkitID == id);
        var devkit = _dkContext.Devkits.Include(s => s.DevkitTools).ThenInclude(t => t.Tool).AsNoTracking().SingleOrDefault(m => m.DevkitID == id);
        return devkit;
    }
        
    public IEnumerable<Tool> GetToolsForDevkit(int devkitID)
    {
            //   var tools = _dkContext.Tools.Include(s => s.DevkitTools).ThenInclude(t => t.Devkits).Where(m => m.DevkitID == devkitID).ToList();
            //   return tools;
            return null;
    }
  }
}