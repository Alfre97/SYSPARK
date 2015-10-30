using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;

namespace SYSPARK.App_Utility
{
    public class ButtonStyle
    {
        public void buttonStyleRed(HtmlInputButton button, string message)
        {
            button.Visible = true;
            button.Style.Add("background-color", "red");
            button.Style.Add("color", "white");
            button.Value = message;
        }

        public void buttonStyleBlue(HtmlInputButton button, string message)
        {
            button.Visible = true;
            button.Style.Add("background-color", "blue");
            button.Style.Add("color", "white");
            button.Value = message;
        }

        public void buttonStyleWhite(HtmlInputButton button, string message)
        {
            button.Visible = true;
            button.Style.Add("background-color", "white");
            button.Style.Add("color", "red");
            button.Value = message;
        }
    }
}