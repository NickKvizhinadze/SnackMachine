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
            SnackPile = new SnackPile(null, 0m, 0);
        }

        #endregion

        #region Properties
        public SnackPile SnackPile { get; set; }
        public virtual SnackMachine SnackMachine { get; protected set; }
        public virtual int Position { get; protected set; }
        #endregion
    }
}
