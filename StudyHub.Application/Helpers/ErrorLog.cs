using AutoMapper.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StudyHub.Domain.Models;
using StudyHub.Infrastructure.Persistence.Data;
using StudyHub.Infrastructure.Persistence.Repo;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Web.Http;
using static StudyHub.Payment.Helper.Tracer;

namespace StudyHub.Payment.Helper
{
    public class ErrorLog
    {
        string dir = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        string pth = "";
        private readonly IRepository<ErrorModel> applicationDb;
        private ApplicationDbContext dbContext;
        public IServiceProvider Services { get; }

        public ErrorLog()
        {
             dbContext = new ApplicationDbContext(
                   Services.GetRequiredService<
                      DbContextOptions<ApplicationDbContext>>());
        }

        public ErrorLog(IRepository<ErrorModel> _applicationDb)
        {
            applicationDb = _applicationDb;

            pth = System.IO.Path.Combine(dir, Assembly.GetCallingAssembly().GetName().Name);
        }
        public ErrorLog(Exception ex)
        {
            //string pth = ConfigurationManager.AppSettings["errorlog"].ToString();
            string err = ex.ToString();
            //string err = ex.Message;
            DateTime dt = DateTime.Now;
            string fld = dt.ToString("yyyy") + "_" + dt.ToString("MM") + "_";
            pth += fld + dt.ToString("dd") + ".txt";
            Logger.Log(err, pth);

        }

        public ErrorLog(string source, string error)
        {


            string pth = ConfigurationManager.AppSettings["errorlog"].ToString();
            string errMessage = source + "  " + error;
            //string err = ex.Message;
            DateTime dt = DateTime.Now;
            string fld = dt.ToString("yyyy") + "_" + dt.ToString("MM") + "_";
            pth += fld + dt.ToString("dd") + ".txt";
            Logger.Log(errMessage, pth);

        }

        public ErrorLog(string ex)
        {
            string pth = ConfigurationManager.AppSettings["errorlog"].ToString();
            string err = ex;
            DateTime dt = DateTime.Now;
            string fld = dt.ToString("yyyy") + "_" + dt.ToString("MM") + "_";
            pth += fld + dt.ToString("dd") + ".txt";
            Logger.Log(err, pth);
        }


        public ErrorLog(HttpResponseMessage responseMessage)
        {
            var error = responseMessage.Content.ReadAsAsync<HttpError>().Result;

            try
            {

                var erromodel =
                      new ErrorModel
                      {

                          ErrorShortDescription = error.ExceptionMessage,
                          ErrorMessage = error.Message,
                          StackTrace = error.StackTrace,
                          InnerExceptionMessage = error.InnerException.Message,
                          ExceptionType = error.ExceptionType
                      };
                dbContext.ErrorModels.Add(erromodel);
                dbContext.SaveChanges();
            }
            catch(Exception exp)
            {
                throw new Exception(exp.Message);
            }

        }
    }

    public class Tracer
    {
        public Tracer(Exception ex)
        {
            string pth = ConfigurationManager.AppSettings["tracer"].ToString();
            string err = ex.ToString();
            //string err = ex.Message;
            DateTime dt = DateTime.Now;
            string fld = dt.ToString("yyyy") + "_" + dt.ToString("MM") + "_";
            pth += fld + dt.ToString("dd") + ".txt";
            Logger.Log(err, pth);

        }
        public Tracer(string ex)
        {
            string pth = ConfigurationManager.AppSettings["tracer"].ToString();
            string err = ex;
            DateTime dt = DateTime.Now;
            string fld = dt.ToString("yyyy") + "_" + dt.ToString("MM") + "_";
            pth += fld + dt.ToString("dd") + ".txt";
            Logger.Log(err, pth);
        }



    } 


    public static class Logger
    {
        public static void Log(string err, string pth)
        {
            DateTime dt = DateTime.Now;
            try
            {
                if (!File.Exists(pth))
                {
                    using (StreamWriter sw = File.CreateText(pth))
                    {
                        sw.WriteLine(dt.ToString() + " : " + err);
                        sw.WriteLine(" ");
                        sw.Close();
                        sw.Dispose();
                    }
                }
                else
                {
                    using (StreamWriter sw = File.AppendText(pth))
                    {
                        sw.WriteLine(dt.ToString() + " : " + err);
                        sw.WriteLine(" ");
                        sw.Close();
                        sw.Dispose();
                    }

                }
            }
            catch
            {
                Logger.Log(err, pth + dt.ToString("HHmmssffffff") + ".txt");
            }
        }
    }
}

