namespace SnackMachine.Logic
{
    public class Snack : Entity
    {
        #region Constructor
        protected Snack()
        { }

        public Snack(string name) : this()
        {
            Name = name;
        }

        #endregion

        #region Properties

        public virtual string Name { get; protected set; }

        #endregion
    }
}
