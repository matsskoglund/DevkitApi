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
        Task<Devkit> UpdateAsync(Devkit kit);
    }
}