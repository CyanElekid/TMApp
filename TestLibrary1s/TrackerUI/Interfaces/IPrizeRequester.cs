using System;
using System.Collections.Generic;
using System.Text;
using TestLibrary1.Models;

namespace TrackerUI.Interfaces
{
    public interface IPrizeRequester
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model">PrizeModel to populate</param>
        void PrizeComplete(PrizeModel model);
    }
}
