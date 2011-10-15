using System;

using DevExpress.Web.ASPxCallback;
using DevExpress.Web.ASPxGridView;

using RepresentativesBusinessLogic;

using RepresentativesDataAccess;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            grid.DataBind();
            grid.DetailRows.ExpandRow(0);
        }
    }

    protected void detailGrid_DataSelect(object sender, EventArgs e)
    {
        Session["RegionId"] = (sender as ASPxGridView).GetMasterRowKeyValue();
    }

    protected void callbackPanel_Callback(object source, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        var id = int.Parse(e.Parameter);

        var person = new PersonGateway().ById(id);

        ltName.Text = person.Name;
        ltEmail.Text = person.Email;
    }

    protected void SendMessage(object sender, CallbackEventArgs e)
    {
        var parameter = e.Parameter;
        var startIndex = parameter.IndexOf(";") + 1;

        var id = int.Parse(parameter.Substring(0, startIndex - 1));
        var message = parameter.Substring(startIndex, parameter.Length - startIndex);

        var person = new PersonGateway().ById(id);

        new MailSender().SendMessage(person.Email, "Hi From Test Application", message);
    }
}
