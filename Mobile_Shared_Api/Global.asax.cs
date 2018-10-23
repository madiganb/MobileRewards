using Mobile_Shared_Api.Models;
using System.Threading.Tasks;
using System.Timers;
using System.Web;
using System.Web.Http;

namespace Mobile_Shared_Api
{
    public class WebApiApplication : HttpApplication
    {
        //private Timer _timer;

        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            DataRepository.Instance.LoadRepository();

            //In case we want this to run on a background thread
            //Task.Factory.StartNew(() => DataRepository.Instance.LoadRepository());

            //Using the timer, we will periodically persist the data repository to the database as well as persisting when the application ends
            //_timer = new Timer(Settings.PersistanceIntervalMS);
            //_timer.Elapsed += Timer_Elapsed;
            //_timer.Start();
        }

        //private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        //{
        //    //Save these in the background
        //    Task.Factory.StartNew(() => DataRepository.Instance.SaveRepository());
        //    //DataRepository.Instance.SaveRepository();
        //}

        //protected void Application_End()
        //{
        //    DataRepository.Instance.SaveRepository();
        //    _timer.Dispose();
        //}
    }
}
