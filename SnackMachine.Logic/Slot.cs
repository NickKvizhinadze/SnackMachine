namespace SnackMachine.Logic
{
    public class Slot : Entity
    {
        #region Constructor
        protected Slot()
        {
        }

        public Slot(SnackMachine snackMachine, Snack snack, int quantity, decimal price, int position) : this()
        {
            SnackMachine = snackMachine;
            Snack = snack;
            Quantity = quantity;
            Price = price;
            Position = position;
        }



        #endregion

        #region Properties
        public virtual SnackMachine SnackMachine { get; protected set; }
        public virtual Snack Snack { get;  set; }
        public virtual int Quantity { get;  set; }
        public virtual decimal Price { get;  set; }
        public virtual int Position { get; protected set; }
        #endregion
    }
}
