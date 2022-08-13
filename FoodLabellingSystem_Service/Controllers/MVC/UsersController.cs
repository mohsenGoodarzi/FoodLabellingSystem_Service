using FoodLabellingSystem_Service.Auth.AuthMVC;
using FoodLabellingSystem_Service.Auth.AuthMVC.Models;
using FoodLabellingSystem_Service.Auth.AuthMVC.Others;
using FoodLabellingSystem_Service.Auth.AuthMVC.Services;
using FoodLabellingSystem_Service.Models;
using FoodLabellingSystem_Service.Other;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace FoodLabellingSystem_Service.Controllers.MVC
{
    [Route("/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly AuthMVC<UserRepo> _authMVC;
        private readonly IHashHelper _hashHelper;
        private readonly ILogger<UsersController> _logger;
        private readonly IEmailSender _emailSender;
        public UsersController(IUserService userService, AuthMVC<UserRepo> authMVC, IHashHelper hashHelper, IEmailSender emailSender, ILogger<UsersController> logger) { 
        _emailSender = emailSender;
            _logger = logger;
            _userService = userService;
            _authMVC = authMVC;
            _hashHelper = hashHelper;
        }
        [HttpGet("Login")]
      
        public IActionResult Login(string returnUrl)
        {
            ViewData["returnUrl"] = returnUrl;
            return View();

        }
        [HttpPost("Login")]
        public async Task<ActionResult> Login(UserLogin userLogin, string? returnUrl)
        {
            List<IUser> users = await _userService.GetAll();
            if (ModelState.IsValid) {
                var result = (FoodLabellingSystem_Service.Auth.AuthMVC.Models.User?)(from usr in users
                             where (usr.UserName == userLogin.IdentefyBy ||
                              usr.Email == userLogin.IdentefyBy ||
                              usr.Phone == userLogin.IdentefyBy) && usr.Password == _hashHelper.Hash512(userLogin.Password)
                             select usr).FirstOrDefault();
                if (result != null) {
                   
                    _authMVC.initClaimsPrincipal(result);

                    if (returnUrl == null)
                    {
                        returnUrl = "/Home/Index";
                        _authMVC.setAuthenticationProperties(returnUrl);
                    }
                    else {
                        _authMVC.setAuthenticationProperties(returnUrl);
                    }

                    await _authMVC.logInAsync();
                }
            
            }

            return View();
        }


        [Authorize(Roles = "Client, Administrator")]
        [HttpGet("Panel/{userName}")]
        public async Task<ActionResult> Panel(string userName) {

           User user = await _userService.GetByUserName(userName);
            if (user.UserName.TrimEnd() == HttpContext.User.Identity?.Name)
            {
                return View(user);
            }
            else { 
            return View("AccessDenied");
            }
        }

      //  [Authorize(Roles = "Administrator")]
        [HttpGet("Index")]
        public async Task<ActionResult> Index()
        {
          List<IUser> users =  await  _userService.GetAll();
            return View(users);
        }

        [HttpGet("Details/{email}")]
        public async Task<ActionResult> Details(string email)
        {
            IUser user = await _userService.GetByEmail(email);
            if (user == null) { 
            return NotFound("The User not found.");
            }
            return View(user);
        }
        [HttpGet("Create")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsersController/Create
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(User user)
        {
            if (ModelState.IsValid) {
                user.Password = _hashHelper.Hash512(user.Password);
                user.Status = StatusType.Activated;
                user.Role = RoleType.Guest;
                QueryResult queryResult = await _userService.Add(user);

                if (queryResult.Result == QueryResultType.SUCCEED)
                {
                    await _userService.ConfirmPhone(user.UserName);
                    string code = _hashHelper.GenerateRAC();
                    string hashedCode = _hashHelper.HashRAC(code);
                    string message = "Hello dear, here is the activation code that you need " + code;
                    await _emailSender.SendEmailAsync(user.Email, "Email address confirmation letter", message);
                    return Redirect("Confirm/Email/" + hashedCode);
                }
                else
                {
                    return View("Error", new ErrorViewModel() { QureyResult = queryResult });
                }

            }
               return View();        
        }
        [Authorize(Roles= "Guest, Administrator")]
        [HttpGet("Confirm/Email/{code}")]
        public async Task<ActionResult> ConfirmEmail(string code)
        {

            string? userName = HttpContext.User.Identity?.Name;
            
            if (!string.IsNullOrEmpty(userName)) {
               User user = await _userService.GetByUserName(userName);
                ViewData["EmailAddress"] = user.Email;
            }
           
            ViewData["Code"] = code;
            return View(); 

        }
       
        [Authorize(Roles = "Guest, Administrator")]
        [HttpPost("Confirm/Email/{code}/{resend?}")]
        public async Task<ActionResult> ConfirmEmail(string code, EmailModel emailModel, bool resend = false) {
           
            if (resend) {
                string? userName = HttpContext.User.Identity?.Name;

                if (!string.IsNullOrEmpty(userName))
                {
                    User user = await _userService.GetByUserName(userName);
                    await _userService.ConfirmPhone(user.UserName);
                    string newCode = _hashHelper.GenerateRAC();
                    string hashedCode = _hashHelper.HashRAC(newCode);
                    string message = "Hello dear, here is the activation code that you need " + newCode;
                    await _emailSender.SendEmailAsync(user.Email, "Email address confirmation letter", message);
                    return Redirect("/Users/Confirm/Email/" + hashedCode);
                }
               

            }

            if (ModelState.IsValid) {

                User user = await _userService.GetByEmail(emailModel.Email);
                if (user != null)
                {
                   
                    string hashedCode = _hashHelper.HashRAC(emailModel.ActivationCode);
                    if (hashedCode == code)
                    {
                        user.Role = RoleType.Client;
                        user.IsPhoneConfirmed = true;
                        user.IsEmailConfirmed = true;
                        QueryResult userUpdated = await _userService.Update(user);
                        QueryResult emailConfirmed = await _userService.ConfirmEmail(user.UserName);

                        if (emailConfirmed.Result == QueryResultType.SUCCEED) {
                            
                            if (userUpdated.Result == QueryResultType.SUCCEED)
                            {
                                StringBuilder stringBuilder = new StringBuilder();
                               
                                return RedirectToAction("Login","Users");
                            }
                            else
                            {
                                return View("Error", new ErrorViewModel() { QureyResult = userUpdated });
                            }
                        }
                        else
                        {
                            return View("Error", new ErrorViewModel() { QureyResult = emailConfirmed });
                        }
                    }
                    else
                    {
                        return View("Error", new ErrorViewModel() { 
                            QureyResult = new QueryResult() 
                            { Result = QueryResultType.FAILED, 
                                Message="The code is wrong."} });
                    }

                }
                return NotFound("User not found");
            }

           return View();
        }

       
        [HttpGet("Password/RequestReset")]
        public ActionResult RequestResetPassword() {
            return View();
        }

        
        [HttpPost("Password/RequestReset")]
        public async Task<ActionResult> RequestResetPassword(RequestResetPasswordModel requestResetPasswordModel)
        {
            if (ModelState.IsValid) {
                var user = (from usr in await _userService.GetAll()
                            where
                            usr.UserName == requestResetPasswordModel.ID ||
                            usr.Email == requestResetPasswordModel.ID ||
                            usr.Phone == requestResetPasswordModel.ID
                            select usr).FirstOrDefault();
                if (user != null)
                {
                    // generate hash code send the email
                   string hashedRequestPasswordReset = _hashHelper.generateHRPR(user.UserName);
                    string message = " Dear Customer, following your request for reseting the accounts password," +
                        "Food Labelling System sends you the link. Please click on the link to change the password" +
                     
                        "<a href ='https://" + Request.Host + "/Users/Password/Reset/"+user.UserName+"/"+ hashedRequestPasswordReset + "' >Change Password</a>";
                    await _emailSender.SendEmailAsync(user.Email, "Requested password reset!", message);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return NotFound("The user not found.");
                }
            }
            return View();
        }

        [HttpGet("Password/Reset/{userName}/{code}")]
        public async Task<ActionResult> ResetPassword(string userName, string code) 
        {

            User user = await _userService.GetByUserName(userName);
            if (user == null) {
                return NotFound("User not found");
            }
            return View(new ResetPasswordModel() { Email = user.Email});
        }


        [HttpPost("Password/Reset/{userName}/{code}")]
        public async Task<ActionResult> ResetPassword(string userName, string code, ResetPasswordModel resetPasswordModel)
        {
            User user = await _userService.GetByUserName(userName);
            if (user == null)
            {
                return NotFound("User not found");
            }
            if (ModelState.IsValid) {
                string checkCode = _hashHelper.generateHRPR(user.UserName);
                if (code == checkCode)
                {
                    // make update password here 
                    string password = _hashHelper.Hash512(resetPasswordModel.Password);

                   QueryResult queryResult = await _userService.UpdatePassword(password, userName);
                    if (queryResult.Result == QueryResultType.SUCCEED)
                    {
                        return RedirectToAction("Login","Users");
                    }
                    else {
                        return View("Error", new ErrorViewModel() { QureyResult = queryResult }); 
                    }
                }
            }
            
            return View();
        }

        [Authorize(Roles = "Guest, Administrator")]
        [HttpGet("Confirm/Phone")]
        public ActionResult ConfirmPhone()
        {
            return View();

        }
        [Authorize(Roles = "Guest, Administrator")]
        [HttpPost("Confirm/Phone")]
        public async Task<ActionResult> ConfirmPhone(PhoneModel phoneModel)
        {

            if (ModelState.IsValid)
            {
                User user = await _userService.GetByPhone(phoneModel.Phone);
                if (user != null)
                {
                    QueryResult queryResult = await _userService.ConfirmPhone(user.UserName);

                    if (queryResult.Result == QueryResultType.SUCCEED)
                    {

                        return RedirectToAction("Panel", "Users");
                    }
                }
            }
            return View();
        }
        [HttpGet("Edit/{userName}")]
        public ActionResult Edit(string userName)
        {
            return View();
        }

        // POST: UsersController/Edit/5
        [HttpPost("Edit/{userName}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(User user)
        {
            if (ModelState.IsValid) {
                QueryResult queryResult = await _userService.Update(user);
                if (queryResult.Result == QueryResultType.SUCCEED)
                {
                    return RedirectToAction("Panel");
                }
                else
                {
                    return View("Error", new ErrorViewModel() { QureyResult = queryResult });
                }
            }
            return View();
            
        }

       [HttpGet("Delete/{userName}")]
        public async Task<ActionResult> Delete(string userName)
        {
            User user = await _userService.GetByUserName(userName);
            if (user == null) { 
            return NotFound("The user not found.");
            }

            return View(user);
        }

        // POST: UsersController/Delete/5
        [HttpPost("Delete/{userName}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string userName)
        {
            
            QueryResult queryResult = await _userService.Remove(userName);
            if (queryResult.Result == QueryResultType.SUCCEED)
            {
                return RedirectToAction("Index", "Home");
            }
            else { 
            
            return View("Error", new ErrorViewModel() { QureyResult = queryResult });
            }
        }
    }
}
