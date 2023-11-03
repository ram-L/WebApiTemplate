namespace WebApiTemplate.Infrastructure.Data
{
    public interface IAppDbContextProvider
    {
        /// <summary>
        /// Gets the application's database context instance.
        /// </summary>
        AppDbContext Context { get; }
    }

    /// <summary>
    /// Provides an implementation of the <see cref="IAppDbContextProvider"/> interface.
    /// </summary>
    public class AppDbContextProvider : IAppDbContextProvider, IDisposable
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppDbContextProvider"/> class using a new instance of the database context.
        /// </summary>
        public AppDbContextProvider()
        {
            _context = new AppDbContext();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppDbContextProvider"/> class using the specified database context.
        /// </summary>
        /// <param name="context">An existing instance of the database context.</param>
        public AppDbContextProvider(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets the application's database context instance.
        /// </summary>
        public AppDbContext Context => _context;

        /// <summary>
        /// Releases any resources held by the database context.
        /// </summary>
        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
