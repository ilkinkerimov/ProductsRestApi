namespace ProductsRestApi.Data
{
    public interface ISoftDelete
    {
        public bool IsDeleted { get; set; }
    }
}
