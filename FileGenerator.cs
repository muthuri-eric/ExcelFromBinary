using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using Dapper;

namespace ExcelFromBinary;
internal class FileGenerator
{
    public async Task<IEnumerable<TemplateModel?>> GetTemplates()
    {
        using IDbConnection connection = new SqlConnection("Data Source=.;Persist Security Info=False;User ID=sa; Password=123; Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False");
        return await connection.QueryAsync<TemplateModel>("SELECT [TemplateName], [Template]  FROM [DB].[Report].[AfricaReport]  Where AfricaReportID = 128", null, commandType:CommandType.Text);
    }
}
public class TemplateModel
{
    public string? TemplateName { get; set; }
    public byte[]? Template { get; set; }
}
public class TemplateData
{
    FileGenerator fg = new FileGenerator();
    public async Task<IEnumerable<TemplateModel?>> fetchTemplates()
    {
        return await fg.GetTemplates();
    }
}
public class Test
{
    public IEnumerable<TemplateModel?>? Templates { get; set; }
    TemplateData td = new();
    public async Task GetTemplates()
    {
        Templates = await td.fetchTemplates();
        foreach (TemplateModel tm in Templates)
        {
            System.IO.File.WriteAllBytes(tm.TemplateName, tm.Template);
        }
    }

}
