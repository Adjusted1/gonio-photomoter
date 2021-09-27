using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace Goniometer.Functions
{
    public static class ReportUtils
    {
        public static TimeSpan TimeEstimate(int horizontalSteps, int verticalSteps)
        {
            long step = 0;
            
            step += (new TimeSpan(0, 0, 15)).Ticks; //time to measure

            return new TimeSpan(step * horizontalSteps * verticalSteps);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param sensorname="step"></param>
        /// <param sensorname="start">inclusive</param>
        /// <param sensorname="stop">inclusive</param>
        /// <returns></returns>
        public static double[] Range(double step, double start, double stop)
        {
            if (step == 0)
                throw new ArgumentException("step cannot be zero");

            if (step > Math.Abs(stop - start))
                throw new ArgumentException("step must be less the difference of start and stop");

            if (start > stop & step > 0)
                step *= -1;

            int size = (int)Math.Ceiling((stop - start) / step);
            double[] range = new double[size + 1];

            for (int i = 0; i <= size; i++)
            {
                range[i] = start + (step * i);
            }

            //set last to stop (in case it went over)
            range[range.Length - 1] = stop;

            return range;
        }

        public static void EmailResults(string subject, string body, string to, IEnumerable<Attachment> attachments)
        {
            try
            {
                string from = ConfigurationManager.AppSettings["fromEmail"];
                if (String.IsNullOrEmpty(from))
                    throw new ConfigurationErrorsException("fromEmail has not been configured properly");

                string host = ConfigurationManager.AppSettings["smtpHost"];
                if (String.IsNullOrEmpty(host))
                    throw new ConfigurationErrorsException("smtpHost has not been configured properly");

                MailMessage message = new MailMessage(from, to);
                message.Subject = subject;
                message.Body = body;

                if (attachments != null)
                {
                    foreach (var attachment in attachments)
                    {
                        if (attachment != null)
                            message.Attachments.Add(attachment);
                    }
                }

                SmtpClient client = new SmtpClient(host);
                client.Send(message);
            }
            catch (Exception ex)
            {
                SimpleLogger.Logging.WriteToLog(ex.Message);
            }
        }
    }
}
