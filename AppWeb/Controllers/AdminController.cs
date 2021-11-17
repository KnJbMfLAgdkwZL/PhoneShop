using System.ComponentModel.DataAnnotations;
using Application.DTO.Frontend.Forms;
using Application.DTO.Frontend;
using Application.DTO.PhoneSpecificationsAPI.Latest;
using Application.DTO.PhoneSpecificationsAPI.ListBrands;
using Application.DTO.PhoneSpecificationsAPI.ListPhones;
using Application.DTO.PhoneSpecificationsAPI.PhoneSpecifications;
using Application.DTO.PhoneSpecificationsAPI.Search;
using Application.DTO.PhoneSpecificationsAPI.TopByFans;
using Application.DTO.PhoneSpecificationsAPI.TopByInterest;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;

namespace PhoneShop.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("admin")]
    public class AdminController : Controller
    {
        private readonly IAdminPhones _adminPhones;
        private readonly IPhoneSpecificationsApi _phoneSpecificationServiceApi;
        private readonly IPromoCodes _promoCodes;

        public AdminController(
            IAdminPhones adminPhones,
            IPhoneSpecificationsApi phoneSpecificationsServiceApiApi,
            IPromoCodes promoCodes
        )
        {
            _adminPhones = adminPhones;
            _phoneSpecificationServiceApi = phoneSpecificationsServiceApiApi;
            _promoCodes = promoCodes;
        }

        [HttpGet("index"), HttpGet("")]
        public ActionResult Index()
        {
            return View();
        }


        #region RemoteApi

        [HttpGet("apiBrands")]
        public async Task<ActionResult<ListBrandsDto>> GetApiBrandsAsync(CancellationToken token)
        {
            var listBrands = await _phoneSpecificationServiceApi.GetListBrandsAsync(token);

            if (listBrands == null)
            {
                return BadRequest("Api not respond");
            }

            return View(listBrands);
        }

        [HttpGet("apiPhones")]
        public async Task<ActionResult<ListPhonesDto>> GetApiPhonesAsync(CancellationToken token,
            [FromQuery] [Required] string brandSlug,
            [FromQuery] int page = 1)
        {
            if (page < 1)
            {
                page = 1;
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("brandSlug not set");
            }

            var phones = await _phoneSpecificationServiceApi.GetListPhonesAsync(brandSlug, page, token);

            if (phones == null)
            {
                return BadRequest("Api not respond");
            }

            var listPhonesRes = new ListPhonesFront()
            {
                Phones = phones,
                BrandSlug = brandSlug,
                Page = page
            };

            return View(listPhonesRes);
        }

        [HttpGet("apiSearch")]
        public async Task<ActionResult<SearchDto>> SearchApiAsync([FromQuery] [Required] string query,
            CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("query not set");
            }

            var search = await _phoneSpecificationServiceApi.SearchAsync(query, token);

            if (search == null)
            {
                return BadRequest("Api not respond");
            }

            return View(search);
        }

        [HttpGet("apiLatest")]
        public async Task<ActionResult<LatestDto>> LatestApiAsync(CancellationToken token)
        {
            var latest = await _phoneSpecificationServiceApi.GetLatestAsync(token);

            if (latest == null)
            {
                return BadRequest("Api not respond");
            }

            return View(latest);
        }

        [HttpGet("apiTopByInterest")]
        public async Task<ActionResult<TopByInterestDto>> TopByInterestAsync(CancellationToken token)
        {
            var topByInterest = await _phoneSpecificationServiceApi.GetTopByInterestAsync(token);

            if (topByInterest == null)
            {
                return BadRequest("Api not respond");
            }

            return View(topByInterest);
        }

        [HttpGet("apiTopByFans")]
        public async Task<ActionResult<TopByFansDto>> TopByFansApiAsync(CancellationToken token)
        {
            var topByFans = await _phoneSpecificationServiceApi.GetTopByFansAsync(token);

            if (topByFans == null)
            {
                return BadRequest("Api not respond");
            }

            return View(topByFans);
        }

        #endregion


        #region Shop

        [HttpGet("shop/phones")]
        public async Task<ActionResult<PhonesPageFront>> GetShopPhonesAsync(CancellationToken token,
            [FromQuery] PhonesFilterForm filterForm,
            [FromQuery] int page = 1)
        {
            const int pageSize = 10;

            var phonesPageFront = await _adminPhones.GetAllAsync(filterForm, page, pageSize, token);
            phonesPageFront.FilterForm = filterForm;

            return View(phonesPageFront);
        }

        [HttpGet("shop/phone/{phoneSlug}")]
        public async Task<ActionResult<PhoneSpecificationsDto>> GetShopPhoneAsync(
            [FromRoute] [Required] string phoneSlug,
            CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("phoneSlug not set");
            }

            var phoneSpecFront = await _adminPhones.GetOneAsync(phoneSlug, token);

            if (phoneSpecFront == null)
            {
                return NoContent();
            }

            return View(phoneSpecFront);
        }

        [HttpPost("shop/phone")]
        public async Task<ActionResult<string>> AddOrUpdateShopPhoneAsync([FromForm] PhoneSpecFront phoneSpecFront,
            CancellationToken token)
        {
            var phone = await _adminPhones.AddOrUpdateAsync(phoneSpecFront, token);

            if (phone == null)
            {
                return BadRequest("Api not respond");
            }

            return Ok("PhoneInsertOrUpdateAsync Done");
        }

        #endregion


        #region PromoCodes

        [HttpGet("codes")]
        public async Task<ActionResult<TopByFansDto>> GetPromoCodesAsync(CancellationToken token)
        {
            var codes = await _promoCodes.GetAllAsync(token);
            return View(codes);
        }

        [HttpPost("code")]
        public async Task<ActionResult<TopByFansDto>> CreatePromoCodesAsync(
            [FromForm] [Required] string phoneSlug,
            [FromForm] [Required] string key,
            [FromForm] [Required] int amount,
            [FromForm] [Required] int discount,
            CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not all fields are set");
            }

            await _promoCodes.AddOrUpdateAsync(phoneSlug, key, amount, discount, token);
            return RedirectToAction("GetPromoCodes", "Admin");
        }

        [HttpGet("code/remove")]
        public async Task<ActionResult<TopByFansDto>> RemoveCodeAsync([FromQuery] [Required] string key,
            CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Key not set");
            }

            await _promoCodes.RemoveIfExistAsync(key, token);
            return RedirectToAction("GetPromoCodes", "Admin");
        }

        #endregion
    }
}