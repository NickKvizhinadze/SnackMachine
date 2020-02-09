using System;
using System.Windows;
using System.Windows.Media;
using SnackMachine.Logic.SnackMachines;

namespace SnackMachine.UI.SnackMachines
{
    public class SnackPileViewModel
    {

        #region Fields
        private readonly SnackPile _snackPile;
        #endregion

        #region Constructor

        public SnackPileViewModel(SnackPile snackPile)
        {
            _snackPile = snackPile;
        }

        #endregion

        #region Properties
        public string Price => _snackPile.Price.ToString("C2");
        public int Amount => _snackPile.Quantity;
        public int ImageWidth => GetImageWidth(_snackPile.Snack);

        public ImageSource Image => (ImageSource)Application.Current.FindResource("img" + _snackPile.Snack.Name);
        #endregion

        #region Private Methods
        private int GetImageWidth(Snack snack)
        {
            if (snack == Snack.Chocolate)
                return 120;

            if (snack == Snack.Soda)
                return 70;

            if (snack == Snack.Gum)
                return 70;

            throw new ArgumentException();

        }
        #endregion

    }
}
