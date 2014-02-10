using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
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
        public DelegateCommand GroupingCommand { get; private set; }
        public DelegateCommand XmlDomAllBarAttributesCommand { get; private set; }
        public DelegateCommand XmlDomSumBarAttributesCommand { get; private set; }

        private IFibonacciView View { get; set; }


        public string XmlInput
        {
            get { return m_XmlInput; }
            set
            {
                if (value == m_XmlInput) return;
                m_XmlInput = value;
                RaisePropertyChanged(() => XmlInput);
            }
        }

        public string XmlOutput
        {
            get { return m_XmlOutput; }
            set
            {
                if (value == m_XmlOutput) return;
                m_XmlOutput = value;
                RaisePropertyChanged(() => XmlOutput);
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="view"></param>
        public MainWindowViewModel(IFibonacciView view)
        {
            View = view;
            Fib1Command = new DelegateCommand(OnFibonacci1Command);
            Fib2Command = new DelegateCommand(OnFibonacci2Command);
            XmlDomAllBarAttributesCommand = new DelegateCommand(OnXmlDomAllBarAttributesCommand);
            XmlDomSumBarAttributesCommand = new DelegateCommand(OnXmlDomSumBarAttributesCommand);
            GroupingCommand = new DelegateCommand(OnGroupingCommand);
            N = 30;

            XmlInput = @"<?xml version='1.0'?>
<foo> 
  <bar a='10'/>
  <bar a='20'/> 
</foo>";

        }

        private string m_ElapsedMillis;
        private string m_List;
        private string m_XmlOutput;
        private string m_XmlInput;

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


        
        private void OnGroupingCommand()
        {
            var result = new StringBuilder();

            var cultures = new[]
                {
                    new CultureInfo("nb-NO"),
                    new CultureInfo("nn-NO"),
                    new CultureInfo("sv-SE"),
                    new CultureInfo("sv-FI")
                };

            var culturesGrouped =
                from c in cultures
                group c by c.TwoLetterISOLanguageName
                into grouped
                select new
                    {
                        LanguageName = grouped.Key,
                        LanguageCount = grouped.Sum(x => 1)
                    };
            result.Append("Cultures 1:\n");
            foreach (var g in culturesGrouped)
            {
                result.Append(string.Format("  Language: {0}, Count: {1}\n", g.LanguageName, g.LanguageCount));
            }


            var culturesGrouped2 =
                from c in cultures
                group c by c.ThreeLetterISOLanguageName
                into grouped
                select new
                    {
                        ThreeLetterName = grouped.Key,
                        LanguageCount = grouped.Count(),
                        Langs = cultures.Where(x => x.ThreeLetterISOLanguageName.Equals(grouped.Key))
                    };
            result.Append("Cultures 2:\n");
            foreach (var g in culturesGrouped2)
            {
                result.Append(string.Format("  Language: {0}, Count: {1}\n", g.ThreeLetterName, g.LanguageCount));
            }
            List = result.ToString();
        }



        private void OnXmlDomAllBarAttributesCommand()
        {
            Time(() =>
                {
                    try
                    {
                        XmlOutput = new XmlDomRunner().GetAllBarAttributes(XmlInput);
                    }
                    catch (Exception e)
                    {
                        XmlOutput = e.ToString();
                    }
                });
        }

        private void OnXmlDomSumBarAttributesCommand()
        {
            Time(() =>
                {

                    try
                    {
                        XmlOutput = new XmlDomRunner().SumBarAttributes(XmlInput);
                    }
                    catch (Exception e)
                    {
                        XmlOutput = e.ToString();
                    }
                });
        }


        private void Time(Action action)
        {
            View.SetWaitPointer();
            View.InvokeAsync(() =>
                {
                    try
                    {
                        var t = new Stopwatch();
                        t.Start();
                        action();
                        t.Stop();
                        ElapsedMillis = t.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture);
                    }
                    finally
                    {
                        View.SetNormalPointer();
                    }
                });
        }


    }
}
