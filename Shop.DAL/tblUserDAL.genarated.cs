namespace Shop.DAL
{
	using Shop.DAL;
	using Shop.DAL.Infrastructure;

    public partial class tblUserDAL : RepositoryBase<ShopEntities, tblUser> , ItblUserDAL
    {
        public tblUserDAL(IDatabaseFactory<ShopEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface ItblUserDAL : IRepositoryBase<tblUser>
    {
    }
    
}
