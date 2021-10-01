﻿using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models.DTO.RemoteAPI.Latest;
using Models.DTO.RemoteAPI.ListBrands;
using Models.DTO.RemoteAPI.ListPhones;
using Models.DTO.RemoteAPI.PhoneSpecifications;
using Models.DTO.RemoteAPI.Search;
using Models.DTO.RemoteAPI.TopByFans;
using Models.DTO.RemoteAPI.TopByInterest;
using Models.Entities;

namespace PhoneShop.Controllers
{
    [Route("home")]
    [Route("")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPhoneSpecificationClient _phoneSpecification;

        private readonly ITestService _testService;

        public HomeController(ILogger<HomeController> logger, IPhoneSpecificationClient phoneSpecificationClient,
            ITestService testService)
        {
            _logger = logger;
            _phoneSpecification = phoneSpecificationClient;
            _testService = testService;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }

        [HttpGet("index")]
        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("privacy")]
        public async Task<IActionResult> Privacy(CancellationToken ct)
        {
            await _testService.RunTest(ct);

            return View();
        }

        [HttpGet("listBrands")]
        public async Task<ActionResult<ListBrands>> ListBrandsAsync(CancellationToken ct)
        {
            var listBrands = await _phoneSpecification.ListBrandsAsync(ct);
            return Ok(listBrands);
        }

        [HttpGet("listPhones")]
        public async Task<ActionResult<ListPhones>> ListPhonesAsync(CancellationToken ct)
        {
            var listPhones = await _phoneSpecification.ListPhonesAsync(ct, "acer-phones-59", 1);
            return Ok(listPhones);
        }

        [HttpGet("phoneSpecifications")]
        public async Task<ActionResult<PhoneSpecifications>> PhoneSpecificationsAsync(CancellationToken ct)
        {
            var phoneSpecifications =
                await _phoneSpecification.PhoneSpecificationsAsync(ct, "acer_chromebook_tab_10-9139");
            return Ok(phoneSpecifications);
        }

        [HttpGet("search")]
        public async Task<ActionResult<Search>> SearchAsync(CancellationToken ct)
        {
            var search = await _phoneSpecification.SearchAsync(ct, "iPhone 12 pro max");
            return Ok(search);
        }

        [HttpGet("latest")]
        public async Task<ActionResult<Latest>> LatestAsync(CancellationToken ct)
        {
            var latest = await _phoneSpecification.LatestAsync(ct);
            return Ok(latest);
        }

        [HttpGet("topByInterest")]
        public async Task<ActionResult<TopByInterest>> TopByInterestAsync(CancellationToken ct)
        {
            var topByInterest = await _phoneSpecification.TopByInterestAsync(ct);
            return Ok(topByInterest);
        }

        [HttpGet("topByFans")]
        public async Task<ActionResult<TopByFans>> TopByFansAsync(CancellationToken ct)
        {
            var topByFans = await _phoneSpecification.TopByFansAsync(ct);
            return Ok(topByFans);
        }
    }
}