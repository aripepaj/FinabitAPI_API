using FinabitAPI.Utilis;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using AutoBit_WebInvoices.Models;
using System.Collections.Generic;

namespace AutoBit_WebInvoices.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PartnersController : ControllerBase
    {
        private readonly DBAccess _dbAccess;
        private readonly PartnerRepository _partnerRepository;

        public PartnersController(DBAccess dbAccess)
        {
            _dbAccess = dbAccess;
            _partnerRepository = new PartnerRepository();
        }

        [HttpGet("GetCustomerAnalyticsAll")]
        public ActionResult<DataTable> GetCustomerAnalyticsAll(DateTime FromDate, DateTime ToDate)
        {
            return _dbAccess.GetCustomerAnalyticsAll(FromDate, ToDate);
        }

        [HttpGet("GetCustomerAnalyticsByPartnerID")]
        public ActionResult<DataTable> GetCustomerAnalyticsByPartnerID(DateTime FromDate, DateTime ToDate, int PartnerID)
        {
            return _dbAccess.GetCustomerAnalyticsByPartnerID(FromDate, ToDate, PartnerID);
        }

        [HttpGet("GetCustomerAnalyticsByPartnerID_MAR")]
        public ActionResult<DataTable> GetCustomerAnalyticsByPartnerID_MAR(int PartnerID, int ProfileID, int TransactionType, int PageId, int RowsForPage, int Language)
        {
            return _dbAccess.GetCustomerAnalyticsByPartnerID_MAR(PartnerID, ProfileID, TransactionType, PageId, RowsForPage, Language);
        }




        [HttpGet("GetPartners")]
        public ActionResult<List<PartnerModel>> GetPartners_API_Model(
            int partnerTypeID = 2,
            string partnerName = "%",
            string partnerGroup = "%",
            string partnerCategory = "%",
            string placeName = "%",
            string stateName = "%")
        {
            var result = _partnerRepository.GetPartners(partnerTypeID, partnerName, partnerGroup, partnerCategory, placeName, stateName);
            return Ok(result);
        }
    }
}
