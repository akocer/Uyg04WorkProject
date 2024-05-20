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
    [Route("api/Work")]
    [ApiController]
    [Authorize]
    public class WorkController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        ResultDto result = new ResultDto();
        public WorkController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]

        public async Task<List<WorkDto>> List()
        {
            var works = await _context.Works.OrderBy(o => o.Order).ToListAsync();
            var workDtos = _mapper.Map<List<WorkDto>>(works);
            return workDtos;
        }

        [HttpGet("{id}")]
        public async Task<WorkDto> Get(int id)
        {
            var works = await _context.Works.Where(s => s.Id == id).SingleOrDefaultAsync();
            var workDto = _mapper.Map<WorkDto>(works);
            return workDto;
        }
        [HttpPost]
        public async Task<ResultDto> Add(WorkDto dto)
        {
            if (_context.Works.Count(c => c.Title == dto.Title) > 0)
            {
                result.Status = false;
                result.Message = "Girilen Başlık Kayıtlıdır!";
                return result;
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var order = _context.Works.Where(s => s.AppUserId == userId).Count() + 1;

            var work = _mapper.Map<Work>(dto);
            work.AppUserId = userId;
            work.Created = DateTime.Now;
            work.Updated = DateTime.Now;
            work.Order = order;
            await _context.Works.AddAsync(work);
            await _context.SaveChangesAsync();

            result.Status = true;
            result.Message = "Kayıt Eklendi";
            return result;
        }
        [HttpPut]
        public async Task<ResultDto> Update(WorkDto dto)
        {
            var work = await _context.Works.Where(s => s.Id == dto.Id).SingleOrDefaultAsync();
            if (work == null)
            {
                result.Status = false;
                result.Message = "Kayıt Bulunamadı!";
                return result;

            }
            work.Title = dto.Title;
            work.Description = dto.Description;
            work.Updated = DateTime.Now;

            _context.Works.UpdateRange(work);
            await _context.SaveChangesAsync();
            result.Status = true;
            result.Message = "Kayıt Güncellendi";
            return result;
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<ResultDto> Delete(int id)
        {

            var work = await _context.Works.Where(s => s.Id == id).SingleOrDefaultAsync();
            if (work == null)
            {
                result.Status = false;
                result.Message = "Kayıt Bulunamadı!";
                return result;

            }
            if (_context.WorkSteps.Count(c => c.WorkId == id) > 0)
            {
                result.Status = false;
                result.Message = "İşlem Kaydı Vardır Silinemez!";
                return result;
            }

            _context.Works.Remove(work);
            await _context.SaveChangesAsync();
            result.Status = true;
            result.Message = "Kayıt Silindi";
            return result;
        }

        [HttpPost]
        [Route("WorkOrderAjax")]
        public ResultDto WorkOrderAjax(int[] ids)
        {
            for (int i = 0; i < ids.Length; i++)
            {
                var work = _context.Works.Where(s => s.Id == ids[i]).SingleOrDefault();
                work.Order = i + 1;
                _context.SaveChanges();

            }
            result.Status = true;
            result.Message = "Sıralandı...";
            return result;

        }

    }
}
