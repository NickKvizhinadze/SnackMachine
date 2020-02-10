namespace SnackMachine.Logic.Management
{
    public static class HeadOfficeInstance
    {
        #region Constants
        public const long HeadOfficeId = 1;
        #endregion

        #region Properties
        public static HeadOffice Instance { get; private set; }
        #endregion

        #region Methods
        public static void Init()
        {
            var repository = new HeadOfficeRepository();
            Instance = repository.GetById(HeadOfficeId);
        }
        #endregion
    }
}
