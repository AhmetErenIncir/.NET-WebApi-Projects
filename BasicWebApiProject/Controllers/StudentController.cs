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

    public StudentController(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    static List<Student> Students = new List<Student>()
    {
        new Student(){Id = 1,Name = "Ahmet Eren",SurName = "Incir"},
        new Student(){Id = 2,Name = "Mustafa",SurName = "Ayar"},
        new Student(){Id = 3,Name = "Özgül",SurName = "Incir"},
        new Student(){Id = 4,Name = "Mehmet",SurName = "Incir"},
    };

    [HttpGet]
    public List<Student> Get()
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

    [HttpGet("{id}")]
    public Student Get(int id)
    {
        var result = Students.FirstOrDefault(x => x.Id == id);
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
        Students.Add(student);
        return student;
    }
}