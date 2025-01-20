using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTP_projekt.Interface
{
    //Interfejs do powiadamiania o zmianach w obserwowanych przez Observera obiektach
    public interface IObserver
    {
        void Update();
    }

}
