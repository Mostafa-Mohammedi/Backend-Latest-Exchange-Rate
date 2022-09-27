

using ViewModels;

namespace Infrastructure
{
    public interface IRepository<T>                                                            
    {
       

        UpdateViewModel GetUpdate(string baseName);
       
        

        UpdateViewModel SaveData(T instance);                                                  

    }
}
