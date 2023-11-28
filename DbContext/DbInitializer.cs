
namespace BankingEvaluation.DbContext
{
    internal class DbInitializer
    {
        public static void Initialize(IUnitOfWork context)
        {
            if (!Directory.Exists("./Assets"))
                Directory.CreateDirectory("./Assets");
            context.EnsureCreated();
        }
        
    }
}
