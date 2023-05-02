using JoyGClient.Entities;
using JoyGClient.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JoyGClient.Data.Repositories
{
	public class SurveyRepository : ISurveyRepository
	{
		private readonly DataContext _context;
		public SurveyRepository(DataContext context)
		{
			_context = context;
		}

		public async Task<bool> AddSurveyAsync(Survey survey)
		{
			_context.Entry(survey).State = EntityState.Added;
			return await _context.SaveChangesAsync() > 0;
		}

		public async Task<bool> UpdateSurveyAsync(Survey survey)
		{
			_context.Entry(survey).State = EntityState.Modified;
			return await _context.SaveChangesAsync() > 0;
		}

		public async Task<IEnumerable<Survey>> GetAllSurveysAsync()
		{
			return await _context.Surveys
				.Include(x => x.Restaurant)
				.Include(x => x.User)
				.Include(x => x.DishesEnjoyed)
				.ToListAsync();
		}
		public async Task<Survey> GetSurveyByIdAsync(string id)
		{
			return await _context.Surveys
				.Where(x => x.Id.ToString() == id.Trim())
				.Include(x => x.Restaurant)
				.Include(x => x.User)
				.Include(x => x.DishesEnjoyed)
				.FirstOrDefaultAsync();
		}

		public async Task<IEnumerable<Survey>> GetAllSurveysByUserAsync(AppUser appUser)
		{
			return await _context.Surveys
				.Where(x => x.User == appUser)
				.Include(x => x.Restaurant)
				.Include(x => x.User)
				.Include(x => x.DishesEnjoyed)
				.ToListAsync();
		}
	}
}
