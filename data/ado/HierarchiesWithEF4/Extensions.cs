namespace HierarchiesWithEF4
{
    public partial class HierarchiesContainer
    {
        // No, this can't be done:
        // '...DeleteObject(object)': cannot override inherited member '...ObjectContext.DeleteObject(object)' because it is not marked virtual, abstract, or override
        //public override void DeleteObject(object entity)
        //{
        //    base.DeleteObject(entity);
        //}
    }
}
