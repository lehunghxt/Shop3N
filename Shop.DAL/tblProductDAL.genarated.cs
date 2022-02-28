namespace Shop.DAL
{
	using Shop.DAL;
	using Shop.DAL.Infrastructure;

    public partial class tblProductDAL : RepositoryBase<ShopEntities, tblProduct> , ItblProductDAL
    {
        public tblProductDAL(IDatabaseFactory<ShopEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface ItblProductDAL : IRepositoryBase<tblProduct>
    {
    }
    
}
