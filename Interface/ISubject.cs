using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTP_projekt.Interface
{
    // Interfejs definiujący zachowanie obiektu pełniącego rolę "Subject" w wzorcu projektowym Obserwator.
    public interface ISubject
    {
        // Dodaje obserwatora do listy obserwatorów.
        void Attach(IObserver observer);

        // Usuwa obserwatora z listy obserwatorów.
        void Detach(IObserver observer);

        // Powiadamia wszystkich zarejestrowanych obserwatorów o zmianie stanu.
        void Notify();
    }

}
