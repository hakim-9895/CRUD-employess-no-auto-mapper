using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication10.DTO;
using WebApplication10.Modal;

namespace WebApplication10.Controllers
{
    [Route("Api/Employ")]
    [ApiController]
    public class Employcontroller : Controller
    {
        private readonly AppDbcontext _dbcontext;
       public Employcontroller(AppDbcontext context ) {
            
        _dbcontext = context;
        
        }


        [HttpPost]
        public async Task<ActionResult> Postemploy(EmployeeDto employeeDto)
        {
            try
            {
                var depertment = _dbcontext.Department.FirstOrDefault(x => x.Id == employeeDto.Departmentid);
                if (depertment == null)
                {

                    return NotFound("department is not found ");
                }
               Console.WriteLine(depertment.Name);
                var employ = new Employes
                {
                    name = employeeDto.Name,
                    Departmentid = depertment.Id,
                   

                };

                

                _dbcontext.Employes.Add(employ);
                await _dbcontext.SaveChangesAsync();

                var resultDto = new EmployeeDto
                {
                    Id = employ.id,
                    Name = employ.name,
                  DepartmentName = depertment.Name,
                    Departmentid = employ.Departmentid
                };

                return Ok(resultDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<ActionResult> getallemploy(int pagnumber ,int pagesize )

        {
            try
            {
                var employement = _dbcontext.Employes
                    .Include(x => x.department)
                    .Select(y => new ReturnEmploy
                    {
                        departmentname = y.department.Name,
                        employid = y.id,
                        name = y.name,
                    }).Skip(pagnumber)
                    .Take(pagesize)
                    .ToList()
                    .ToList();

             

                return Ok(skiped);
                if (employement != null) { return NotFound("employees not found"); }
                
            }
           
            catch (Exception ex) { return StatusCode(500, "system error"); }
        }

        [HttpDelete]
        public async Task<ActionResult> deleteemploy(int id)
        {
            var employ = _dbcontext.Employes.FirstOrDefault(x => x.id == id);
            if (employ == null)  return BadRequest("not found");

               _dbcontext.Employes.Remove(employ);
                await _dbcontext.SaveChangesAsync();
            return Ok("succesfully deleted");
        }

    }

}
