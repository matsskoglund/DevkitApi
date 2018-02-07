using System.Collections.Generic;
using System.Threading.Tasks;
using DevkitApi.Model;

namespace DevkitApi.Services
{
    public interface IDevkitService
    {
        // Search and Get
        Devkit FindById(int id);
        IEnumerable<Tool> GetToolsForDevkit(int devkitID);
        IEnumerable<DevkitTools> GetDevkitToolsForDevkit(int devkitID);
        IEnumerable<Devkit> GetDevkits();
       

        Devkit Add(Devkit devkit);
        Task<Devkit> AddAsync(Devkit devkit);
        Task<Devkit> UpdateAsync(Devkit devkit, object key);

        Task<DevkitTools> AddToolsAsync(DevkitTools tools);
        Task<int> DeleteAsync(Devkit devkit);
        Task<Devkit> FindByIdAsync(int id);
        Task<int> DeleteToolsAsync(int id);

    }
}