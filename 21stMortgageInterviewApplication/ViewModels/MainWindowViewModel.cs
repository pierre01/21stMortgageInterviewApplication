using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _21stMortgageInterviewApplication.ViewModels
{

    public sealed class MainWindowViewModel:ViewModelBase
    {
        #region private members
        /// <summary>
        /// Numbers entered by the use in the textbix (after decoding them)
        /// </summary>
        private readonly List<int> _numbers = new List<int>();
        private string _result;
        private int _resNumber;
        #endregion

        #region Commands
        // Commands for the 3 buttons
        public RelayCommand FindLargestNumberCommand { get; private set; }
        public RelayCommand FindOddSumCommand  { get; private set; }
        public RelayCommand FindEvenSumCommand  { get; private set; }
        #endregion

        #region constructor
        public MainWindowViewModel()
        {
            FindLargestNumberCommand = new RelayCommand(ExecuteFindLargestNumber, CanExecuteOnList);
            FindOddSumCommand = new RelayCommand(ExecuteFindOddSum, CanExecuteOnList);
            FindEvenSumCommand = new RelayCommand(ExecuteFindEvenSum, CanExecuteOnList);
        }
        #endregion


        #region command handlers

            /// <summary>
            /// Can we execute the command on the list
            /// </summary>
            public bool CanExecuteOnList(object obj)
            {
                // return _numbers.Count == 0; // Removed because of tabbing issue constraints
                return true;
            }

            /// <summary>
            /// Find the largest number in the list
            /// </summary>
            public  void ExecuteFindLargestNumber(object obj)
            {
                if (_numbers.Count == 0)
                    return;

                _resNumber= _numbers.Max();
                Result = _resNumber.ToString();
            }

            /// <summary>
            /// calculate the sum of odd numbers in the list
            /// </summary>
            public  void ExecuteFindOddSum(object obj)
            {
                if (_numbers.Count == 0)
                    return;

                _resNumber = _numbers.Where(n => n % 2 != 0).Sum();
                Result = _resNumber.ToString();
            }

            /// <summary>
            /// calculate the sum of even numbers in the list
            /// </summary>
            public  void ExecuteFindEvenSum(object obj)
            {
                if (_numbers.Count == 0)
                    return;

                _resNumber = _numbers.Where(n => n % 2 == 0).Sum();
                Result = _resNumber.ToString();
            }
    #endregion

        #region properties

            public bool IsPositive => _resNumber >= 0;

            /// <summary>
            /// Result of the commands
            /// </summary>
            public string Result
            {
                get => _result;
                private set
                {
                    SetField(ref _result, value);
                    OnPropertyChanged(nameof(IsPositive));
                }
            }
            
            /// <summary>
            /// The text the user types (must be a list of coma separated integers)
            /// </summary>
            public string UserInput {
                get => string.Join(",", _numbers);
                set
                {
                    _numbers.Clear();
                    var items = value.Split(',');
                    foreach (var item in items)
                    {
                        int res;
                        if(int.TryParse(item, out res))
                            _numbers.Add(res);
                    }

                    OnPropertyChanged();
                    
                }
            }
            #endregion


    }
}

