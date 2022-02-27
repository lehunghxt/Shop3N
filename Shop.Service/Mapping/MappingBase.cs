namespace Shop.Service.Mapping
{
    #region

    using AutoMapper;

    #endregion

    /// <summary>
    /// The model mapping base.
    /// </summary>
    /// <typeparam name="TEntity">
    /// Type of entity.
    /// </typeparam>
    /// <typeparam name="TModel">
    /// Type of model.
    /// </typeparam>
    public abstract class MappingBase<TEntity, TModel> : Profile
    {
        #region Methods

        public MappingBase()
        {
            //CreateMap<TEntity, TModel>();
            //CreateMap<TModel, TEntity>();
            this.Map(this.CreateMap<TEntity, TModel>(), this.CreateMap<TModel, TEntity>());
        }

        ///// <summary>
        /////     The configure.
        ///// </summary>
        //protected override void Configure()
        //{
        //    this.Map(this.CreateMap<TEntity, TModel>(), this.CreateMap<TModel, TEntity>());
        //}

        /// <summary>
        /// The config map.
        /// </summary>
        /// <param name="mapping">
        /// The mapping.
        /// </param>
        /// <param name="reserveMapping">
        /// The reserve mapping.
        /// </param>
        protected abstract void Map(
            IMappingExpression<TEntity, TModel> mapping,
            IMappingExpression<TModel, TEntity> reserveMapping);

        #endregion
    }
}