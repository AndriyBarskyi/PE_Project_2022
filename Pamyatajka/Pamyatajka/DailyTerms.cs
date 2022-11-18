using System.Collections.ObjectModel;
using System.Windows;

namespace PamyatajkaUI
{
    public class DailyTerms : ObservableCollection<int>
    {
        public DailyTerms ()
        {
            Add(5);
            Add(7);
            Add(10);
            Add(12);
            Add(15);
        }
    }
}