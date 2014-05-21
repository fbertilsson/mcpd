using System.Windows;

namespace LinqToSql1
{
    public partial class LinqToSql1DataContext
    {
        partial void DeleteTopic(Topic instance)
        {
            MessageBox.Show(string.Format("In extension point, method DeleteTopic with id {0}.", instance.id), "Good news");
        }
    }
}
