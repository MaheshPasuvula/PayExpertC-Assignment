using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using CaseStudyAssignment.Model;
using CaseStudyAssignment.DataBaseUtil;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CaseStudyAssignment.DataBaseUtil
{
    internal class DataBaseContext
    {
        private static IConfiguration _iConfiguration;
        static DataBaseContext()
        {
            GetAppSettingsFile();
        }
        private static void GetAppSettingsFile()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            _iConfiguration = builder.Build();

        }
        public static string GetConnectionString()
        {
            return _iConfiguration.GetConnectionString("LocalConnString");
        }
    }
}
