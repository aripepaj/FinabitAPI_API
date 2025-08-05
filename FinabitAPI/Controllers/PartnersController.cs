using Finabit_API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;

namespace AutoBit_WebInvoices.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PartnersController : ControllerBase
    {
        private readonly DBAccess _dbAccess;

        public PartnersController(DBAccess dbAccess)
        {
            _dbAccess = dbAccess;
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
    }
}
