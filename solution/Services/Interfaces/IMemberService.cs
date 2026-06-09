using solution.ViewModels.Team;

namespace solution.Services.Interfaces
{
    public interface IMemberService
    {
        Task<List<MemberVM>> GetAllAsync();

        Task<MemberVM> GetByIdAsync(int id);

        Task CreateAsync(MemberCreateVM model);

        Task UpdateAsync(int id, MemberUpdateVM model);

        Task DeleteAsync(int id);

        Task<List<MemberUIVM>> GetAllForUIAsync();
    }
}