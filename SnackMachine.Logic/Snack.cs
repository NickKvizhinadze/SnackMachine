
namespace SnackMachine.Logic
{
    public class Snack : AggregateRoot
    {
        #region Static Fields
        public static readonly Snack Chocolate = new Snack(1, "Chocolate");
        public static readonly Snack Soda = new Snack(2, "Soda");
        public static readonly Snack Gum = new Snack(3, "Gum");
        public static readonly Snack None = new Snack(0, "None");
        #endregion

        #region Constructor
        protected Snack()
        { }

        private Snack(long id, string name) : this()
        {
            Id = id;
            Name = name;
        }

        #endregion

        #region Properties

        public virtual string Name { get; protected set; }

        #endregion
    }
}
