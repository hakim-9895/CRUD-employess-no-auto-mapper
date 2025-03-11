using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication10.DTO;
using WebApplication10.Modal;

namespace WebApplication10.Controllers
{
    [Route("api/departmentcontroller")]
    [ApiController]
    public class Departmentcontroller : ControllerBase
    {
        private readonly AppDbcontext _departmentdbcontext;
     public Departmentcontroller(AppDbcontext dbcontext)
        {
          _departmentdbcontext = dbcontext;
        }
        [HttpPost]
        public async Task<ActionResult> Postdepartment(DepartmentDto departmentDto)
        {
            var department = new Department
            {
                Name = departmentDto.Name
            };
            _departmentdbcontext.Department.Add(department);
            await _departmentdbcontext.SaveChangesAsync();
            return Ok(department);
        }
        [HttpGet]

        public async Task<ActionResult> getdepartment()
        {
            var department = await _departmentdbcontext.Department
                   .Include(x => x.Employes)
                   .Select(y => new Returndepartment
                   {
                       departmentname = y.Name,
                       
                       employess = y.Employes.Select( x => new EmployeeDto {
                       Name=x.name,
                       Id =x.id


                       }).ToList(),
                       totalemploye = y.Employes.Count()
                       
                   }).ToListAsync();
            return Ok(department);
        }

        [HttpDelete]

        public async Task<ActionResult> deletedeparrtment (int id)
        {
            var department = _departmentdbcontext.Department.FirstOrDefault(x => x.Id == id);
            if (department == null) return BadRequest("user is not found");
            try
            {
                _departmentdbcontext.Remove(department);
                await _departmentdbcontext.SaveChangesAsync();
                return Ok("suc");
            }
            catch (Exception ex) {
                return NotFound(ex.Message);
            }
        }
        

    


    }
}
