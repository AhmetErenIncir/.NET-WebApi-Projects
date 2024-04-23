using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace BasicWebApiProject.Controllers;


[ApiController]
[Route("[controller]")]
public class StudentController : ControllerBase
{
    public readonly IConfiguration _configuration;
    public static List<Student> Students = new List<Student>();

    public StudentController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    private List<Student> LoadData()
    {
        List<Student> list = new List<Student>();
        using (var con = new SqlConnection(_configuration.GetConnectionString("StudentDbCon")))
        using (var sda = new SqlDataAdapter("Select * from [Student].[dbo].[Students]", con))
        {
            DataTable dt = new DataTable();
            sda.Fill(dt);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Student student = new Student();
                student.Id = Convert.ToInt32(dt.Rows[i]["ID"]);
                student.Name = Convert.ToString(dt.Rows[i]["NAME"]) ?? "";
                student.SurName = Convert.ToString(dt.Rows[i]["SURNAME"]) ?? "";
                list.Add(student);
            }
        }
        return list;
    }

    private void InsertData(Student student)
    {
        using (var con = new SqlConnection(_configuration.GetConnectionString("StudentDbCon")))
        {
            con.Open();
            var sql = "INSERT INTO [Student].[dbo].[Students](ID,NAME, SURNAME) VALUES(@ID,@NAME, @SURNAME)";
            using (var cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@ID", student.Id);
                cmd.Parameters.AddWithValue("@NAME", student.Name);
                cmd.Parameters.AddWithValue("@SURNAME", student.SurName);

                cmd.ExecuteNonQuery();
            }
        }
    }

    [HttpGet]
    public List<Student> Get()
    {
        return LoadData();
    }

    [HttpGet("{id}")]
    public Student Get(int id)
    {
        var data = LoadData();
        var result = data.FirstOrDefault(x => x.Id == id);
        if (result != null)
        {
            return result;
        }
        else
        {
            return new Student();
        }
    }
    [HttpPost]
    public Student Post(Student student)
    {
        InsertData(student);
        return student;
    }
}