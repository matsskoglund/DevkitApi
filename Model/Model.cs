using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Configuration;
using System;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;

namespace DevkitApi.Model
{
    
    public class Category
    {
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public ICollection<Tool> Tools { get; set; }
    }
    
    public class Tool
    {
        public int ToolID { get; set; }
        public string Name { get; set; }
        public string URLRef { get; set; }
        public string Description { get; set; }
        public string Aquire { get; set; }

        public int CategoryID { get; set; }       
        public ICollection<DevkitTools> DevkitTools { get; set; }
    }

    public class Devkit
    {
        

        public int DevkitID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        public string Email { get; set; }

        public ICollection<DevkitTools> DevkitTools { get; set; }
    }
    public class DevkitTools
    {
        public int DevkitToolsID { get; set; }
        public int DevkitID { get; set; }
        public int ToolId { get; set; }

        public Devkit Devkit { get; set; }      
        public Tool Tool {get; set; }
    }
}