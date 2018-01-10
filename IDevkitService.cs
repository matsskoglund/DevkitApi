using System.Collections.Generic;
using DevkitApi.Model;

namespace DevkitApi.Services 
{
  public interface IDevkitService
  {
    // Search and Get
    Devkit FindById (int id);
    IEnumerable<Tool> GetToolsForDevkit(int devkitID);
  }
}