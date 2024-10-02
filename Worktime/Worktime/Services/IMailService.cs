using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Worktime.Models;
using Worktime.ViewModel;

namespace Worktime.Services
{
    public interface IMailService
    {
        Task SendMajReport(MajReportRequest request);

        Task SendForgotPassword(ForgotPasswordVM request);
    }
}
