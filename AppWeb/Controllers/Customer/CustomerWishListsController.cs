using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Threading;

namespace PhoneShop.Controllers.Customer
{
    [Authorize(Roles = "Customer")]
    [Route("CustomerWishLists")]
    public class CustomerWishListsController : Controller
    {
        private readonly ICustomerWishList _customerWishList;

        public CustomerWishListsController(ICustomerWishList customerWishList)
        {
            _customerWishList = customerWishList;
        }

        [HttpGet("wishLists")]
        public async Task<ActionResult> GetWishListsAsync(CancellationToken token)
        {
            var userMail = User.Identity?.Name;
            var wishList = await _customerWishList.GetAllAsync(userMail, token);
            return View(wishList);
        }

        [HttpGet("wishList/add/{phoneSlug}")]
        public async Task<ActionResult> AddWishListAsync([FromRoute] [Required] string phoneSlug,
            CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("phoneSlug not set");
            }

            var userMail = User.Identity?.Name;
            await _customerWishList.AddIfNotExistAsync(phoneSlug, userMail, token);
            return Ok("AddToWishListAsync ok");
        }

        [HttpGet("wishList/remove/{phoneSlug}")]
        public async Task<ActionResult> RemoveWishListAsync([FromRoute] [Required] string phoneSlug,
            CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("phoneSlug not set");
            }

            var userMail = User.Identity?.Name;
            await _customerWishList.RemoveAsync(phoneSlug, userMail, token);
            return Ok("RemoveFromWishListAsync ok");
        }
    }
}