namespace ProductsRestApi.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Category GetCategory(string CategoryName)
        {
            return _context.Categories.FirstOrDefault(c => c.Name == CategoryName);
        }
    }
}
