using BillProtocol.Data;
using BillProtocol.Models.SettingsModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BillProtocol.Controllers
{
    [Authorize]
    public class SettingsController : Controller
    {
        private ApplicationDbContext _db;

        public SettingsController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IndexSettingsViewModel model = new IndexSettingsViewModel(_db, User.Identity.Name);
            return View(model);
        }

        public IActionResult UserInfo()
        {
            UserInfoViewModel model = new UserInfoViewModel(_db, User.Identity.Name);
            return View(model);
        }

        [HttpPost]
        public IActionResult UserInfo(UserInfoFormModel Form)
        {
            if(!ModelState.IsValid)
            {
                UserInfoViewModel model = new UserInfoViewModel(_db, User.Identity.Name);
                model.Form = Form;
                return View(model);
            }

            var userInfo = _db.UserInfos.SingleOrDefault(x => x.UserId == User.Identity.Name);
            if(userInfo == null)
            {
                userInfo = new UserInfo
                {
                     UserId = User.Identity.Name, AddressCity = Form.AddressCity, AddressLine1 = Form.AddressLine1,
                     AddressLine2 = Form.AddressLine2, AddressPostalCode = Form.AddressPostalCode, AddressState = Form.AddressState,
                     CompanyName = Form.CompanyName, CountryId = Form.CountryId, FirstName = Form.FirstName, LastName = Form.LastName,
                     TaxId = Form.TaxId
                };
                _db.UserInfos.Add(userInfo);
            }
            else
            {
                userInfo.AddressLine1 = Form.AddressLine1;
                userInfo.AddressLine2 = Form.AddressLine2;
                userInfo.AddressState = Form.AddressState;
                userInfo.CompanyName = Form.CompanyName;
                userInfo.CountryId = Form.CountryId;
                userInfo.FirstName = Form.FirstName;
                userInfo.LastName = Form.LastName;
                userInfo.TaxId = Form.TaxId;
                userInfo.AddressCity = Form.AddressCity;
                userInfo.AddressPostalCode = Form.AddressPostalCode;
            }

            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult MyWallets()
        {
            MyWalletsViewModel model = new MyWalletsViewModel(_db, User.Identity.Name);
            return View(model);
        }

        public IActionResult AddWallet()
        {
            AddWalletViewModel model = new AddWalletViewModel(_db, User.Identity.Name);
            return View(model);
        }

        [HttpPost]
        public IActionResult AddWallet(AddWalletFormModel Form)
        {
            if(!string.IsNullOrEmpty(Form.Address))
            {
                var existWallet = _db.Wallets.Any(x => x.Address == Form.Address);
                if(existWallet)
                {
                    ModelState.AddModelError("Form.Address", "The XRPL address has already been added above.");
                }
            }
            if(!ModelState.IsValid)
            {
                AddWalletViewModel model = new AddWalletViewModel(_db, User.Identity.Name);
                model.Form = Form;
                return View(model);
            }

            Wallet wallet = new Wallet
            {
                 Id = Guid.NewGuid(), CreatedAt = DateTime.UtcNow, UserId = User.Identity.Name,
                 Name = Form.Name, Address = Form.Address
            };
            _db.Wallets.Add(wallet);
            _db.SaveChanges();

            return RedirectToAction("MyWallets");
        }

        public IActionResult EditWallet(Guid id)
        {
            EditWalletViewModel model = new EditWalletViewModel(_db, id);
            return View(model);
        }

        [HttpPost]
        public IActionResult EditWallet(EditWalletFormModel Form)
        {
            if (!string.IsNullOrEmpty(Form.Address))
            {
                var existWallet = _db.Wallets.Any(x => x.Address == Form.Address && x.UserId != User.Identity.Name);
                if (existWallet)
                {
                    ModelState.AddModelError("Form.Address", "The XRPL address has already been added.");
                }
            }
            if (!ModelState.IsValid)
            {
                EditWalletViewModel model = new EditWalletViewModel(_db, Form.Id);
                model.Form = Form;
                return View(model);
            }

            Wallet wallet = _db.Wallets.SingleOrDefault(x => x.Id == Form.Id);
            wallet.Address = Form.Address;
            wallet.Name = Form.Name;
            _db.SaveChanges();
            return RedirectToAction("MyWallets");
        }



        public IActionResult MyDestinations()
        {
            MyDestinationsViewModel model = new MyDestinationsViewModel(_db, User.Identity.Name);
            return View(model);
        }

        public IActionResult AddDestination()
        {
            AddDestinationViewModel model = new AddDestinationViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult AddDestination(AddDestinationFormModel Form)
        {
            if(!string.IsNullOrEmpty(Form.Address))
            {
                var existAddress = _db.Destinations.Any(x => x.UserId == User.Identity.Name && x.Address == Form.Address);
                if(existAddress)
                {
                    ModelState.AddModelError("Form.Address", "This destination/email is already added to your account.");
                }
            }
            if(!ModelState.IsValid)
            {
                AddDestinationViewModel model = new AddDestinationViewModel();
                model.Form = Form;
                return View(model);
            }

            Destination destination = new Destination
            {
                Address = Form.Address,
                CreatedAt = DateTime.UtcNow,
                Id = Guid.NewGuid(),
                Name = Form.Name,
                UserId = User.Identity.Name
            };
            _db.Destinations.Add(destination);
            _db.SaveChanges();
            return RedirectToAction("MyDestinations");
        }

        public IActionResult EditDestination(Guid id)
        {
            EditDestinationViewModel model = new EditDestinationViewModel(_db, id);
            return View(model);
        }

        [HttpPost]
        public IActionResult EditDestination(EditDestinationFormModel Form)
        {
            if (!string.IsNullOrEmpty(Form.Address))
            {
                var existWallet = _db.Destinations.Any(x => x.Address == Form.Address && x.UserId != User.Identity.Name);
                if (existWallet)
                {
                    ModelState.AddModelError("Form.Address", "The email address (destination) has already been added.");
                }
            }
            if (!ModelState.IsValid)
            {
                EditDestinationViewModel model = new EditDestinationViewModel(_db, Form.Id);
                model.Form = Form;
                return View(model);
            }

            Destination wallet = _db.Destinations.SingleOrDefault(x => x.Id == Form.Id);
            wallet.Address = Form.Address;
            wallet.Name = Form.Name;
            _db.SaveChanges();
            return RedirectToAction("MyDestinations");
        }
    }
}
