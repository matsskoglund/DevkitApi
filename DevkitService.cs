using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Configuration;
using System;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;
using DevkitApi.Model;
using System.Linq;
using System.Threading.Tasks;

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
                .SingleOrDefault();
            return devkit;
    }

        public async Task<Devkit> FindByIdAsync(int id)
        {
            //var devkit = _dkContext.Devkits.Where(i => i.DevkitID == id)
            //     .Include(m => m.DevkitTools)
            //  .SingleOrDefault();
            return await _dkContext.Set<Devkit>().FindAsync(id);
            //return devkit;
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

        public IEnumerable<Devkit> GetDevkits()
        {
            return _dkContext.Devkits;
        }


        public  void Save()
        {

            _dkContext.SaveChanges();
        }


        public  Devkit Add(Devkit devkit)
        {

            _dkContext.Set<Devkit>().Add(devkit);
            _dkContext.SaveChanges();
            return devkit;
        }

        public  async Task<Devkit> AddAsync(Devkit devkit)
        {
            _dkContext.Set<Devkit>().Add(devkit);
            await _dkContext.SaveChangesAsync();
            return devkit;
        }

        public async Task<DevkitTools> AddToolsAsync(DevkitTools tools)
        {
            _dkContext.Set<DevkitTools>().Add(tools);
            await _dkContext.SaveChangesAsync();
            return tools;
        }

        public  async Task<Devkit> UpdateAsync(Devkit devkit, object key)
        {
            if (devkit == null)
                return null;
            Devkit exist = await _dkContext.Set<Devkit>().FindAsync(key);
            if (exist != null)
            {
                _dkContext.Entry(exist).CurrentValues.SetValues(devkit);
                await _dkContext.SaveChangesAsync();
            }
            return exist;
        }

        public  async Task<int> DeleteAsync(Devkit devkit)
        {
            _dkContext.Set<Devkit>().Remove(devkit);
            return await _dkContext.SaveChangesAsync();
        }

        public async Task<int> DeleteToolsAsync(int id)
        {
            IEnumerable<DevkitTools> allTools = this.GetDevkitToolsForDevkit(id);
            _dkContext.DevkitTools.RemoveRange(allTools);

            
            return await _dkContext.SaveChangesAsync();
        }

    }
}