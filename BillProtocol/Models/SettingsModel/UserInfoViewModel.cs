using BillProtocol.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BillProtocol.Models.SettingsModel
{
    public class UserInfoViewModel
    {
        public UserInfoFormModel Form { get; set; }
        public IEnumerable<SelectListItem> Countries { get; set; }

        public UserInfoViewModel(ApplicationDbContext db, string? name)
        {
            Countries = db.Countries.Where(x => x.Enabled).OrderBy(x => x.Name).Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
            var userInfo = db.UserInfos.SingleOrDefault(x => x.UserId == name);
            
            if(userInfo != null)
            {
                Form = new UserInfoFormModel
                {
                    AddressCity = userInfo.AddressCity,
                    AddressLine1 = userInfo.AddressLine1,
                    AddressLine2 = userInfo.AddressLine2,
                    AddressPostalCode = userInfo.AddressPostalCode,
                    AddressState = userInfo.AddressState,
                    CompanyName = userInfo.CompanyName,
                    CountryId = userInfo.CountryId,
                    FirstName = userInfo.FirstName,
                    LastName = userInfo.LastName,
                    TaxId = userInfo.TaxId
                };
            }
        }
    }
}
