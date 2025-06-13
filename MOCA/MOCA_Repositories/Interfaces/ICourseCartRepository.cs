using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOCA_Repositories.Models.CourseCartDTO;

namespace MOCA_Repositories.Interfaces
{
    public interface ICourseCartRepository
    {
        CartModel? GetCart(int userId);
        void SaveCart(CartModel cart);
        void RemoveCart(int userId);
    }
}
