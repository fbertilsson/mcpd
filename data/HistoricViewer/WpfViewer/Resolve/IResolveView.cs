namespace WpfViewer.Resolve
{
    public interface IResolveView
    {
        ResolveViewModel Model { set; }
        bool? ShowDialog();
        void Close();
    }
}