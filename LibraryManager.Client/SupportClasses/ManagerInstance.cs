using LibraryManager.Model;

namespace LibraryManager.Client.SupportClasses
{

    static class ManagerInstance
    {
        private static Manager? _instance { get; set; }
        public static Manager Instance
        {
            get
            {
                if (_instance == null)
                {
                    try
                    {
                        _instance = new Manager();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Ошибка при создании экземпляра класса Manager: " + ex.Message);
                    }
                }
                return _instance;
            }
        }
    }
}
