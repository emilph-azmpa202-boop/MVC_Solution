using Microsoft.EntityFrameworkCore;
using solution.Data;
using solution.Models;
using solution.Services.Interfaces;
using solution.ViewModels.Team;

namespace solution.Services
{
    public class MemberService : IMemberService
    {
        private readonly AppDbContext _context;
        private readonly IFileService _fileService;

        public MemberService(
            AppDbContext context,
            IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        public async Task<List<MemberVM>> GetAllAsync()
        {
            return await _context.Members
                .Select(m => new MemberVM
                {
                    Id = m.Id,
                    Name = m.Name,
                    Job = m.Job,
                    Description = m.Description,
                    Image = m.Image
                })
                .ToListAsync();
        }

        public async Task<MemberVM> GetByIdAsync(int id)
        {
            return await _context.Members
                .Where(m => m.Id == id)
                .Select(m => new MemberVM
                {
                    Id = m.Id,
                    Name = m.Name,
                    Job = m.Job,
                    Description = m.Description,
                    Image = m.Image
                })
                .FirstOrDefaultAsync();
        }

        public async Task CreateAsync(MemberCreateVM model)
        {
            string fileName =
                await _fileService.UploadAsync(
                    model.Photo,
                    "uploads/team");

            Member member = new()
            {
                Name = model.Name,
                Job = model.Job,
                Description = model.Description,
                Image = fileName
            };

            await _context.Members.AddAsync(member);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(
            int id,
            MemberUpdateVM model)
        {
            Member member =
                await _context.Members
                .FirstOrDefaultAsync(x => x.Id == id);

            if (member == null)
                return;

            if (model.Photo != null)
            {
                string oldPath =
                    Path.Combine(
                        "wwwroot",
                        "uploads",
                        "team",
                        member.Image);

                _fileService.Delete(oldPath);

                member.Image =
                    await _fileService.UploadAsync(
                        model.Photo,
                        "uploads/team");
            }

            member.Name = model.Name;
            member.Job = model.Job;
            member.Description = model.Description;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Member member =
                await _context.Members
                .FirstOrDefaultAsync(x => x.Id == id);

            if (member == null)
                return;

            string path =
                Path.Combine(
                    "wwwroot",
                    "uploads",
                    "team",
                    member.Image);

            _fileService.Delete(path);

            _context.Members.Remove(member);

            await _context.SaveChangesAsync();
        }

        public async Task<List<MemberUIVM>> GetAllForUIAsync()
        {
            return await _context.Members
                .Select(x => new MemberUIVM
                {
                    Name = x.Name,
                    Job = x.Job,
                    Image = x.Image
                })
                .ToListAsync();
        }
    }
}