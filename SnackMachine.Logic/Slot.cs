namespace SnackMachine.Logic
{
    public class Slot : Entity
    {
        #region Constructor
        protected Slot()
        {
        }

        public Slot(SnackMachine snackMachine, int position)
        {
            SnackMachine = snackMachine;
            Position = position;
            SnackPile = SnackPile.Empty;
        }

        #endregion

        #region Properties
        public virtual SnackPile SnackPile { get; set; }
        public virtual SnackMachine SnackMachine { get; protected set; }
        public virtual int Position { get; protected set; }
        #endregion
    }
}
