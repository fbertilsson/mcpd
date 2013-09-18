using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;

namespace linq
{
    class MainWindowViewModel : NotificationObject
    {
        public bool IsParallelSelected { get; set; }

        /// <summary>
        /// Number of fibonacci numbers to calculate
        /// </summary>
        public int N { get; set; }

        public string List
        {
            get { return m_List; }
            set
            {
                if (value == m_List) return;
                m_List = value;
                RaisePropertyChanged(() => List);
            }
        }

        public DelegateCommand Fib1Command { get; private set; }
        public DelegateCommand Fib2Command { get; private set; }

        private IFibonacciView View { get; set; }

        public MainWindowViewModel(IFibonacciView view)
        {
            View = view;
            Fib1Command = new DelegateCommand(OnFibonacci1Command);
            Fib2Command = new DelegateCommand(OnFibonacci2Command);
            N = 30;
        }

        private string m_ElapsedMillis;
        private string m_List;

        public string ElapsedMillis
        {
            get { return m_ElapsedMillis; }
            set
            {
                if (value == m_ElapsedMillis) return;
                m_ElapsedMillis = value;
                RaisePropertyChanged(() => ElapsedMillis);
            }
        }

        public void OnFibonacci1Command()
        {
            Execute(() => new Fibonacci().Fib(N, IsParallelSelected));
        }

        public void OnFibonacci2Command()
        {
            Execute(() => new Fibonacci().Fib2(N));
        }

        public void Execute(Func<IEnumerable<long>> func)
        {
            View.SetWaitPointer();

            List = null;
            View.InvokeAsync(() =>
            {
                var t = new Stopwatch();
                t.Start();
                var result = func();
                ElapsedMillis = t.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture);
                List = Format(result);
                View.SetNormalPointer();
            });
        }

        private static string Format(IEnumerable<long> fib)
        {
            return fib.Aggregate(string.Empty, (prefix, element) => string.Format("{0}\n{1}", prefix, element.ToString(CultureInfo.InvariantCulture)));
        }

    }
}
