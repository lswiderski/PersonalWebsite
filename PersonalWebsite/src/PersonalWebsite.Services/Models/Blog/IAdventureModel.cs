using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebsite.Services.Models
{
    public interface IAdventureModel
    {
        List<AdventureMapPointerDTO> GetListForMap();
        List<AdventureViewModel> GetListForAdmin();
        EditAdventureViewModel GetEdit(int id);
        void Edit(EditAdventureViewModel model);
        void Add(AddAdventureViewModel model);
        AddAdventureViewModel GetAdd();
        void Remove(int id);
    }
}
