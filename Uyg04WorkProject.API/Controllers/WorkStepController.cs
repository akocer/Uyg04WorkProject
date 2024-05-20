using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Uyg04WorkProject.API.DTOs;
using Uyg04WorkProject.API.Models;

namespace Uyg04WorkProject.API.Controllers
{
    [Route("api/WorkStep")]
    [ApiController]
    [Authorize]
    public class WorkStepController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        ResultDto result = new ResultDto();
        public WorkStepController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]

        public async Task<List<WorkStepDto>> List()
        {
            var works = await _context.WorkSteps.ToListAsync();
            var workDtos = _mapper.Map<List<WorkStepDto>>(works);
            return workDtos;
        }

        [HttpPost]
        public async Task<ResultDto> Add(WorkStepDto dto)
        {
            if (_context.WorkSteps.Count(c => c.Title == dto.Title && c.WorkId == dto.WorkId) > 0)
            {
                result.Status = false;
                result.Message = "Girilen Başlık Kayıtlıdır!";
                return result;
            }


            var order = _context.WorkSteps.Where(s => s.WorkId == dto.WorkId).Count() + 1;

            var workstep = _mapper.Map<WorkStep>(dto);

            workstep.Created = DateTime.Now;
            workstep.Updated = DateTime.Now;
            workstep.Order = order;
            await _context.WorkSteps.AddAsync(workstep);
            await _context.SaveChangesAsync();

            result.Status = true;
            result.Message = "Kayıt Eklendi";
            return result;
        }
        [HttpPut]
        public async Task<ResultDto> Update(WorkStepDto dto)
        {
            var workstep = await _context.WorkSteps.Where(s => s.Id == dto.Id).SingleOrDefaultAsync();
            if (workstep == null)
            {
                result.Status = false;
                result.Message = "Kayıt Bulunamadı!";
                return result;

            }
            workstep.Title = dto.Title;
            workstep.Status = dto.Status;
            workstep.Updated = DateTime.Now;

            _context.WorkSteps.UpdateRange(workstep);
            await _context.SaveChangesAsync();
            result.Status = true;
            result.Message = "Kayıt Güncellendi";
            return result;
        }
        [HttpDelete]
        [Route("id")]
        public async Task<ResultDto> Delete(int id)
        {

            var workstep = await _context.WorkSteps.Where(s => s.Id == id).SingleOrDefaultAsync();
            if (workstep == null)
            {
                result.Status = false;
                result.Message = "Kayıt Bulunamadı!";
                return result;

            }


            _context.WorkSteps.Remove(workstep);
            await _context.SaveChangesAsync();
            result.Status = true;
            result.Message = "Kayıt Silindi";
            return result;
        }
        [HttpPost]
        [Route("WorkStepOrderAjax")]
        public ResultDto WorkStepOrderAjax(int[] ids)
        {
            for (int i = 0; i < ids.Length; i++)
            {
                var workstep = _context.WorkSteps.Where(s => s.Id == ids[i]).SingleOrDefault();
                workstep.Order = i + 1;
                _context.SaveChanges();

            }
            result.Status = true;
            result.Message = "Sıralandı...";
            return result;

        }
    }
}
