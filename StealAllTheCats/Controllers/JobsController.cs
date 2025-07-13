using AutoMapper;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StealAllTheCats.Models;
using StealAllTheCats.Models.Dto;
using StealAllTheCats.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace StealAllTheCats.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobsController : ControllerBase
    {
        [HttpGet("{jobId}")]
        public IActionResult GetJobStatus(string jobId)
        {
            var monitoringApi = JobStorage.Current.GetMonitoringApi();
            var jobDetails = monitoringApi.JobDetails(jobId);

            if (jobDetails == null)
            {
                return NotFound(new { status = "NotFound" });
            }

            var state = jobDetails.History?.OrderBy(x=> x.CreatedAt).Last()?.StateName ?? "Unknown";

            return Ok(new { jobId, status = state });
        }
    }
}
